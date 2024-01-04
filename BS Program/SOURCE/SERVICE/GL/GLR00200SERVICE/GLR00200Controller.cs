using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GLR00200BACK;
using GLR00200COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Reflection;

namespace GLR00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLR00200Controller : ControllerBase, IGLR00200
    {
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GLR00200InitialDTO GetInitialVar()
        {
            var loEx = new R_Exception();
            GLR00200InitialDTO loRtn = null;

            try
            {
                var poParam = new GLR00200InitialDTO();
                loRtn = new GLR00200InitialDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLR00200Cls();

                loRtn = loCls.GetInitial(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLR00200UniversalDTO> GetPrintMethodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLR00200UniversalDTO> loRtn = null;

            try
            {
                var poParam = new GLR00200UniversalDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLR00200Cls();

                var loTempRtn = loCls.GetAllPrintMethod(poParam);

                loRtn = GetStream<GLR00200UniversalDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GLR00200GLSystemParamDTO GetSystemParam()
        {
            var loEx = new R_Exception();
            GLR00200GLSystemParamDTO loRtn = null;

            try
            {
                var poParam = new GLR00200GLSystemParamDTO();
                loRtn = new GLR00200GLSystemParamDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLR00200Cls();

                loRtn = loCls.GetSystemParam(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}