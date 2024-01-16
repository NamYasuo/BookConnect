﻿using System;
using System.Security.Claims;
using DataAccess.DTO;

namespace APIs.Services.Intefaces
{
	public interface ITokenService
	{
        TokenResponse GetToken(IEnumerable<Claim> claims);
        string GetRefeshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

