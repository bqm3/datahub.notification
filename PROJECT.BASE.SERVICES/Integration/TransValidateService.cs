using Lib.Setting;
using Lib.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using PROJECT.BASE.CORE;
using PROJECT.BASE.DAO;
using PROJECT.BASE.ENTITY;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
//using static Google.Apis.Requests.BatchRequest;

namespace PROJECT.BASE.SERVICES
{
    public class TransValidateService
    {
        public TransValidateService() { }
        /// <summary>
        /// STEP_2 - Kiểm tra tính hợp lệ gia dịch
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static async Task<bool> ValidateTrans(IntegrationRequest request, string creator)
        {
            try
            {               
                string pathXSD = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/configuration/xsd";
                var xmlDocumet = StringConvert.StringToXml(StringConvert.DecodeStringFromBase64(request.Body.Content));
                var validXSD = Valid.ValidateXML(xmlDocumet, $"{pathXSD}/{request.Header.Tran_Code}.xsd");
                if (validXSD != null && validXSD.Count > 0)
                {                    
                    var message = string.Empty;
                    foreach (var itemx in validXSD)
                    {
                        message += $"{Environment.NewLine}-{itemx}";
                    }
                    if(!string.IsNullOrEmpty(message))
                    {
                        message = $"{Constant.STEP_2}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_2]}:{message}";
                        IntegrationService.ITG_MSGO_Activitie(request, short.Parse(Constant.RETURN_CODE_WARNING), message, creator);
                        return false;
                    }
                }
                IntegrationService.ITG_MSGO_Activitie(request, short.Parse(Constant.RETURN_CODE_SUCCESS), $"{Constant.STEP_2}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_2]}", creator);
                return true;

            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                var message = $"{Constant.STEP_2}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_2]}:[{ex.Message}]";
                IntegrationService.ITG_MSGO_Activitie(request, short.Parse(Constant.RETURN_CODE_ERROR), message, creator);
                return false;
            }

        }
    }
}
