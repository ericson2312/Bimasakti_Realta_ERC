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
    public class GLM00420Controller : ControllerBase, IGLM00420
    {
        [HttpPost]
        public IAsyncEnumerable<GLM00420DTO> GetSourceAllocationCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00420DTO> loRtn = null;

            try
            {
                var loCls = new GLM00420Cls();
                var poParam = new GLM00420DTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);

                var loResult = loCls.GetAllSourceAllocationCenter(poParam);

                loRtn = GetStream<GLM00420DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00421DTO> GetAllocationCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00421DTO> loRtn = null;

            try
            {
                var loCls = new GLM00420Cls();
                var poParam = new GLM00421DTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllAllocationCenter(poParam);

                loRtn = GetStream<GLM00421DTO>(loResult);
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
        public GLM00421DTO SaveAllocationCenterList(GLM00421DTO poParam)
        {
            R_Exception loException = new R_Exception();
            GLM00421DTO loRtn = new GLM00421DTO();
            GLM00420Cls loCls = new GLM00420Cls();

            try
            {
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.SaveAllocationCenter(poParam);
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
