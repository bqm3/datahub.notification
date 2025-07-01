using Lib.Utility;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Reflection;

namespace Lib.Middleware
{
    public class JwtBuilder
    {
        private readonly JwtOptions _options;
        public static string JwtSecurityKey = "PPIT.CORE.RELEASE_76623d90-837b-4862-8031-d13a99eccc98";
        public static string JwtIssuer = "PPIT.Server.1";
        public static string JwtAudience = "PPIT.Client";

        public JwtBuilder(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        //public static string GetToken(string userId)
        //{
        //    string JwtExpiryInHours = "1";

        //    int expiryInHours;
        //    var Claims = new[]
        //    {
        //        new Claim("userId", userId.ToString()),
        //    };
        //    //JwtSecurityKey = Config.APP_SETTING["jwt"]["Secret"].Value;
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecurityKey));

        //    key.KeyId = "PPIT_Relase_key";
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        //    Int32.TryParse(JwtExpiryInHours, out expiryInHours);
        //    var expiry = DateTime.UtcNow.AddHours(expiryInHours);



        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(Claims),
        //        Expires = expiry,
        //        Issuer = JwtIssuer,
        //        Audience = JwtAudience,
        //        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        //    };


        //    var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        //    LogEventError.LogEvent("GetToken:" + token);
        //    return tokenHandler.WriteToken(token);
        //}
        //public static string ValidateToken(string token)
        //{
        //    LogEventError.LogEvent("ValidateToken:" + token);
        //    Console.WriteLine("ValidateToken:" + token);            
        //    try
        //    {
        //        //JwtSecurityKey = Config.APP_SETTING["jwt"]["Secret"].Value;
        //        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSecurityKey));
        //        signingKey.KeyId = "PPIT_Relase_key";
        //        Console.WriteLine(signingKey);
        //        SecurityToken tokenValidated = null;
        //        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        //        TokenValidationParameters validationParameters = new TokenValidationParameters()
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = signingKey,

        //            ValidateAudience = true,
        //            ValidAudience = JwtAudience,

        //            ValidateIssuer = true,
        //            ValidIssuer = JwtIssuer,

        //            ValidateLifetime = true
        //        };
        //        handler.ValidateToken(token, validationParameters, out tokenValidated);
        //        IEnumerable<Claim> jwtSecurityToken = ((JwtSecurityToken)tokenValidated).Claims;
        //        var result = ((JwtSecurityToken)tokenValidated).Claims.FirstOrDefault(x => x.Type == "userId")?.Value;                
        //        LogEventError.LogEvent("ValidateToken:GOOD");
        //        Console.WriteLine("token: GOOD");
        //        return result;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
        //        Console.WriteLine("token: BAD");
        //        Console.WriteLine(ex.Message);
        //        return String.Empty;
        //    }

        //}

        public static string GetToken(string userId)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.CONFIGURATION_GLOBAL.jwt.Secret));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim("userId", userId.ToString()),
            };
            //var expirationDate = DateTime.Now.AddMinutes(_options.ExpiryMinutes);
            var expirationDate = DateTime.Now.AddMinutes(int.Parse(Config.CONFIGURATION_GLOBAL.jwt.ExpiryMinutes));
            //var expirationDate = DateTime.Now.AddMinutes(1440);            
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: expirationDate);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return "Bearer " + encodedJwt;
        }

        public static string ValidateToken(string token)
        {
            var principal = GetPrincipal(token);
            if (principal == null)
            {
                return string.Empty;
            }

            ClaimsIdentity identity;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
                var userIdClaim = identity.FindFirst("userId");
                //var userId = new Guid(userIdClaim.Value);            
                return userIdClaim.Value;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return string.Empty;
            }
            
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                {
                    return null;
                }
                var key = Encoding.UTF8.GetBytes(Config.CONFIGURATION_GLOBAL.jwt.Secret);
                var parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                IdentityModelEventSource.ShowPII = true;
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                return null;
            }
        }
    }
}
