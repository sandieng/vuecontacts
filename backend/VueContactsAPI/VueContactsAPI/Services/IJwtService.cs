using System.IdentityModel.Tokens.Jwt;

namespace VueContactsAPI.Services
{
    public interface IJwtService
    {
        JwtSecurityToken DecodeJwt(string token);

    }
}
