using Lib.Consul.Configuration;
using Lib.Utility;
using Newtonsoft.Json;
using PROJECT.BASE.ENTITY;
using Lib.Setting;
using System.Collections.Generic;
using System.Linq;

namespace PROJECT.BASE.SERVICES
{
    public class OAuth2RoleService : IOAuth2RoleService
    {
        public OAuth2RoleService() { }

        #region OAuth2_Roles
        public bool OAuth2_RoleCreate(string realms, RoleInfo info)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.POST(info,
                BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/roles", realms),
                tokenInfo.access_token,string.Empty);
            if (result != null && result["errorMessage"] != null)
                return false;
            return true;

        }
        public bool OAuth2_RoleUpdate(string realms, RoleInfo info)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var payload = new Dictionary<string, object>
                {
                    {"id", info.id},
                    {"name", info.name},
                    {"description", info.name},
                    {"composite", false},
                    {"clientRole", false},
                    {"containerId", realms},
                    {"attributes", info.attributes}
                };
            var result = HttpClientBase.PUT(payload,
                BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/roles-by-id/{1}", realms, info.id),
                tokenInfo.access_token, string.Empty);
            if (result != null && result["errorMessage"] != null)
                return false;
            return true;

        }
        public bool OAuth2_RoleDelete(string realms, string id)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.DELETE(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/roles-by-id", realms, id),
                tokenInfo.access_token, string.Empty);
            if (result != null && result["errorMessage"] != null)
                return false;
            return true;

        }
        public Dictionary<string, object> OAuth2_RoleGetById(string realms, string id)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.GET_TO_INFO(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/roles-by-id/{1}", realms, id),
                tokenInfo.access_token, string.Empty);
            return result;

        }
        public bool OAuth2_UserMappings(string realms, string userId, List<Dictionary<string, object>> roleInfos)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.POST(roleInfos,
                BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}/role-mappings/realm", realms, userId),
                tokenInfo.access_token, string.Empty);
            if (result != null && result["errorMessage"] != null)
                return false;
            return true;

        }
        public bool OAuth2_RemoveUserMappings(string realms, string userId, List<Dictionary<string, object>> roleInfos)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.DELETE(roleInfos,
                BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}/role-mappings/realm", realms, userId),
                tokenInfo.access_token, string.Empty);
            if (result != null && result["errorMessage"] != null)
                return false;
            return true;

        }
        public bool OAuth2_AttributesMappings(string realms, string roleId, Dictionary<string, object> dictData)
        {
            var curentInfo = OAuth2_RoleGetById(realms, roleId);
            if (curentInfo != null)
            {
                Dictionary<string, string[]> dictAttributes = JsonConvert.DeserializeObject<IDictionary<string, string[]>>(curentInfo["attributes"].ToString()) as dynamic;
                if (dictData != null)
                {
                    if (dictData.ContainsKey("ACTIONS"))
                    {
                        if (!dictAttributes.ContainsKey("ACTIONS"))
                        {
                            string actionCode = string.Empty;
                            var listAcction = ((string[])dictData["ACTIONS"])[0];
                            foreach (var item in listAcction.Split(",").ToList())
                                actionCode += item + ",";
                            if (!string.IsNullOrEmpty(actionCode))
                                dictAttributes.Add("ACTIONS", new string[] { actionCode.Substring(0, actionCode.Length - 1) });
                        }
                        else
                        {
                            var listAcction = ((string[])dictData["ACTIONS"])[0].Split(",").ToList();
                            listAcction.AddRange(dictAttributes["ACTIONS"].ToString().Split(",").ToList());
                            string actionCode = string.Empty;
                            foreach (var item in listAcction.Distinct())
                                actionCode += item + ",";
                            if (!string.IsNullOrEmpty(actionCode))
                                dictAttributes["ACTIONS"] = new string[] { actionCode.Substring(0, actionCode.Length - 1) };
                        }
                    }
                    else if (dictData.ContainsKey("FUNCTIONS"))
                    {
                        if (!dictAttributes.ContainsKey("FUNCTIONS"))
                        {
                            string functionCode = string.Empty;
                            var listfunction = ((string[])dictData["FUNCTIONS"])[0];
                            foreach (var item in listfunction.Split(",").ToList())
                                functionCode += item + ",";
                            if (!string.IsNullOrEmpty(functionCode))
                                dictAttributes.Add("FUNCTIONS", new string[] { functionCode.Substring(0, functionCode.Length - 1) });
                        }
                        else
                        {
                            var listfunction = ((string[])dictData["FUNCTIONS"])[0].Split(",").ToList();
                            listfunction.AddRange(dictAttributes["FUNCTIONS"].ToString().Split(",").ToList());
                            string functionCode = string.Empty;
                            foreach (var item in listfunction.Distinct())
                                functionCode += item + ",";
                            if (!string.IsNullOrEmpty(functionCode))
                                dictAttributes["FUNCTIONS"] = new string[] { functionCode.Substring(0, functionCode.Length - 1) };
                        }
                    }
                    else
                    {
                        foreach (var pairKeyValue in dictData)
                        {
                            if (dictAttributes.ContainsKey(pairKeyValue.Key))
                                dictAttributes[pairKeyValue.Key] = new string[] { pairKeyValue.Value.ToString() };
                            else
                                dictAttributes.Add(pairKeyValue.Key, new string[] { pairKeyValue.Value.ToString() });
                        }
                    }
                }
                var roleInfo = new RoleInfo
                {
                    id = curentInfo["id"].ToString(),
                    name = curentInfo.ContainsKey("name") ? curentInfo["name"].ToString() : string.Empty,
                    description = curentInfo.ContainsKey("description") ? curentInfo["description"].ToString() : string.Empty,
                    composite = bool.Parse(curentInfo["composite"].ToString()),
                    clientRole = bool.Parse(curentInfo["clientRole"].ToString()),
                    containerId = curentInfo.ContainsKey("containerId") ? curentInfo["containerId"].ToString() : string.Empty,
                    attributes = dictAttributes
                };
                var result = OAuth2_RoleUpdate(realms, roleInfo);
                return result;
            }
            return false;

        }
        public bool OAuth2_RemoveAttributesMappings(string realms, string roleId, Dictionary<string, object> dictData)
        {
            var curentInfo = OAuth2_RoleGetById(realms, roleId);
            if (curentInfo != null)
            {
                Dictionary<string, string[]> dictAttributes = JsonConvert.DeserializeObject<IDictionary<string, string[]>>(curentInfo["attributes"].ToString()) as dynamic;
                if (dictData != null)
                {
                    if (dictData.ContainsKey("ACTIONS"))
                    {
                        if (dictAttributes.ContainsKey("ACTIONS"))
                        {
                            var listAcctionCurent = ((string[])dictAttributes["ACTIONS"])[0].ToString().Split(",").ToList();
                            foreach (var item in ((string[])dictData["ACTIONS"])[0].ToString().Split(",").ToList())
                                listAcctionCurent.Remove(item);
                            string actionCode = string.Empty;
                            foreach (var item in listAcctionCurent)
                                actionCode += item + ",";
                            if (!string.IsNullOrEmpty(actionCode))
                                dictAttributes["ACTIONS"] = new string[] { actionCode.Substring(0, actionCode.Length - 1) };
                        }

                    }
                    else if (dictData.ContainsKey("FUNCTIONS"))
                    {
                        if (dictAttributes.ContainsKey("FUNCTIONS"))
                        {
                            var listAcctionCurent = ((string[])dictAttributes["FUNCTIONS"])[0].ToString().Split(",").ToList();
                            foreach (var item in ((string[])dictData["FUNCTIONS"])[0].ToString().Split(",").ToList())
                                listAcctionCurent.Remove(item);
                            string functionCode = string.Empty;
                            foreach (var item in listAcctionCurent)
                                functionCode += item + ",";
                            if (!string.IsNullOrEmpty(functionCode))
                                dictAttributes["FUNCTIONS"] = new string[] { functionCode.Substring(0, functionCode.Length - 1) };
                        }
                    }
                    else
                    {
                        foreach (var pairKeyValue in dictData)
                        {
                            if (dictAttributes.ContainsKey(pairKeyValue.Key))
                                dictAttributes.Remove(pairKeyValue.Key);
                        }
                    }
                }
                var roleInfo = new RoleInfo
                {
                    id = curentInfo["id"].ToString(),
                    name = curentInfo.ContainsKey("name") ? curentInfo["name"].ToString() : string.Empty,
                    description = curentInfo.ContainsKey("description") ? curentInfo["description"].ToString() : string.Empty,
                    composite = bool.Parse(curentInfo["composite"].ToString()),
                    clientRole = bool.Parse(curentInfo["clientRole"].ToString()),
                    containerId = curentInfo.ContainsKey("containerId") ? curentInfo["containerId"].ToString() : string.Empty,
                    attributes = dictAttributes
                };
                var result = OAuth2_RoleUpdate(realms, roleInfo);
                return result;
            }
            return false;

        }
        public List<Dictionary<string, object>> OAuth2_GetUserInRole(string realms, string roleName)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/roles/{1}/users?first=0&max=" + int.MaxValue, realms, roleName),
                tokenInfo.access_token, string.Empty);
            return result;

        }       
        public List<Dictionary<string, object>> OAuth2_GetListRoles(string realms, string keyword, int pageIndex, int pageSize)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            string url = !string.IsNullOrEmpty(keyword) ? string.Format("/auth/admin/realms/{0}/roles?search={1}&first={2}&max={3}", realms, keyword, (pageIndex - 1) * pageSize, pageSize) : string.Format("/auth/admin/realms/{0}/roles?first=0&max=1000", realms);
            var result = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) + url, tokenInfo.access_token, string.Empty);
            foreach (var item in new string[] { "offline_access", "uma_authorization" })
                result.RemoveAll(dict => dict.Any(kv => kv.Key == "name" && (kv.Value as string) == item));
            return result;

        }
        public List<Dictionary<string, object>> OAuth2_RealmAccessRoles(string realms, string userId)
        {            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            var result = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                string.Format("/auth/admin/realms/{0}/users/{1}/role-mappings/realm", realms, userId),
                tokenInfo.access_token, string.Empty);
            return result;
        }
        public List<string> OAuth2_RolesAccessAction(string realms, List<string> listRoles)
        {
            List<string> result = new List<string>();            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            foreach (var roleName in listRoles)
            {
                var listData = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url)
                    + string.Format("/auth/admin/realms/{0}/roles?first=0&max=1000&search={1}", realms, roleName)
                    , tokenInfo.access_token, string.Empty);
                if (listData != null && listData.Count > 0)
                {
                    var roleInfo = HttpClientBase.GET_TO_INFO(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                    string.Format("/auth/admin/realms/{0}/roles-by-id/{1}", realms, listData[0]["id"]),
                    tokenInfo.access_token, string.Empty);
                    Dictionary<string, string[]> dictAttributes = JsonConvert.DeserializeObject<IDictionary<string, string[]>>(roleInfo["attributes"].ToString()) as dynamic;
                    if (dictAttributes.ContainsKey("ACTIONS"))
                        result = dictAttributes["ACTIONS"].ToString().Split(",").ToList();

                }
            }
            return result.Count > 0 ? result : null;
        }
        public List<string> OAuth2_RolesAccessFunction(string realms, List<string> listRoles)
        {
            List<string> result = new List<string>();            
            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(OAuth2.OAuth2_GetJWT());
            foreach (var roleName in listRoles)
            {
                var listData = HttpClientBase.GET_TO_LIST(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url)
                    + string.Format("/auth/admin/realms/{0}/roles?first=0&max=1000&search={1}", realms, roleName)
                    , tokenInfo.access_token, string.Empty);
                if (listData != null && listData.Count > 0)
                {
                    var roleInfo = HttpClientBase.GET_TO_INFO(BaseSettings.Get(ConstantConsul.UrlConfig_OAuth2_Url) +
                    string.Format("/auth/admin/realms/{0}/roles-by-id/{1}", realms, listData[0]["id"]),
                    tokenInfo.access_token, string.Empty);
                    Dictionary<string, string[]> dictAttributes = JsonConvert.DeserializeObject<IDictionary<string, string[]>>(roleInfo["attributes"].ToString()) as dynamic;
                    if (dictAttributes.ContainsKey("FUNCTIONS"))
                        result = dictAttributes["FUNCTIONS"].ToString().Split(",").ToList();

                }
            }
            return result.Count > 0 ? result : null;
        }

        #endregion
    }
}
