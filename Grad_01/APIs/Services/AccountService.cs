using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using DataAccess.DAO;
using DataAccess.DAO.Ecom;
using DataAccess.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace APIs.Repositories.Interfaces
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
                IsValidated = false,
                RoleId = GetRoleDetails("BaseUser").RoleId,
            };
            accountDAO.CreateAccount(user);
            return user;
        }

        public AppUser? FindUserByEmailAsync(string email) => new AccountDAO().FindUserByEmailAsync(email);

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

        public string? GetUsernameById(Guid userId) => new AccountDAO().GetNameById(userId);

        //Address services
        public List<Address> GetAllUserAdderess(Guid userId) => new AddressDAO().GetAllUserAddress(userId);

        public Address GetDefaultAddress(Guid userId) => new AddressDAO().GetUserDefaultAddress(userId);

        //Account validation
        public int SetUserIsValidated(bool choice, Guid userId) => new AccountDAO().SetIsAccountValid(choice, userId);

        public bool IsUserValidated(Guid userId) => new AccountDAO().IsUserValidated(userId);

        //Agency Registration
        public string RegisterAgency(AgencyRegistrationDTO dto, string logoUrl)
        {
            //register post address
            AddressDAO addressDAO = new AddressDAO();
            AccountDAO accDAO = new AccountDAO();
            AgencyDAO agencyDAO = new AgencyDAO();

            Guid addressId = Guid.NewGuid();
            int newAddress = addressDAO.AddNewAddress(new Address
            {
                AddressId = addressId,
                Rendezvous = dto.Rendezvous,
                Default = true
            });
            //add new agency
            if (newAddress > 0)
            {
                int newAgency = agencyDAO.AddNewAgency(new Agency
                {
                    AgencyId = Guid.NewGuid(),
                    AgencyName = dto.AgencyName,
                    PostAddressId = addressId,
                    LogoUrl = logoUrl,
                    BusinessType = dto.BusinessType,
                    OwnerId = dto.OwnerId
                });
                if (newAgency > 0)
                {
                    int changes = accDAO.SetIsAgency(true, dto.OwnerId);
                    if (changes > 0) return "Successful!";
                    else return "Fail to set agency!";
                }
                else return "Fail to add new agency!";
            }
            else return "Fail to add address!";
            //change IsSeller in AppUser
        }

        public bool IsSeller(Guid userId) => new AccountDAO().IsSeller(userId);

        public Task<bool> IsBanned(Guid userId) => new AccountDAO().IsBanned(userId);

        public Agency GetAgencyById(Guid agencyId) => new AgencyDAO().GetAgencyById(agencyId);

        public int UpdateAgency(AgencyUpdateDTO updatedData, string? updatedLogoUrl)
        {
            AddressDAO addressDAO = new AddressDAO();
            AgencyDAO agencyDAO = new AgencyDAO();
            Guid addressId = Guid.Empty;

            Address? oldAddress = agencyDAO.GetCurrentAddress(updatedData.AgencyId);
            if(oldAddress != null) addressId = oldAddress.AddressId;

            if (oldAddress != null && oldAddress.Rendezvous != null && updatedData.PostAddress != null)
            {
                if(!oldAddress.Rendezvous.Equals(updatedData.PostAddress))
                {
                    Address newAddress = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        City_Province = null,
                        District = null,
                        SubDistrict = null,
                        Rendezvous = updatedData.PostAddress
                    };
                    if (addressDAO.AddNewAddress(newAddress) > 0) addressId = newAddress.AddressId;
                }
            }

            string? logoUrl = (updatedLogoUrl != null) ? updatedLogoUrl : agencyDAO.GetCurrentLogoUrl(updatedData.AgencyId);

            return agencyDAO.UpadateAgency(new Agency
            {
                AgencyId = updatedData.AgencyId,
                AgencyName = updatedData.AgencyName,
                PostAddressId = addressId,
                BusinessType = updatedData.BusinessType,
                LogoUrl = logoUrl,
                OwnerId = updatedData.OwnerId,
            });
        }
    }
}

