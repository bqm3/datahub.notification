using Lib.Json;
using Lib.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Lib.Middleware;
using PROJECT.BASE.ENTITY;
using PROJECT.BASE.SERVICES;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using System.Text;
using PROJECT.BASE.DAO;
using PROJECT.BASE.CORE;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace microservice.identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IOAuth2Service _IOAuth2Service;
        private readonly ILogger _logger;
        public IdentityController(IOAuth2Service IOAuth2Service, ILogger<IdentityController> logger)
        {
            _IOAuth2Service = IOAuth2Service;
            _logger = logger;
        }
        [HttpPost("Login")]
        public async Task<ActionResult> OAuth2_UserLogin(LoginInfo info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.AppCode) == true || string.IsNullOrEmpty(info.UserName) == true
                    || string.IsNullOrEmpty(info.PassWord) == true || string.IsNullOrEmpty(info.Realms) == true
                    || string.IsNullOrEmpty(info.ClientID) == true)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        TotalRecords = 0
                    });
                var result = _IOAuth2Service.OAuth2_UserLogin(info);
                if (result != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var decodedValue = handler.ReadJwtToken(result.access_token);
                    //var Claims = decodedValue.Claims.ToString();
                    string[] stringSeparators = new string[] { "}.{" };
                    string[] firstNames = decodedValue.ToString().Split(stringSeparators, StringSplitOptions.None);
                    var userID = JSON.Parse("{" + firstNames[1])["sub"].Value;
                    var session_state = JSON.Parse("{" + firstNames[1])["session_state"].Value;
                    var authTime = JSON.Parse("{" + firstNames[1])["iat"].Value;
                    var expTime = JSON.Parse("{" + firstNames[1])["exp"].Value;
                    //--------------------------------------------------------------------------------------------------------------------------
                    var dictDataInfo = _IOAuth2Service.OAuth2_UserGetByUserName(info.Realms, info.UserName);
                    //$"{realms}#{userId}#{sessionState}#{clientId}#{authTime}"
                    string clientIP = Request.Headers.ContainsKey("cf-connecting-ip") ? Request.Headers["cf-connecting-ip"].ToString() : string.Empty;
                    string ipCountry = Request.Headers.ContainsKey("cf-ipcountry") ? Request.Headers["cf-ipcountry"].ToString() : string.Empty;
                    string userAgent = Request.Headers.ContainsKey("User-Agent") ? Request.Headers["User-Agent"].ToString() : string.Empty;
                    string hashUserAgent = Encrypt.ComputeMd5Hash(Encoding.UTF8.GetBytes(userAgent));
                    //var byteUserAgent = Encoding.UTF8.GetBytes(userAgent);
                    var resultToken = dictDataInfo.ContainsKey("id") ? JwtBuilder.GetToken($"{info.Realms}#{dictDataInfo["id"]}#{session_state}#{info.AppCode}#{authTime}#{expTime}#{hashUserAgent}") : string.Empty;
                    result.token_api = resultToken;
                    DataObjectFactory.GetInstanceLOG_LOGIN().Insert<LOG_LOGINInfo>(new LOG_LOGINInfo
                    {
                        CODE = Guid.NewGuid().ToString(),
                        AUTH_TIME = long.Parse(authTime),
                        EXP_TIME = long.Parse(expTime),
                        CLIENT_ID = info.AppCode,
                        IP = clientIP,
                        COUNTRY = ipCountry,
                        USER_AGENT = userAgent,
                        USER_ID = userID,
                        REALMS = info.Realms,
                        USER_NAME = info.UserName,
                        CDATE = Utility.ConvertToUnixTime(DateTime.Now),
                        CUSER = Config.CONFIGURATION_PRIVATE.Infomation.Service
                    });
                }
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    TotalRecords = result != null ? 1 : 0,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult> OAuth2_RefreshToken(RefreshTokenInfo info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.AppCode) == true || string.IsNullOrEmpty(info.ClientID) == true
                    || string.IsNullOrEmpty(info.Realms) == true || string.IsNullOrEmpty(info.RefreshToken) == true)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        TotalRecords = 0
                    });
                var result = _IOAuth2Service.OAuth2_RefreshToken(info);
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    TotalRecords = result != null ? 1 : 0,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }

        [HttpGet("Logout/{realms}/{id}/{authTime}")]
        public async Task<ActionResult> OAuth2_UserLogout(string realms, string id, long authTime)
        {
            try
            {
                if (string.IsNullOrEmpty(realms) == true || string.IsNullOrEmpty(id) == true || authTime < 0)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE
                    });
                var result = _IOAuth2Service.OAuth2_UserLogout(realms, id, authTime);
                return Ok(new
                {
                    ResultCode = string.IsNullOrEmpty(result) ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = string.IsNullOrEmpty(result) ? Constant.MESSAGE_SUCCESS : result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpPost("Create")]
        public async Task<ActionResult> OAuth2_UserCreate(PostOAuth2Info<UserPostInfo> info)
        {
            try
            {
                //if (!Valid.IsValidEmail(info.DataPost.email))
                //    return Ok(new
                //    {
                //        ResultCode = Constant.RETURN_CODE_WARNING,
                //        Message = Constant.MESSAGE_VALIDATE_EMAIL,
                //        ReturnValue = string.Empty
                //    };
                Dictionary<string, object> dictDataInfo = null;
                var result = _IOAuth2Service.OAuth2_UserCreate(info.Realms, info.DataPost);
                if (!string.IsNullOrEmpty(result))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_ERROR,
                        Message = Constant.MESSAGE_ERROR,
                        ReturnValue = result
                    });
                dictDataInfo = _IOAuth2Service.OAuth2_UserGetByUserName(info.Realms, info.DataPost.username);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_SUCCESS,
                    Message = Constant.MESSAGE_SUCCESS,
                    ReturnValue = dictDataInfo["id"].ToString()
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpPut("Update")]
        public async Task<ActionResult> OAuth2_UserUpdate(PostOAuth2Info<UserInfo> info)
        {
            try
            {
                if (!string.IsNullOrEmpty(info.DataPost.email) && !Valid.IsValidEmail(info.DataPost.email))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_VALIDATE_EMAIL,
                        ReturnValue = string.Empty
                    });
                if (string.IsNullOrEmpty(info.DataPost.username))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_VALIDATE_CONTENT,
                        ReturnValue = string.Empty
                    });
                var result = _IOAuth2Service.OAuth2_UserUpdate(info.Realms, info.DataPost);
                if (!string.IsNullOrEmpty(result))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_ERROR,
                        Message = Constant.MESSAGE_ERROR,
                        ReturnValue = result
                    });
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_SUCCESS,
                    Message = Constant.MESSAGE_SUCCESS,
                    ReturnValue = result.ToString()
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }
        }

        [HttpDelete("Delete/{realms}/{id}")]
        public async Task<ActionResult> OAuth2_UserDeletea(string realms, string id)
        {
            try
            {
                var result = _IOAuth2Service.OAuth2_UserDelete(realms, id);
                if (!string.IsNullOrEmpty(result))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_ERROR,
                        Message = Constant.MESSAGE_ERROR,
                        ReturnValue = result
                    });
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_SUCCESS,
                    Message = Constant.MESSAGE_SUCCESS,
                    ReturnValue = result.ToString()
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpPost("UserGetByAccessToken")]
        public async Task<ActionResult> OAuth2_UserGetByAccessToken(PostOAuth2Info<object> info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.Realms))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        TotalRecords = 0
                    });
                if (info.DataPost == null)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        TotalRecords = 0
                    });
                var result = _IOAuth2Service.OAuth2_UserGetByAccessToken(info.Realms, info.DataPost.ToString());
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_AUT_ERROR,
                    TotalRecords = 1,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpGet("GetSession/{realms}/{id}")]
        public async Task<ActionResult> OAuth2_UserGetSession(string realms, string id)
        {
            try
            {
                var result = _IOAuth2Service.OAuth2_UserGetSession(realms, id);
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    TotalRecords = result != null ? result.Count : 0,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpGet("GetById/{realms}/{id}")]
        public async Task<ActionResult> OAuth2_UserGetById(string realms, string id)
        {
            try
            {
                var result = _IOAuth2Service.OAuth2_UserGetById(realms, id);
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    TotalRecords = 1,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpGet("GetByUserName/{realms}/{userName}")]
        public async Task<ActionResult> OAuth2_UserGetByUserName(string realms, string userName)
        {
            try
            {
                var result = _IOAuth2Service.OAuth2_UserGetByUserName(realms, userName);
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    TotalRecords = 1,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }
        }
        [HttpGet("GetByReferralCode/{realms}/{referralCode}")]
        public async Task<ActionResult> OAuth2_UserGetByReferralCode(string realms, string referralCode)
        {
            try
            {
                var result = _IOAuth2Service.OAuth2_UserGetByReferralCode(realms, referralCode);
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    TotalRecords = 1,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }
        }
        [HttpGet("GetList/{realms}")]
        public async Task<ActionResult> OAuth2_UserGetList(string realms)
        {
            try
            {
                var result = _IOAuth2Service.OAuth2_UserGetList(realms);
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    TotalRecords = result != null ? result.Count : 0,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpGet("GetAllWithPagination/{realms}/{keyword}/{pageIndex}/{pageSize}")]
        public async Task<ActionResult> OAuth2_GetAllWithPagination(string realms, string keyword, string pageIndex, string pageSize)
        {
            try
            {
                keyword = keyword == "-1" ? string.Empty : keyword;
                var result = _IOAuth2Service.OAuth2_GetAllWithPagination(realms, keyword, int.Parse(pageIndex), int.Parse(pageSize));
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    TotalRecords = result != null ? result.Count : 0,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpGet("GetListRealms")]
        public async Task<ActionResult> OAuth2_GetListRealms()
        {
            try
            {
                var result = _IOAuth2Service.OAuth2_GetListRealms();
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    TotalRecords = result != null ? result.Count : 0,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpPut("ResetPassword")]
        public async Task<ActionResult> OAuth2_UserResetPassword(ResetPassWordInfo info)
        {
            try
            {
                info.DataPost.temporary = false;
                //info.DataPost.type = "password";
                //info.DataPost.value = "123456789";
                string errorMessage = "";
                var result = _IOAuth2Service.OAuth2_UserResetPassword(info.Realms, info.Id, info.DataPost, ref errorMessage);
                if (result == false)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_ERROR,
                        Message = errorMessage,//Constant.MESSAGE_SUCCESS,
                        ReturnValue = result.ToString()
                    });
                else
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_SUCCESS,
                        Message = Constant.MESSAGE_SUCCESS,
                        ReturnValue = result.ToString()
                    });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpPost("ChangePassword")]
        public async Task<ActionResult> OAuth2_UserChangePassword(ChangePasswordInfo info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.UserName)
                    || string.IsNullOrEmpty(info.CurentPassWord)
                    || string.IsNullOrEmpty(info.NewPassWord)
                    || string.IsNullOrEmpty(info.ConfirmPassWord)
                    || string.IsNullOrEmpty(info.Realms)
                    || string.IsNullOrEmpty(info.ClientID))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        ReturnValue = "false"
                    });
                if (info.NewPassWord != info.ConfirmPassWord)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = "confirmPasswordNotTheSame!",//Constant.MESSAGE_VALIDATE_PASSWORD,
                        ReturnValue = "false"
                    });
                var resultLogin = _IOAuth2Service.OAuth2_UserLogin(new LoginInfo
                {
                    UserName = info.UserName,
                    PassWord = info.CurentPassWord,
                    ClientID = info.ClientID,
                    Realms = info.Realms
                });

                if (resultLogin != null && !string.IsNullOrEmpty(resultLogin.access_token))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var decodedValue = handler.ReadJwtToken(resultLogin.access_token);
                    var Claims = decodedValue.Claims.ToString();
                    string[] stringSeparators = new string[] { "}.{" };
                    string[] firstNames = decodedValue.ToString().Split(stringSeparators, StringSplitOptions.None);
                    string jsonJwt = "{" + firstNames[1];
                    var userId = JSON.Parse("{" + firstNames[1])["sub"].Value;
                    var errorMessage = "";
                    var result = _IOAuth2Service.OAuth2_UserResetPassword(info.Realms, userId,
                        new CredentialsInfo
                        {
                            temporary = false,
                            type = "password",
                            value = info.NewPassWord
                        }, ref errorMessage);

                    if (result == false)
                        return Ok(new
                        {
                            ResultCode = Constant.RETURN_CODE_ERROR,
                            Message = errorMessage,//Constant.MESSAGE_SUCCESS,
                            ReturnValue = result.ToString()
                        });
                    else
                        return Ok(new
                        {
                            ResultCode = Constant.RETURN_CODE_SUCCESS,
                            Message = Constant.MESSAGE_SUCCESS,
                            ReturnValue = result.ToString()
                        });
                }

                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_WARNING,
                    Message = "unauthenticated",//Constant.MESSAGE_VALIDATE_ACCOUNT,
                    ReturnValue = "false"
                });
            }
            catch (System.Net.WebException ex)
            {
                _logger.LogError(ex, ex.Message);
                if (((System.Net.HttpWebResponse)ex.Response).StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_ERROR,
                        Message = "unauthenticated"
                    });
                else
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_ERROR,
                        Message = "undetermined"
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }
        [HttpGet("GetHierarchyPath")]
        public async Task<ActionResult> OAuth2_GetHierarchyPath(string realms, string referralCode)
        {
            try
            {
                var result = _IOAuth2Service.OAuth2_GetHierarchyPath(realms, referralCode);
                return Ok(new
                {
                    ResultCode = !string.IsNullOrEmpty(result) ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = !string.IsNullOrEmpty(result) ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    ReturnValue = result
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    ex.Message
                });
            }

        }


    }
}
