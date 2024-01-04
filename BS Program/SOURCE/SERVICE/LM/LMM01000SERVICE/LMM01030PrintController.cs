using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using LMM01000BACK;
using LMM01000COMMON;
using LMM01000COMMON.Print;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Reflection;

namespace LMM01000SERVICE
{
    public class LMM01030PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private LMM01030PrintParamDTO _AllRatePGParameter;

        #region instantiate
        public LMM01030PrintController()
        {
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "LMM01030RatePG.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllRatePGParameter));
            pcDataSourceName = "ResponseDataModel";
        }

        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        }
        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO AllRatePGReportPost(LMM01030PrintParamDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<LMM01030PrintParamDTO>(poParameter));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamRatePGReportGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                _AllRatePGParameter = R_NetCoreUtility.R_DeserializeObjectFromByte<LMM01030PrintParamDTO>(R_DistributedCache.Cache.Get(pcGuid));
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        #region Helper
        private LMM01030ResultWithBaseHeaderPrintDTO GenerateDataPrint(LMM01030PrintParamDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01030ResultWithBaseHeaderPrintDTO loRtn = new LMM01030ResultWithBaseHeaderPrintDTO();

            try
            {
                LMM01030ResultPrintDTO loData = new LMM01030ResultPrintDTO()
                {
                    Title = "Parkir and General Fixed Rate",
                    Header = $"{poParam.CPROPERTY_ID} - {poParam.CPROPERTY_NAME}",
                };

                var loCls = new LMM01030Cls();

                var loHeaderParam = R_Utility.R_ConvertObjectToObject<LMM01030PrintParamDTO, LMM01030DTO>(poParam);

                var loHeaderCollection = loCls.GetReportRatePG(loHeaderParam);

                loData.HeaderData = loHeaderCollection;

                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "Parkir and General Fixed Rate",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };

                //Assembly loAsm = Assembly.Load("BIMASAKTI_LM_API");

                //var lcResourceFile = "BIMASAKTI_LM_API.Image.CompanyLogo.png";

                //using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                //{

                //    var ms = new MemoryStream();
                //    resFilestream.CopyTo(ms);
                //    var bytes = ms.ToArray();

                //    loParam.BLOGO_COMPANY = bytes;
                //}

                loRtn.BaseHeaderData = loParam;
                loRtn.RatePG = loData;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion
    }
   
}