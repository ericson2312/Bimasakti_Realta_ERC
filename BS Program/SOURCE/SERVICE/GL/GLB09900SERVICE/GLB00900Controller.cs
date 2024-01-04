using GLB09900BACK;
using GLB09900COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLB09900SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLB09900Controller : ControllerBase, IGLB09900
    {
        [HttpPost]
        public GLB09900InitialDTO GetInitialVar()
        {
            var loEx = new R_Exception();
            GLB09900InitialDTO loRtn = null;

            try
            {
                loRtn = new GLB09900InitialDTO();

                GLB09900InitialDTO poParam = new GLB09900InitialDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLB09900Cls();

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
        public GLB09900GLSystemParamDTO GetSystemParam()
        {
            var loEx = new R_Exception();
            GLB09900GLSystemParamDTO loRtn = null;

            try
            {
                GLB09900GLSystemParamDTO poParam = new GLB09900GLSystemParamDTO();

                loRtn = new GLB09900GLSystemParamDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLB09900Cls();

                loRtn = loCls.GetSystemParam(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GLB09900DTO GetResultCloseGlPeriod(GLB09900DTO poParam)
        {
            var loEx = new R_Exception();
            GLB09900DTO loRtn = null;

            try
            {
                loRtn = new GLB09900DTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLB09900Cls();

                loRtn = loCls.GetResultClosePeriod(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GLB09900ValidateDTO GetValidateResultCloseGlPeriod(GLB09900ValidateDTO poParam)
        {
            var loEx = new R_Exception();
            GLB09900ValidateDTO loRtn = null;

            try
            {
                loRtn = new GLB09900ValidateDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GLB09900Cls();

                loRtn = loCls.GetValidateResultClosePeriod(poParam);
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
