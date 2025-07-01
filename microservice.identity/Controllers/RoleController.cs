using Lib.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Lib.Middleware;
using Newtonsoft.Json;
using PROJECT.BASE.ENTITY;
using PROJECT.BASE.SERVICES;
using Lib.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace microservice.identity.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IOAuth2RoleService _IOAuth2RoleService;
        private readonly ILogger _logger;
        public RoleController(IOAuth2RoleService IOAuth2RoleService, ILogger<RoleController> logger)
        {
            _IOAuth2RoleService = IOAuth2RoleService;
            _logger = logger;
        }
        [HttpPost("Create")]
        public async Task<ActionResult> OAuth2_RoleCreate(PostOAuth2Info<RoleInfo> info)
        {
            try
            {                
                var result = _IOAuth2RoleService.OAuth2_RoleCreate(info.Realms, info.DataPost);
                return Ok(new
                {
                    ResultCode = result == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result == true ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                    ReturnValue = result.ToString()
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
        [HttpPut("Update")]
        public async Task<ActionResult> OAuth2_RoleUpdate(PostOAuth2Info<RoleInfo> info)
        {
            try
            {
                var curentInfo = _IOAuth2RoleService.OAuth2_RoleGetById(info.Realms, info.DataPost.id);
                if (curentInfo != null)
                {
                    Dictionary<string, string[]> dictAttributes = JsonConvert.DeserializeObject<IDictionary<string, string[]>>(curentInfo["attributes"].ToString()) as dynamic;
                    info.DataPost.attributes = dictAttributes;
                    var result = _IOAuth2RoleService.OAuth2_RoleUpdate(info.Realms, info.DataPost);
                    return Ok(new
                    {
                        ResultCode = result == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                        Message = result == true ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                        ReturnValue = result.ToString()
                    });
                }
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_NOT_FOUND,
                    Message = Constant.MESSAGE_NOT_FOUND,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                    ReturnValue = "false"
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
        [HttpDelete("Delete/{realms}/{id}")]
        public async Task<ActionResult> OAuth2_RoleDelete(string realms, string id)
        {
            try
            {
                var result = _IOAuth2RoleService.OAuth2_RoleDelete(realms, id);
                return Ok(new
                {
                    ResultCode = result == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result == true ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                    ReturnValue = result.ToString()
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
        [HttpGet("GetById/{realms}/{id}")]
        public async Task<ActionResult> OAuth2_RoleGetById(string realms, string id)
        {
            try
            {
                var result = _IOAuth2RoleService.OAuth2_RoleGetById(realms, id);
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
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
        [HttpPost("UserMappings")]
        public async Task<ActionResult> OAuth2_UserMappings(PostOAuth2ByID<Dictionary<string, object>> info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.Realms) || string.IsNullOrEmpty(info.Id))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                if (info.DataPost == null || info.DataPost.Count == 0)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)                        
                    });
                var result = _IOAuth2RoleService.OAuth2_UserMappings(info.Realms, info.Id, info.DataPost);
                return Ok(new
                {
                    ResultCode = result == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result == true ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                    ReturnValue = result.ToString()
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
        [HttpPost("UserMappings/Remove")]
        public async Task<ActionResult> OAuth2_RemoveUserMappings(PostOAuth2ByID<Dictionary<string, object>> info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.Realms) || string.IsNullOrEmpty(info.Id))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                if (info.DataPost == null || info.DataPost.Count == 0)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                var result = _IOAuth2RoleService.OAuth2_RemoveUserMappings(info.Realms, info.Id, info.DataPost);
                return Ok(new
                {
                    ResultCode = result == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result == true ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    ReturnValue = result.ToString()
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
        [HttpPost("ActionMappings")]
        public async Task<ActionResult> OAuth2_ActionMappings(PostOAuth2ByID<object> info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.Realms) || string.IsNullOrEmpty(info.Id))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                if (info.DataPost == null || info.DataPost.Count == 0)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                string actionCode = string.Empty;
                foreach (var item in info.DataPost)
                    actionCode += item + ",";
                var dictAttributes = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(actionCode))
                    dictAttributes.Add("ACTIONS", new string[] { actionCode.Substring(0, actionCode.Length - 1) });
                var result = _IOAuth2RoleService.OAuth2_AttributesMappings(info.Realms, info.Id, dictAttributes);
                return Ok(new
                {
                    ResultCode = result == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result == true ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                    ReturnValue = result.ToString()
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
        [HttpPost("ActionMappings/Remove")]
        public async Task<ActionResult> OAuth2_RemoveActionMappings(PostOAuth2ByID<object> info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.Realms) || string.IsNullOrEmpty(info.Id))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                if (info.DataPost == null || info.DataPost.Count == 0)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                string actionCode = string.Empty;
                foreach (var item in info.DataPost)
                    actionCode += item + ",";
                var dictAttributes = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(actionCode))
                    dictAttributes.Add("ACTIONS", new string[] { actionCode.Substring(0, actionCode.Length - 1) });
                var result = _IOAuth2RoleService.OAuth2_RemoveAttributesMappings(info.Realms, info.Id, dictAttributes);
                return Ok(new
                {
                    ResultCode = result == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result == true ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                    ReturnValue = result.ToString()
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
        [HttpPost("FunctionMappings")]
        public async Task<ActionResult> OAuth2_FunctionMappings(PostOAuth2ByID<object> info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.Realms) || string.IsNullOrEmpty(info.Id))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                if (info.DataPost == null || info.DataPost.Count == 0)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                string functionCode = string.Empty;
                foreach (var item in info.DataPost)
                    functionCode += item + ",";
                var dictAttributes = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(functionCode))
                    dictAttributes.Add("FUNCTIONS", new string[] { functionCode.Substring(0, functionCode.Length - 1) });
                var result = _IOAuth2RoleService.OAuth2_AttributesMappings(info.Realms, info.Id, dictAttributes);
                return Ok(new
                {
                    ResultCode = result == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result == true ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                    ReturnValue = result.ToString()
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
        [HttpPost("FunctionMappings/Remove")]
        public async Task<ActionResult> OAuth2_RemoveFunctionMappings(PostOAuth2ByID<object> info)
        {
            try
            {
                if (string.IsNullOrEmpty(info.Realms) || string.IsNullOrEmpty(info.Id))
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                if (info.DataPost == null || info.DataPost.Count == 0)
                    return Ok(new
                    {
                        ResultCode = Constant.RETURN_CODE_WARNING,
                        Message = Constant.MESSAGE_NOT_VALIDATE,
                        Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                    });
                string actionCode = string.Empty;
                foreach (var item in info.DataPost)
                    actionCode += item + ",";
                var dictAttributes = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(actionCode))
                    dictAttributes.Add("FUNCTIONS", new string[] { actionCode.Substring(0, actionCode.Length - 1) });
                var result = _IOAuth2RoleService.OAuth2_RemoveAttributesMappings(info.Realms, info.Id, dictAttributes);
                return Ok(new
                {
                    ResultCode = result == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_ERROR,
                    Message = result == true ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_ERROR,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                    ReturnValue = result.ToString()
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
        [HttpGet("GetUserInRole/{realms}/{roleName}")]
        public async Task<ActionResult> OAuth2_GetUserInRole(string realms,string roleName)
        {
            try
            {
                var result = _IOAuth2RoleService.OAuth2_GetUserInRole(realms, roleName);
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_NOT_FOUND,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_NOT_FOUND,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
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
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : string.Empty,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                });
            }

        }
        [HttpGet("GetListRoles/{realms}/{keyword}/{pageIndex}/{pageSize}")]
        public async Task<ActionResult> OAuth2_GetListRoles(string realms,string keyword, string pageIndex, string pageSize)
        {
            try
            {
                keyword = keyword == "-1" ? string.Empty : keyword;
                var result = _IOAuth2RoleService.OAuth2_GetListRoles(realms, keyword, int.Parse(pageIndex), int.Parse(pageSize));
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_NOT_FOUND,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_NOT_FOUND,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
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
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : string.Empty,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                });
            }

        }           
        [HttpGet("RealmAccessRoles/{realms}/{userId}")]
        public async Task<ActionResult> OAuth2_RealmAccessRoles(string realms, string userId)
        {
            try
            {                
                var result = _IOAuth2RoleService.OAuth2_RealmAccessRoles(realms, userId);
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_NOT_FOUND,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_NOT_FOUND,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
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
                    Message = !string.IsNullOrEmpty(ex.Message) ? ex.Message : string.Empty,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now)
                });
            }

        }
        [HttpPost("RolesAccessAction")]
        public async Task<ActionResult> OAuth2_RolesAccessAction(PostOAuth2ByID<object> info)
        {
            try
            {
                var result = _IOAuth2RoleService.OAuth2_RolesAccessAction(info.Realms, info.DataPost.Select(s => (string)s).ToList());
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_NOT_FOUND,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_NOT_FOUND,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),  
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
        [HttpPost("RolesAccessFunction")]
        public async Task<ActionResult> OAuth2_RolesAccessFunction(PostOAuth2ByID<object> info)
        {
            try
            {
                var result = _IOAuth2RoleService.OAuth2_RolesAccessAction(info.Realms, info.DataPost.Select(s => (string)s).ToList());
                return Ok(new
                {
                    ResultCode = result != null ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_NOT_FOUND,
                    Message = result != null ? Constant.MESSAGE_SUCCESS : Constant.MESSAGE_NOT_FOUND,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),                    
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
