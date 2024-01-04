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
    public class LMM01000UniversalController : ControllerBase, ILMM01000Universal
    {
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetChargesTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllChargesType(poParam);

                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetStatusList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllStatus(poParam);

                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetTaxExemptionCodeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllTaxExemptionCode(poParam);

                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetWithholdingTaxTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllWithholdingTaxType(poParam);

                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetUsageRateModeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllUsageRateMode(poParam);

                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetRateTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllRateType(poParam);

                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetAdminFeeTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAdminFeeType(poParam);

                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetAccrualMethodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllAccrualMethod(poParam);

                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
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