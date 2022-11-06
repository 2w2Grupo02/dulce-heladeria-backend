using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Dulce.Heladeria.Services.Helper
{
    public static class AuthHelper
    {
        public static string GetRole(HttpRequest Request)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);
            var claims = token.Claims;
            var claim = claims.Where(x => x.Type == "role").FirstOrDefault();
            return claim.Value;
        }
    }
}
