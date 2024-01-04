using GLB00600BACK;
using GLB00600COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLB00600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLB00600Controller : ControllerBase, IGLB00600
    {
        [HttpPost]
        public GLB00600GSMTransactionCodeDTO GetInitialGSMTransactionCode(GLB00600GSMTransactionCodeDTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600GSMTransactionCodeDTO loRtn = null;

            try
            {
                loRtn = new GLB00600GSMTransactionCodeDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GLB00600Cls();

                loRtn = loCls.GetGSMTransactionCode(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GLB00600SuspenseAmountDTO GetInitialSupenseAmount(GLB00600SuspenseAmountDTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600SuspenseAmountDTO loRtn = null;

            try
            {
                loRtn = new GLB00600SuspenseAmountDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GLB00600Cls();

                loRtn = loCls.GetSuspenseAmount(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GLB00600InitialDTO GetInitialVar(GLB00600InitialDTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600InitialDTO loRtn = null;

            try
            {
                loRtn = new GLB00600InitialDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLB00600Cls();

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
        public GLB00600DTO GetResultClosingEntries(GLB00600DTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600DTO loRtn = new GLB00600DTO();

            try
            {
                var loCls = new GLB00600Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;


                loCls.GetClosingEntries(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GLB00600GLSystemParamDTO GetSystemParam()
        {
            var loEx = new R_Exception();
            GLB00600GLSystemParamDTO loRtn = null;

            try
            {
                loRtn = new GLB00600GLSystemParamDTO();

                var poParam = new GLB00600GLSystemParamDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLB00600Cls();

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
        public IAsyncEnumerable<GLB00600DTO> GetValidationClosingResult(GLB00600DTO poParam)
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLB00600DTO> loRtn = null;

            try
            {
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLB00600Cls();

                var loResult = loCls.GetResult(poParam);

                loRtn = GetStream<GLB00600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}
