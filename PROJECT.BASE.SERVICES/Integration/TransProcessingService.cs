using Lib.Setting;
using Lib.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using PROJECT.BASE.CORE;
using PROJECT.BASE.DAO;
using PROJECT.BASE.ENTITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace PROJECT.BASE.SERVICES
{
    public class TransProcessingService
    {
        public TransProcessingService() { }
        public static async Task ProcessingTrans(IntegrationRequest request, string creator)
        {
            #region Kiểm tra tính hợp lệ giao dịch
            var resultValidate = await TransValidateService.ValidateTrans(request, creator);
            if (!resultValidate)
                return;
            #endregion
            #region Xử lý giao dịch - Thụ lý
            var resultAcceptance = await AcceptanceTrans(request, creator);
            if (!resultAcceptance)
                return;
            #endregion
            //#region Xử lý giao dịch - Extract
            //var resultExtract = await ExtractTrans(creator, request);
            //if (!resultExtract)
            //    return;
            //#endregion
            //#region Xử lý giao dịch - Transform
            //var resultTransform = await TransformTrans(creator, request);
            //if (!resultTransform)
            //    return;
            //#endregion

        }
        /// <summary>
        /// STEP_3 - Thụ lý gia dịch
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<bool> AcceptanceTrans(IntegrationRequest request, string creator)
        {
            try
            {
                var resultUpload = await IntegrationService.UploadStorage(request, creator);
                if (string.IsNullOrEmpty(resultUpload))
                {
                    IntegrationService.ITG_MSGO_Activitie(request,
                    short.Parse(Constant.RETURN_CODE_FAILED),
                    $"{Constant.STEP_3}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_3]}",
                    creator);
                    return false;
                }
                else
                {
                    IntegrationService.ITG_MSGO_Activitie(request,
                    short.Parse(Constant.RETURN_CODE_SUCCESS),
                    $"{Constant.STEP_3}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_3]}",
                    creator);
                    return true;
                }
                
                
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                var message = $"{Constant.STEP_3}-{ConstantConfig.DICT_STEP_TRANSACTION[Constant.STEP_3]}:[{ex.Message}]";
                IntegrationService.ITG_MSGO_Activitie(request,short.Parse(Constant.RETURN_CODE_ERROR), message, creator);
                return false;
            }


        }      
        

    }
}
