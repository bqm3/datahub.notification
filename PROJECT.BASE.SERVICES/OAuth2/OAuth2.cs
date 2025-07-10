using Lib.Consul.Configuration;
using PROJECT.BASE.CORE;
using PROJECT.BASE.ENTITY;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROJECT.BASE.SERVICES
{
    public class OAuth2
    {
        public static string OAuth2_GetJWT()
        {
            string result = OAuth2Provider.request
                (
                    BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) + "/auth/realms/master/protocol/openid-connect/token",
                    "POST", OAuth2Provider.buildQueryString(new Dictionary<string, string>
                    {
                            { "username",BaseSettings.Get(ConstantConsul.Microservice_Identity_UserName) },
                            { "password",BaseSettings.Get(ConstantConsul.Microservice_Identity_PassWord) },
                            { "grant_type","password" },
                            { "client_id",BaseSettings.Get(ConstantConsul.Microservice_Identity_ClientId) },
                            { "client_secret",BaseSettings.Get(ConstantConsul.Microservice_Identity_ClientSecret) }
                    })
                );
            return result;
        }
        public static string OAuth2_GetJWTByLoginInfo(LoginInfo info)
        {
            string result = OAuth2Provider.request
                (
                        BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) + string.Format("/auth/realms/{0}/protocol/openid-connect/token", info.Realms),
                        "POST", OAuth2Provider.buildQueryString(new Dictionary<string, string>
                        {
                            { "username",info.UserName },
                            { "password",info.PassWord },
                            { "grant_type","password" },
                            { "client_id",info.ClientID }
                        })
                 );
            return result;
        }
        public static string OAuth2_GetJWTByRefreshToken(RefreshTokenInfo info)
        {
            string result = OAuth2Provider.request
                (
                        BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) + string.Format("/auth/realms/{0}/protocol/openid-connect/token", info.Realms),
                        "POST", OAuth2Provider.buildQueryString(new Dictionary<string, string>
                        {
                            { "client_id",info.ClientID },
                            { "grant_type","refresh_token" },
                            { "refresh_token",info.RefreshToken }
                        })
                    );
            return result;
        }
    }
}
