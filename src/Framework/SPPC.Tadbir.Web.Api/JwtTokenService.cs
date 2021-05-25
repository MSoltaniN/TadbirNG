using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Web.Api
{
    /// <summary>
    ///
    /// </summary>
    public class JwtTokenService : ITokenService
    {
        /// <summary>
        ///
        /// </summary>
        public JwtTokenService()
        {
            ////_config = configuration;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Generate(ISecurityContext context)
        {
            Verify.ArgumentNotNull(context, nameof(context));
            var utcNow = DateTime.UtcNow;
            var contextClaims = new Dictionary<string, object>
            {
                { AppConstants.ContextCookieName, context.User }
            };
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, context.User.UserName),
                new Claim(ClaimTypes.GivenName, context.User.PersonFirstName),
                new Claim(ClaimTypes.Surname, context.User.PersonLastName)
            };
            var descriptor = new SecurityTokenDescriptor
            {
                Audience = JwtConfig.Audience,
                Issuer = JwtConfig.Issuer,
                ////Audience = _config["Jwt:Audience"],
                ////Issuer = _config["Jwt:Issuer"],
                Subject = new ClaimsIdentity(claims),
                IssuedAt = utcNow,
                Claims = contextClaims,
                ////Expires = utcNow.AddMinutes(Double.Parse(_config["Jwt:Expiration"])),
                Expires = utcNow.AddMinutes(Double.Parse(JwtConfig.Expiration)),
                SigningCredentials = GetSigningCredentials()
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Validate(string token)
        {
            var validated = GetValidatedToken(token);
            return validated != null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ISecurityContext GetSecurityContext(string token)
        {
            var context = default(SecurityContext);
            var validatedToken = GetValidatedToken(token);
            if (validatedToken != null)
            {
                string jsonContext = validatedToken.Payload[AppConstants.ContextCookieName].ToString();
                var userContext = JsonHelper.To<UserContextViewModel>(jsonContext);
                context = new SecurityContext(userContext);
            }

            return context;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secretKey = Convert.FromBase64String(JwtConfig.Secret);
            ////var secretKey = Convert.FromBase64String(_config["Jwt:Secret"]);
            var symmetricKey = new SymmetricSecurityKey(secretKey);
            return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
        }

        private JwtSecurityToken GetValidatedToken(string token)
        {
            Verify.ArgumentNotNullOrEmptyString(token, nameof(token));
            var validation = GetValidationParameters();
            var handler = new JwtSecurityTokenHandler();
            _ = handler.ValidateToken(token, validation, out SecurityToken validated);
            return validated as JwtSecurityToken;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            var credentials = GetSigningCredentials();
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = JwtConfig.Issuer,
                ValidAudience = JwtConfig.Audience,
                ////ValidIssuer = _config["Jwt:Issuer"],
                ////ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = credentials.Key
            };
        }

        private readonly IConfiguration _config;
    }

    // TEMPORARY CLASS
    internal static class JwtConfig
    {
        internal const string Audience = "tadbir-app";
        internal const string Issuer = "tadbir-api";
        internal const string Expiration = "60";
        internal const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
    }
}
