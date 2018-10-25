using System;
using System.IdentityModel.Tokens.Jwt;

namespace VueContactsAPI.Services
{
    public class JwtService : IJwtService
    {
        public JwtSecurityToken DecodeJwt(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (token.ToLower().Contains("bearer"))
                {
                    token = token.Substring(7);
                }
                var reversedToken = handler.ReadToken(token);

                return (JwtSecurityToken)reversedToken;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
