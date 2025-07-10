using Lib.Consul.Configuration;
using Lib.Middleware;
using Lib.Utility;
using Newtonsoft.Json;
using PROJECT.BASE.CORE;
using PROJECT.BASE.DAO;
using PROJECT.BASE.ENTITY;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Lib.Json;

namespace PROJECT.BASE.SERVICES
{
    public class OAuth2Service : IOAuth2Service
    {
        public OAuth2Service() { }
        #region OAuth2_User
        public TokenInfo OAuth2_UserLogin(LoginInfo info)
        {
            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWTByLoginInfo(info));
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(tokenInfo.access_token);
            //var Claims = decodedValue.Claims.ToString();
            string[] stringSeparators = new string[] { "}.{" };
            string[] firstNames = decodedValue.ToString().Split(stringSeparators, StringSplitOptions.None);            
            var session_state = JSON.Parse("{" + firstNames[1])["session_state"].Value;
            var userId = JSON.Parse("{" + firstNames[1])["sub"].Value; 
            var authTime = JSON.Parse("{" + firstNames[1])["iat"].Value;
            var expTime = JSON.Parse("{" + firstNames[1])["exp"].Value;
            var tokenApi = JwtBuilder.GetToken($"{info.Realms}#{userId}#{session_state}#{info.AppCode}#{authTime}#{expTime}");
            //var tokenApi = JwtBuilder.GetToken($"{realms}#{userId}#{sessionState}#{appCode}#{authTime}#{expTime}");
            tokenInfo.token_api = tokenApi;
            tokenInfo.user_id = userId;
            return tokenInfo;
        }
        public TokenInfo OAuth2_RefreshToken(RefreshTokenInfo refreshTokenInfo)
        {
            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWTByRefreshToken(refreshTokenInfo));
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(tokenInfo.access_token);
            //var Claims = decodedValue.Claims.ToString();
            string[] stringSeparators = new string[] { "}.{" };
            string[] firstNames = decodedValue.ToString().Split(stringSeparators, StringSplitOptions.None);
            var session_state = JSON.Parse("{" + firstNames[1])["session_state"].Value;
            var userId = JSON.Parse("{" + firstNames[1])["sub"].Value;
            var authTime = JSON.Parse("{" + firstNames[1])["iat"].Value;
            var expTime = JSON.Parse("{" + firstNames[1])["exp"].Value;
            var tokenApi = JwtBuilder.GetToken($"{refreshTokenInfo.Realms}#{userId}#{session_state}#{refreshTokenInfo.AppCode}#{authTime}#{expTime}");
            //var tokenApi = JwtBuilder.GetToken($"{realms}#{userId}#{sessionState}#{appCode}#{authTime}#{expTime}");
            tokenInfo.token_api = tokenApi;
            tokenInfo.user_id = userId;
            return tokenInfo;
            
        }
        public string OAuth2_UserLogout(string realms,string userID, long authTime)
        {           
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var resultPost = HttpClientBase.POST(null,
                BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}/logout", realms,userID),
                tokenInfo.access_token,string.Empty);
            if (resultPost != null && resultPost["errorMessage"] != null)
                return resultPost["errorMessage"];
            else
            {
                var info = DataObjectFactory.GetInstanceLOG_LOGIN().LOG_LOGIN_GetInfo(userID, authTime);
                if (info == null)
                    return string.Empty;
                if (info.AUTH_TIME < authTime)
                {
                    info.EXP_TIME = Utility.ConvertToUnixTime(DateTime.Now);
                    DataObjectFactory.GetInstanceLOG_LOGIN().LOG_LOGIN_Update(info);                    
                }
                return string.Empty;
            }
            
        }       
        public string OAuth2_UserCreate(string realms, UserPostInfo info)
        {            
            Dictionary<string, string[]> userProfile = new Dictionary<string, string[]>();
            Dictionary<string, string[]> userProfilePost = info.attributes == null ?  new Dictionary<string, string[]>() : info.attributes;
            if (!userProfilePost.ContainsKey("ReferralCode"))
                userProfilePost.Add("ReferralCode", new string[] { "00000000" });
            else
            {
                if (string.IsNullOrEmpty(userProfilePost["ReferralCode"][0]))
                    userProfilePost["ReferralCode"] = new string[] { "00000000" };
            }

            foreach (string item in ConstantConfig.ATTRIBUTES_USER_PROFILE)
            {
                if (userProfilePost.ContainsKey(item))
                {
                    if (item == "ReferralCode")
                    {
                        var parentInfo = OAuth2_UserGetByUserName(realms, userProfilePost["ReferralCode"][0]);
                        if (parentInfo != null)
                        {
                            Dictionary<string, string[]> dictAttributes = JsonConvert.DeserializeObject<IDictionary<string, string[]>>(parentInfo["attributes"].ToString()) as dynamic;
                            if (dictAttributes.ContainsKey("HierarchyPath"))
                            {
                                string hierarchyPath = dictAttributes["HierarchyPath"][0] + "/" + userProfilePost["ReferralCode"][0];
                                userProfile["HierarchyPath"] = new string[] { hierarchyPath };
                                if (!userProfile.ContainsKey("ReferralCode"))
                                    userProfile.Add("ReferralCode", new string[] { userProfilePost["ReferralCode"][0] });
                                else
                                    userProfile["ReferralCode"] = new string[] { userProfilePost["ReferralCode"][0] };
                            }
                            else
                            {
                                userProfile.Add("HierarchyPath", new string[] { userProfilePost["ReferralCode"][0] });
                                userProfile["ReferralCode"] = new string[] { userProfilePost["ReferralCode"][0] };
                            }
                        }
                        else
                        {
                            userProfile.Add("HierarchyPath", new string[] { userProfilePost["ReferralCode"][0] });
                            userProfile["ReferralCode"] = new string[] { userProfilePost["ReferralCode"][0] };
                        }
                    }
                    else
                    {
                        if (!userProfile.ContainsKey(item))
                            userProfile.Add(item, userProfilePost[item]);
                    }
                }
                else
                {
                    if (item == "Code")
                    {
                        if (!userProfile.ContainsKey(item))
                            userProfile.Add(item, new string[] { info.username });
                    }
                    else
                    {
                        if (!userProfile.ContainsKey(item))
                            userProfile.Add(item, new string[] { "#" });
                    }
                }
            }
            info.attributes = userProfile;
            //---------------------------------------------------------------------------------------------------------------           
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.POST(info,
                BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users", realms),
                tokenInfo.access_token,string.Empty);
            if (result != null && result["errorMessage"] != null)
                return result["errorMessage"];
            return string.Empty;

        }
        public string OAuth2_UserUpdate(string realms, UserInfo info)
        {           
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            //---------------------------------------------------------------------------------------------------------------
            var resultGetInfo = HttpClientBase.GET_TO_INFO(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}", realms, info.id),
                tokenInfo.access_token,string.Empty);
            if (resultGetInfo != null)
            {
                Dictionary<string, string[]> userProfileCurent = JsonConvert.DeserializeObject<IDictionary<string, string[]>>(resultGetInfo["attributes"].ToString()) as dynamic;
                Dictionary<string, string[]> userProfile = new Dictionary<string, string[]>();
                foreach (string item in ConstantConfig.ATTRIBUTES_USER_PROFILE)
                {
                    if (info.attributes != null)
                    {
                        if (info.attributes.ContainsKey(item))
                            userProfile.Add(item, info.attributes[item]);
                        else
                        {
                            if (item == "Code")
                                userProfile.Add(item, new string[] { resultGetInfo["username"].ToString() });
                            else if (item == "ReferralCode")
                            {
                                if (userProfileCurent.ContainsKey("ReferralCode"))
                                    userProfile.Add("ReferralCode", userProfileCurent["ReferralCode"]);
                                else
                                    userProfile.Add("ReferralCode", new string[] { "00000000" });
                            }
                            else if (item == "HierarchyPath")
                            {
                                if (userProfileCurent.ContainsKey("HierarchyPath"))
                                    userProfile.Add("HierarchyPath", userProfileCurent["HierarchyPath"]);
                                else
                                    userProfile.Add("HierarchyPath", new string[] { "00000000" });
                            }
                            else
                                userProfile.Add(item, new string[] { "#" });
                        }
                    }
                    else
                    {
                        if (item == "Code")
                            userProfile.Add(item, new string[] { resultGetInfo["username"].ToString() });
                        else if (item == "ReferralCode")
                        {
                            if (userProfileCurent.ContainsKey("ReferralCode"))
                                userProfile.Add("ReferralCode", userProfileCurent["ReferralCode"]);
                            else
                                userProfile.Add("ReferralCode", new string[] { "00000000" });
                        }
                        else if (item == "HierarchyPath")
                        {
                            if (userProfileCurent.ContainsKey("HierarchyPath"))
                                userProfile.Add("HierarchyPath", userProfileCurent["HierarchyPath"]);
                            else
                                userProfile.Add("HierarchyPath", new string[] { "00000000" });
                        }
                        else
                            userProfile.Add(item, new string[] { "#" });
                    }

                }
                info.attributes = userProfile;



            }
            //---------------------------------------------------------------------------------------------------------------
            var result = HttpClientBase.PUT(info,
                BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}", realms, info.id),
                tokenInfo.access_token,string.Empty);
            if (result != null && result["errorMessage"] != null)
                return result["errorMessage"];
            return string.Empty;
        }
        public string OAuth2_UserDelete(string realms, string id)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.DELETE(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}", realms, id),
                tokenInfo.access_token,string.Empty);
            if (result != null && result["errorMessage"] != null)
                return result["errorMessage"];
            return string.Empty;

        }
        public Dictionary<string, object> OAuth2_UserGetByAccessToken(string realms, string accessToken)
        {
            var result = HttpClientBase.GET_TO_INFO(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                    string.Format("/auth/realms/{0}/protocol/openid-connect/userinfo", realms), accessToken,string.Empty);
            if (result.ContainsKey("error"))
                return null;
            return result;
        }
        public Dictionary<string, object> OAuth2_UserGetById(string realms, string id)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.GET_TO_INFO(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}", realms, id),
                tokenInfo.access_token,string.Empty);
            return result;

        }
        public Dictionary<string, object> OAuth2_UserGetByUserName(string realms, string userName)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users?username={1}", realms, userName),
                tokenInfo.access_token,string.Empty);
            return result.Count > 0 ? result[0] : null;

        }
        public Dictionary<string, object> OAuth2_UserGetByReferralCode(string realms, string referralCode)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users?username={1}", realms, Encrypt.DecryptText(referralCode)),
                tokenInfo.access_token,string.Empty);
            return result.Count > 0 ? result[0] : null;

        }
        public List<Dictionary<string, object>> OAuth2_UserGetList(string realms)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users", realms),
                tokenInfo.access_token,string.Empty);
            return result;
        }
        public List<Dictionary<string, object>> OAuth2_GetAllWithPagination(string realms, string keyword, int pageIndex, int pageSize)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users?search={1}&first={2}&max={3}", realms, keyword, (pageIndex - 1) * pageSize, pageSize),
                tokenInfo.access_token,string.Empty);
            return result;
        }
        public List<Dictionary<string, object>> OAuth2_UserGetSession(string realms, string id)
        {
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}/sessions", realms, id),
                tokenInfo.access_token, string.Empty);
            return result;

        }
        public Dictionary<string, object> OAuth2_UserGetSession(string realms, string id, string sessionState)
        {
            string jsonJwt = OAuth2Provider.request
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
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(jsonJwt);
            var responseSessions = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}/sessions", realms, id),
                tokenInfo.access_token, string.Empty);
            if (responseSessions != null && responseSessions.Count > 0)
            {
                var sessions = responseSessions.Where(d => d.Keys.Contains("id") && d["id"].ToString() == sessionState).Select(d => d).ToList();
                if (sessions != null && sessions.Count > 0)
                    return sessions[0];
            }
            return null;

        }
        public List<RealmInfo> OAuth2_GetListRealms()
        {
            string jsonRealms = Config.CONFIGURATION_PRIVATE.AppSetting.OAuth2.ListRealms.ToString();
            var result = JsonConvert.DeserializeObject<List<RealmInfo>>(jsonRealms);
            return result;
        }
        public bool OAuth2_UserResetPassword(string realms, string id, CredentialsInfo info, ref string errorMessage)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.PUT(info,
                BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}/reset-password", realms, id),
                tokenInfo.access_token,string.Empty);


            if (result != null && (result["errorMessage"] != null || result["error"] != null))
            {
                errorMessage = result["error"].ToString();
                return false;
            }    
                
            return true;

        }
        #endregion
        #region Utility
        public string OAuth2_GetHierarchyPath(string realms, string referralCode)
        {
            if (string.IsNullOrEmpty(referralCode))
                return "00000000";
            else
            {
                var parentInfo = OAuth2_UserGetByUserName(realms, referralCode);
                if (parentInfo != null)
                {
                    Dictionary<string, string[]> dictAttributes = JsonConvert.DeserializeObject<IDictionary<string, string[]>>(parentInfo["attributes"].ToString()) as dynamic;
                    if (dictAttributes.ContainsKey("HierarchyPath"))
                        return dictAttributes["HierarchyPath"][0] + "/" + referralCode;
                }
                return "00000000";
            }
        }
        private string OAuth2_GetRandomUserName(string realms)
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "123456789";
            Random r = new Random();
            for (int j = 0; j <= 8; j++)
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            var checkUserName = OAuth2_UserGetByUserName(realms, randomText.ToString());
            if (checkUserName == null)
                return randomText.ToString();
            else
                return string.Empty;
        }
        #endregion
    }
}
