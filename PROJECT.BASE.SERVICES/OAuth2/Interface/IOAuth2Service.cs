using PROJECT.BASE.ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT.BASE.SERVICES
{
    public interface IOAuth2Service
    {
        TokenInfo OAuth2_UserLogin(LoginInfo info);

        TokenInfo OAuth2_RefreshToken(RefreshTokenInfo info);
        string OAuth2_UserCreate(string realms, UserPostInfo info);
        string OAuth2_UserUpdate(string realms, UserInfo info);
        string OAuth2_UserDelete(string realms, string id);
        Dictionary<string, object> OAuth2_UserGetByAccessToken(string realms, string accessToken);
        Dictionary<string, object> OAuth2_UserGetById(string realms, string id);
        Dictionary<string, object> OAuth2_UserGetByUserName(string realms, string userName);
        Dictionary<string, object> OAuth2_UserGetByReferralCode(string realms, string referralCode);
        List<Dictionary<string, object>> OAuth2_UserGetList(string realms);
        List<Dictionary<string, object>> OAuth2_GetAllWithPagination(string realms, string keyword, int pageIndex, int pageSize);
        List<Dictionary<string, object>> OAuth2_UserGetSession(string realms, string id);
        Dictionary<string, object> OAuth2_UserGetSession(string realms, string id, string sessionState);
        List<RealmInfo> OAuth2_GetListRealms();
        bool OAuth2_UserResetPassword(string realms, string id, CredentialsInfo info, ref string errorMessage);
        #region Utility
        string OAuth2_GetHierarchyPath(string realms, string referralCode);
        string OAuth2_UserLogout(string realms,string userID, long authTime);
        #endregion

    }
}
