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
    public class LMM01010Controller : ControllerBase, ILMM01010
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01010DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01010DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01010DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01010DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01010Cls();

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
        public R_ServiceSaveResultDTO<LMM01010DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01010DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01010DTO> loRtn = new R_ServiceSaveResultDTO<LMM01010DTO>();

            try
            {
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                    poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01010Cls();

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01010DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01011DTO> GetRateECList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01011DTO> loRtn = null;
            List<LMM01011DTO> loTempRtn = null;

            try
            {
                var loCls = new LMM01010Cls();
                loTempRtn = new List<LMM01011DTO>();
                var poParam = new LMM01011DTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loTempRtn = loCls.GetAllRateECList(poParam);

                loRtn = GetRateECListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<LMM01011DTO> GetRateECListStream(List<LMM01011DTO> poParameter)
        {
            foreach (LMM01011DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}