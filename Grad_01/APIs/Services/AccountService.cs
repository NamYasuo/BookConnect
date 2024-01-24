using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using APIs.Services.Intefaces;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using DataAccess.DAO;
using DataAccess.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace APIs.Repositories.Intefaces
{
    public class AccountService : IAccountService
    {
        //private readonly ITokenService tokenService;
        private readonly IConfiguration config;

        private AccountDAO? accountDAO;

        public AccountService(ITokenService tokenService, IConfiguration config)
        {
            //this.tokenService = tokenService;
            this.config = config;
        }

        //User services
        public Task<IdentityResult> ChangePassword(PasswordChangeDTO model)
        {
            throw new NotImplementedException();
        }

        public AppUser Register(RegisterDTO model)
        {
            accountDAO = new AccountDAO();
            HashPassword(model.Password, out byte[] salt, out byte[] pwdHash);
            AppUser user = new AppUser()
            {
                UserId = Guid.NewGuid(),
                Username = model.Username,
                Email = model.Email,
                Password = Convert.ToHexString(pwdHash),
                Salt = Convert.ToHexString(salt),
                RoleId = GetRoleDetails("BaseUser").RoleId,
            };
            accountDAO.CreateAccount(user);
            return user;
        }

        public AppUser FindUserByEmailAsync(string email) => new AccountDAO().FindUserByEmailAsync(email);

        public string CreateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "BaseUser"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("userId", user.UserId.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Pepper").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: "Book connect",
                audience: "Pikachu",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public TokenInfo GenerateRefreshToken()
        {
            var token = new TokenInfo
            {
                RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiredDate = DateTime.Now.AddDays(6)
            }; return token;
        }

        public bool VerifyPassword(string pwd, string hash, byte[] salt, out byte[] result)
        {
            const int keySize = 64;
            const int iterations = 360000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(pwd),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            result = hashToCompare;
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        private void HashPassword(string pwd, out byte[] salt, out byte[] pwdHash)
        {
            const int keySize = 64;
            const int iterations = 360000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(pwd),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            pwdHash = hash;
        }

        public void AddNewRole(Role role) => new AccountDAO().AddNewRole(role);

        public Role GetRoleDetails(string roleName) => new AccountDAO().GetRolesDetails(roleName);

        //Address services
        public List<Address> GetAllUserAdderess(Guid userId) => new AddressDAO().GetAllUserAddress(userId);

        public Address GetDefaultAddress(Guid userId) => new AddressDAO().GetUserDefaultAddress(userId);

        //public UserProfile? GetUserProfile(string token)
        //{
        //    UserProfile? profile = new UserProfile();
        //    try
        //    {
               
        //    }
        //    catch(Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
    }
}

