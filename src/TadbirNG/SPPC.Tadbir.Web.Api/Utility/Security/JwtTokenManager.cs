using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    ///
    /// </summary>
    public class JwtTokenManager : ITokenManager
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        public JwtTokenManager(IConfiguration configuration)
        {
            _config = configuration;
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
                new Claim(ClaimTypes.GivenName, context.User.PersonFullName)
            };
            var descriptor = new SecurityTokenDescriptor
            {
                Audience = _config["Jwt:Audience"],
                Issuer = _config["Jwt:Issuer"],
                Subject = new ClaimsIdentity(claims),
                IssuedAt = utcNow,
                Claims = contextClaims,
                Expires = utcNow.AddMinutes(Double.Parse(_config["Jwt:Expiration"])),
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
            var secretKey = Convert.FromBase64String(_config["Jwt:Secret"]);
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
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = credentials.Key
            };
        }

        private readonly IConfiguration _config;
    }
}
