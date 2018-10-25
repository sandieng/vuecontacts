using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;
using VueContactsAPI.Services;
using VueContactsAPI.ViewModels;

namespace VueContactsAPI.Middleware
{
    public class SecurityMiddleware
    {
        private RequestDelegate _next;
        private IConfiguration _configuration;

        public SecurityMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            // Fill in the security next and move to the next middleware in the pipeline
            var authHeader = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader))
            {
                await AccessDenied(context, "Unauthorized access denied.");
                return;
            }

            // Make sure the token comes from FB with the correct client id
            var message = "";
            if (!ValidateToken(context, authHeader, out message))
            {
                await AccessDenied(context, message);
                return;
            }

            await _next(context);
        }

        private bool ValidateToken(HttpContext context, Microsoft.Extensions.Primitives.StringValues authHeader, out string message)
        {
            var jwtService = context.RequestServices.GetService(typeof(IJwtService)) as IJwtService;
            var jwtToken = jwtService.DecodeJwt(authHeader);
            var issuer = jwtToken.Payload["iss"].ToString();
            var subject = jwtToken.Payload["sub"].ToString();
            var audience = jwtToken.Payload["aud"].ToString();
            var expiry = Convert.ToInt64(jwtToken.Payload["exp"]);

            if (issuer != _configuration["Facebook:Issuer"])
            {
                message = "You are not authorised. Please log in or sign up first.";
                return false;
            }

            if (subject != _configuration["Facebook:Subject"])
                    {
                message = "You are not authorised. Please log in or sign up first.";
                return false;
            }

            if (audience != _configuration["Facebook:Audience"])
            {
                message = "You are not authorised. Please log in or sign up first.";
                return false;
            }

            if (expiry < (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds)
            {
                message = "Your session has expired, please log in.";
                return false;
            }

            message = "";
            return true;
        }

        private async Task AccessDenied(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var error = new ErrorVM
            {
                Message = message
            };

            var jsonString = JsonConvert.SerializeObject(error, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            await context.Response.WriteAsync(jsonString);
        }
    }
}
