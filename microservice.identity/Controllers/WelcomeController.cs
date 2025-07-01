using System.Net;
using System.Text.RegularExpressions;
using Lib.Consul.Configuration;
using Lib.Utility;
using Microsoft.AspNetCore.Mvc;
using Lib.Setting;
using System.Reflection;

namespace microservice.identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WelcomeController : ControllerBase
    {
        private readonly ILogger<WelcomeController> _logger;
        public WelcomeController(ILogger<WelcomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet("GetInfo")]
        public async Task<ActionResult> GetInfo()
        {
            try
            {
                string hostName = Dns.GetHostName();
                IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
                List<string> listIpAddress = new List<string>();
                foreach (IPAddress ipAddres in hostAddresses)
                {
                    Match match = Regex.Match(ipAddres.ToString(), @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                    if (match.Success)
                        listIpAddress.Add(ipAddres.ToString());
                }
                return Ok(new
                {
                    ResultCode = Constant.RETURN_CODE_SUCCESS,
                    Message = Constant.MESSAGE_SUCCESS,
                    Timestamp = Utility.ConvertToUnixTime(DateTime.Now),
                    ServiceName = Config.CONFIGURATION_PRIVATE.Infomation.Service,
                    GlobalConfigMap = Config.CONFIGURATION_GLOBAL.Infomation.SettingName,
                    PrivateConfigMap = Config.CONFIGURATION_PRIVATE.Infomation.SettingName,
                    IPAddress = (listIpAddress.Count > 0 ? listIpAddress[listIpAddress.Count - 1] : string.Empty)
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
