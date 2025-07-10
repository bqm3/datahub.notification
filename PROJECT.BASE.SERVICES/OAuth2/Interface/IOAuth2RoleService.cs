using PROJECT.BASE.ENTITY;
using System.Collections.Generic;

namespace PROJECT.BASE.SERVICES
{
    public interface IOAuth2RoleService
    {
        bool OAuth2_RoleCreate(string realms, RoleInfo info);
        bool OAuth2_RoleUpdate(string realms, RoleInfo info);
        bool OAuth2_RoleDelete(string realms, string id);
        Dictionary<string, object> OAuth2_RoleGetById(string realms, string id);
        bool OAuth2_UserMappings(string realms, string userId, List<Dictionary<string, object>> roleInfos);
        bool OAuth2_RemoveUserMappings(string realms, string userId, List<Dictionary<string, object>> roleInfos);
        bool OAuth2_AttributesMappings(string realms, string roleId, Dictionary<string, object> dictData);
        bool OAuth2_RemoveAttributesMappings(string realms, string roleId, Dictionary<string, object> dictData);
        public List<Dictionary<string, object>> OAuth2_GetUserInRole(string realms, string roleName);
        List<Dictionary<string, object>> OAuth2_GetListRoles(string realms, string keyword, int pageIndex, int pageSize);
        List<Dictionary<string, object>> OAuth2_RealmAccessRoles(string realms, string userId);
        List<string> OAuth2_RolesAccessAction(string realms, List<string> listRoles);
        List<string> OAuth2_RolesAccessFunction(string realms, List<string> listRoles);

    }
}
