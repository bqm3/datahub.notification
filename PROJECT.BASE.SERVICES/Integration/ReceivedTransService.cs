using Aspose.Cells;
using Lib.Setting;
using Lib.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using PROJECT.BASE.CORE;
using PROJECT.BASE.DAO;
using PROJECT.BASE.ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
//using static Google.Apis.Requests.BatchRequest;

namespace PROJECT.BASE.SERVICES
{
    public class ReceivedTransService
    {
        public ReceivedTransService() { }
        /// <summary>
        /// STEP_1 - Tiếp nhận giao dịch đầu vào -- Đã nhận giao dịch
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static long? ReceivedTrans(IntegrationRequest request, string creator)
        {
            try
            {
                var curentInfo = DataObjectFactory.GetInstanceITG_MSGI().GetList(new Dictionary<string, object>
                {
                    {"MSG_ID", request.Header.Msg_Id}
                });
                string bucketName = request.Header.Tran_Code.ToLower();
                string objectName = $"{request.Header.Msg_Id}.{request.Header.Data_Type}";
                var result = DataObjectFactory.GetInstanceITG_MSGI().Insert(new ITG_MSGI_Information()
                {
                    CODE = (request != null && !string.IsNullOrEmpty(request.Header.Request_Id)) ? request.Header.Request_Id : null,
                    MSG_ID = (request != null && !string.IsNullOrEmpty(request.Header.Msg_Id)) ? request.Header.Msg_Id : null,
                    MSG_REFID = (request != null && !string.IsNullOrEmpty(request.Header.Msg_Refid)) ? request.Header.Msg_Refid : null,
                    MSG_CONTENT = $"{bucketName}/{objectName}",
                    TRAN_CODE = (request != null && !string.IsNullOrEmpty(request.Header.Tran_Code)) ? request.Header.Tran_Code : null,
                    TRAN_NAME = (request != null && !string.IsNullOrEmpty(request.Header.Tran_Name)) ? request.Header.Tran_Name : null,
                    SENDER_CODE = (request != null && !string.IsNullOrEmpty(request.Header.Sender_Code)) ? request.Header.Sender_Code : null,
                    SENDER_NAME = (request != null && !string.IsNullOrEmpty(request.Header.Sender_Name)) ? request.Header.Sender_Name : null,
                    RECEIVER_CODE = (request != null && !string.IsNullOrEmpty(request.Header.Receiver_Code)) ? request.Header.Receiver_Code : null,
                    RECEIVER_NAME = (request != null && !string.IsNullOrEmpty(request.Header.Receiver_Name)) ? request.Header.Receiver_Name : null,
                    DATA_TYPE = (request != null && !string.IsNullOrEmpty(request.Header.Data_Type)) ? request.Header.Data_Type : null,
                    SEND_DATE = (request != null && !string.IsNullOrEmpty(request.Header.Send_Date)) ? request.Header.Send_Date : null,
                    ORIGINAL_CODE = (request != null && !string.IsNullOrEmpty(request.Header.Original_Code)) ? request.Header.Original_Code : null,
                    ORIGINAL_NAME = (request != null && !string.IsNullOrEmpty(request.Header.Original_Name)) ? request.Header.Original_Name : null,
                    ORIGINAL_DATE = (request != null && !string.IsNullOrEmpty(request.Header.Original_Date)) ? request.Header.Original_Date : null,
                    VERSION = (request != null && !string.IsNullOrEmpty(request.Header.Version)) ? request.Header.Version : null,
                    STATUS = short.Parse(Constant.RETURN_CODE_SUCCESS),
                    REMOVED = 0,
                    CREATOR = creator

                });
                if (result != null && result > 0)
                {
                    IntegrationService.ITG_MSGO_Activitie(request,
                        short.Parse(Constant.RETURN_CODE_SUCCESS),
                        $"{Constant.STEP_1}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_1]}",
                        creator);
                }
                else
                {
                    IntegrationService.ITG_MSGO_Activitie(request,
                        short.Parse(Constant.RETURN_CODE_FAILED),
                        $"{Constant.STEP_1}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_1]}",
                        creator);
                    return null;
                }
                if (curentInfo != null && curentInfo.Count > 0)
                {
                    var message = $"Msg_Id:{request.Header.Msg_Id} - {Constant.MESSAGE_TRANSACTION_DUPLICATE}";
                    message = $"{Constant.STEP_2}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_2]}:{message}";
                    IntegrationService.ITG_MSGO_Activitie(request, short.Parse(Constant.RETURN_CODE_WARNING), message, creator);
                    return null;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                var message = $"{Constant.STEP_1}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_1]}:[{ex.Message}]";
                IntegrationService.ITG_MSGO_Activitie(request, short.Parse(Constant.RETURN_CODE_ERROR), message, creator);
                return null;
            }
        }


    }
}
