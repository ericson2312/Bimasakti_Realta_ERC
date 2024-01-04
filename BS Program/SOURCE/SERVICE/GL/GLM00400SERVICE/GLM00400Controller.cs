﻿using GLM00400BACK;
using GLM00400COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLM00400SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLM00400Controller : ControllerBase, IGLM00400
    {
        [HttpPost]
        public GLM00400InitialDTO GetInitialVar(GLM00400InitialDTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400InitialDTO loRtn = null;

            try
            {
                loRtn = new GLM00400InitialDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLM00400Cls();

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
        public GLM00400GLSystemParamDTO GetSystemParam(GLM00400GLSystemParamDTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400GLSystemParamDTO loRtn = null;

            try
            {
                loRtn = new GLM00400GLSystemParamDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLM00400Cls();

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
        public IAsyncEnumerable<GLM00400DTO> GetAllocationJournalHDList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00400DTO> loRtn = null;

            try
            {
                var loCls = new GLM00400Cls();
                var poParam = new GLM00400DTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loTempRtn = loCls.GetAllAllocationJournalHD(poParam);

                loRtn = GetAllocationJournalHDListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<GLM00400DTO> GetAllocationJournalHDListStream(List<GLM00400DTO> poParameter)
        {
            foreach (GLM00400DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
