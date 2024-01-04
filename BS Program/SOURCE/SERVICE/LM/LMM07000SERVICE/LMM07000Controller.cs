using LMM07000BACK;
using LMM07000COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM07000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM07000Controller : ControllerBase, ILMM07000
    {
        // Method Stream
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<LMM07000DTO> GetChargesDiscountList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000DTO> loRtn = null;
            var loParameter = new LMM07000DTO();

            try
            {
                var loCls = new LMM07000Cls();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);

                var loResult = loCls.GetAllChargesDiscount(loParameter);

                loRtn = GetStream<LMM07000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM07000DTOInitial> GetProperty()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000DTOInitial> loRtn = null;
            var loParameter = new LMM07000DTOInitial();

            try
            {
                var loCls = new LMM07000Cls();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetProperty(loParameter);

                loRtn = GetStream<LMM07000DTOInitial>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM07000DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new LMM07000Cls();

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
        public R_ServiceGetRecordResultDTO<LMM07000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM07000DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM07000DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM07000DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM07000Cls();

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
        public R_ServiceSaveResultDTO<LMM07000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM07000DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM07000DTO> loRtn = new R_ServiceSaveResultDTO<LMM07000DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM07000Cls();

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
        public LMM07000Record<LMM07000DTO> LMM07000ActiveInactive(LMM07000DTO poParam)
        {
            R_Exception loException = new R_Exception();
            LMM07000Record<LMM07000DTO> loRtn = null;

            try
            {
                LMM07000Cls loCls = new LMM07000Cls();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.LMM00700ActiveInactiveSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM07000Record<LMM07000PeriodDTO> GetMaxInvoicePeriodValue(LMM07000PeriodDTO poParam)
        {
            R_Exception loException = new R_Exception();
            LMM07000Record<LMM07000PeriodDTO> loRtn = null;

            try
            {
                loRtn = new LMM07000Record<LMM07000PeriodDTO> ();
                LMM07000Cls loCls = new LMM07000Cls();

                //set global var
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                loRtn.Data = loCls.GetMaxInvoicePeriod(poParam);
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