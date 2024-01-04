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
    public class GLM00410Controller : ControllerBase, IGLM00410
    {
        [HttpPost]
        public IAsyncEnumerable<GLM00411DTO> GetAllocationAccountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00411DTO> loRtn = null;
            List<GLM00411DTO> loTempRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loTempRtn = new List<GLM00411DTO>();
                var poParam = new GLM00411DTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);

                loTempRtn = loCls.GetAllAllocationAccount(poParam);

                loRtn = GetAllocationAccountListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00411DTO> GetAllocationAccountListStream(List<GLM00411DTO> poParameter)
        {
            foreach (GLM00411DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00415DTO> GetAllocationPeriodByTargetCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00415DTO> loRtn = null;
            List<GLM00415DTO> loTempRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loTempRtn = new List<GLM00415DTO>();

                var poParam = new GLM00415DTO();
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);
                poParam.CPERIOD_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD_NO);
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstant.CYEAR);

                loTempRtn = loCls.GetAllAllocationPeriodByTargetCenter(poParam);

                loRtn = GetAllocationPeriodByTargetCenterListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00415DTO> GetAllocationPeriodByTargetCenterListStream(List<GLM00415DTO> poParameter)
        {
            foreach (GLM00415DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00414DTO> GetAllocationPeriodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00414DTO> loRtn = null;
            List<GLM00414DTO> loTempRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loTempRtn = new List<GLM00414DTO>();
                var poParam = new GLM00414DTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CCYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstant.CYEAR);

                loTempRtn = loCls.GetAllAllocationPeriod(poParam);

                loRtn = GetAllocationPeriodListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00414DTO> GetAllocationPeriodListStream(List<GLM00414DTO> poParameter)
        {
            foreach (GLM00414DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00413DTO> GetAllocationTargetCenterByPeriodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00413DTO> loRtn = null;
            List<GLM00413DTO> loTempRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loTempRtn = new List<GLM00413DTO>();
                var poParam = new GLM00413DTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_CENTER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_CENTER_ID);
                poParam.CYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstant.CYEAR);

                loTempRtn = loCls.GetAllAllocationTargetCenterByPeriod(poParam);

                loRtn = GetAllocationTargetCenterByPeriodListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00413DTO> GetAllocationTargetCenterByPeriodListStream(List<GLM00413DTO> poParameter)
        {
            foreach (GLM00413DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GLM00412DTO> GetAllocationTargetCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLM00412DTO> loRtn = null;
            List<GLM00412DTO> loTempRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loTempRtn = new List<GLM00412DTO>();
                var poParam = new GLM00412DTO();

                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CREC_ID_ALLOCATION_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID_ALLOCATION_ID);

                loTempRtn = loCls.GetAllAllocationTargetCenter(poParam);

                loRtn = GetAllocationTargetCenterStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GLM00412DTO> GetAllocationTargetCenterStream(List<GLM00412DTO> poParameter)
        {
            foreach (GLM00412DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLM00410DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new GLM00410Cls();

                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLM00410DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLM00410DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GLM00410DTO> loRtn = new R_ServiceGetRecordResultDTO<GLM00410DTO>();

            try
            {
                var loCls = new GLM00410Cls();

                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GLM00410DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLM00410DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GLM00410DTO> loRtn = new R_ServiceSaveResultDTO<GLM00410DTO>();

            try
            {
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLM00410Cls();

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
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
