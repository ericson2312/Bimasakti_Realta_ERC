using GLM00400BACK;
using GLM00400COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLM00400SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLM00430Controller : ControllerBase, IGLM00430
    {
        [HttpPost]
        public IAsyncEnumerable<GLM00430DTO> GetSourceAllocationAccountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00430DTO> loRtn = null;

            try
            {
                var loCls = new GLM00430Cls();
                var poParam = new GLM00430DTO();

                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllSourceAllocationAccount(poParam);
                
                loRtn = GetStream<GLM00430DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00431DTO> GetAllocationAccountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00431DTO> loRtn = null;

            try
            {
                var loCls = new GLM00430Cls();
                var poParam = new GLM00431DTO();
                
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllAllocationAccount(poParam);

                loRtn = GetStream<GLM00431DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GLM00431DTO SaveAllocationAccountList(GLM00431DTO poParam)
        {
            R_Exception loException = new R_Exception();
            GLM00431DTO loRtn = new GLM00431DTO();
            GLM00430Cls loCls = new GLM00430Cls();

            try
            {
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.SaveAllocationAccount(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
