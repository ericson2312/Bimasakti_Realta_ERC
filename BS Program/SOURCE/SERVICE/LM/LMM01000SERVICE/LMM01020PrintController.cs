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
    public class LMM01020PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private LMM01020PrintParamDTO _AllRateWGParameter;

        #region instantiate
        public LMM01020PrintController()
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
            pcFileTemplate = System.IO.Path.Combine("Reports", "LMM01020RateWG.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllRateWGParameter));
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
        public R_DownloadFileResultDTO AllRateWGReportPost(LMM01020PrintParamDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<LMM01020PrintParamDTO>(poParameter));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamRateWGReportGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                _AllRateWGParameter = R_NetCoreUtility.R_DeserializeObjectFromByte<LMM01020PrintParamDTO>(R_DistributedCache.Cache.Get(pcGuid));
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
        private LMM01020ResultWithBaseHeaderPrintDTO GenerateDataPrint(LMM01020PrintParamDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01020ResultWithBaseHeaderPrintDTO loRtn = new LMM01020ResultWithBaseHeaderPrintDTO();

            try
            {
                LMM01020ResultPrintDTO loData = new LMM01020ResultPrintDTO()
                {
                    Title = "Water and Gas",
                    Header = $"{poParam.CPROPERTY_ID} - {poParam.CPROPERTY_NAME}",
                };

                var loCls = new LMM01020Cls();

                var loHeaderParam = R_Utility.R_ConvertObjectToObject<LMM01020PrintParamDTO, LMM01020DTO>(poParam);
                var loDetailParam = R_Utility.R_ConvertObjectToObject<LMM01020PrintParamDTO, LMM01021DTO>(poParam);

                var loHeaderCollection = loCls.GetHDReportRateWG(loHeaderParam);
                var loDetailCollection = loCls.GetDetailReportRateWG(loDetailParam);

                loHeaderCollection.CRATE_WG_LIST = new List<LMM01021DTO>();
                loHeaderCollection.CRATE_WG_LIST.AddRange(loDetailCollection);

                loData.HeaderData = loHeaderCollection;

                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "Water and Gas",
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
                loRtn.RateWG = loData;

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