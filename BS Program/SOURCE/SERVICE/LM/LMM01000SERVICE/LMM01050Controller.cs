using LMM01000BACK;
using LMM01000COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM01000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01050Controller : ControllerBase, ILMM01050
    {
        [HttpPost]
        public IAsyncEnumerable<LMM01051DTO> GetRateOTList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01051DTO> loRtn = null;
            List<LMM01051DTO> loTempRtn = null;

            try
            {
                var loCls = new LMM01050Cls();
                loTempRtn = new List<LMM01051DTO>();
                var poParam = new LMM01051DTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CDAY_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDAY_TYPE);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loTempRtn = loCls.GetAllRateOTList(poParam);

                loRtn = GetRateOTListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<LMM01051DTO> GetRateOTListStream(List<LMM01051DTO> poParameter)
        {
            foreach (LMM01051DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01050DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01050DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01050DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01050DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01050Cls();

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
        public R_ServiceSaveResultDTO<LMM01050DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01050DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01050DTO> loRtn = new R_ServiceSaveResultDTO<LMM01050DTO>();

            try
            {
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                    poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01050Cls();

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01050DTO> poParameter)
        {
            throw new NotImplementedException();
        }

    }
}