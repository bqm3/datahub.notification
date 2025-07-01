using Lib.Json;
using Lib.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Lib.Middleware;
using Newtonsoft.Json;
using PROJECT.BASE.CORE;
using PROJECT.BASE.ENTITY;
using PROJECT.BASE.SERVICES;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using PROJECT.BASE.DAO;
using Lib.Consul.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using MySqlX.XDevAPI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace microservice.identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenAPIController : ControllerBase
    {
        private readonly IOAuth2Service _IOAuth2Service;
        private readonly ILogger _logger;
        public TokenAPIController(IOAuth2Service IOAuth2Service, ILogger<TokenAPIController> logger)
        {
            _IOAuth2Service = IOAuth2Service;
            _logger = logger;
        }
        [HttpPost("GetToken")]
        public async Task<ActionResult> OAuth2_GetToken(LoginInfo info)
        {
            try
            {

                var tokenInfo = _IOAuth2Service.OAuth2_UserLogin(info);
                if (tokenInfo != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var decodedValue = handler.ReadJwtToken(tokenInfo.access_token);
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
                    var resultToken = dictDataInfo.ContainsKey("id") ? JwtBuilder.GetToken($"{info.Realms}#{dictDataInfo["id"]}#{session_state}#{info.AppCode}#{authTime}#{expTime}#{hashUserAgent}") : string.Empty;
                    if (info.UserName == "token")
                        return Ok(new
                        {
                            ResultCode = Constant.RETURN_CODE_SUCCESS,
                            Message = Constant.MESSAGE_SUCCESS,
                            ReturnValue = resultToken
                        });
                    else
                    {
                        var result = DataObjectFactory.GetInstanceLOG_LOGIN().Insert<LOG_LOGINInfo>(new LOG_LOGINInfo
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
                        return Ok(new
                        {
                            ResultCode = result ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                            Message = result ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                            ReturnValue = resultToken
                        });
                    }
                }
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    Message = Constant.MESSAGE_AUT_ERROR,
                    ReturnValue = string.Empty
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : string.Empty,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                });
            }

        }
        [HttpGet("GetToken/{ma_don_vi}")]
        public async Task<ActionResult> OAuth2_GetToken(string ma_don_vi)
        {
            try
            {
                var resultToken = TokenJWT.GenerateJwtToken(new Dictionary<string, string>()
                {
                    { "CORP_CODE","VIX001" },
                    { "EXPIRED","2025-12-30" },
                    { "CORP_NAME","CÔNG TY CỔ PHẦN CHỨNG KHOÁN VIX" },
                    { "EMAIL","vix@gmail.com" },
                    { "CORP_PHONE","0988888888" },
                    { "ADDRESS","Tầng 22, số 52 Phố Lê Đại Hành, Phường Lê Đại Hành, Quận Hai Bà Trưng, Thành phố Hà Nội" },
                    { "GROUP_CODE","PRT" }
                });
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_SUCCESS,
                    Message = Constant.MESSAGE_SUCCESS,
                    Data = resultToken
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : string.Empty,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                });
            }

        }
        [HttpPost("ValidateToken")]
        public async Task<ActionResult> OAuth2_Validate(string token)
        {
            try
            {
                var tokentValue = JwtBuilder.ValidateToken(token);
                if (string.IsNullOrEmpty(tokentValue))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_ERROR,
                        Message = Constant.MESSAGE_AUT_ERROR_TOKEN
                    });
                var realms = tokentValue.Split('#')[0];
                var userId = tokentValue.Split('#')[1];
                var sessionState = tokentValue.Split('#')[2];
                var result = _IOAuth2Service.OAuth2_UserGetSession(realms, userId, sessionState);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_SUCCESS,
                    Message = Constant.MESSAGE_SUCCESS,
                    Data = result
                });


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_ERROR,
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : string.Empty,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                });
            }

        }


    }
}
