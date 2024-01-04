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
    public class LMM07000UniversalController : ControllerBase, ILMM07000Universal
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
        public IAsyncEnumerable<LMM07000DTOUniversal> GetChargesTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000DTOUniversal> loRtn = null;
            var loParameter = new LMM07000DTOUniversal();

            try
            {
                var loCls = new LMM07000UniversalCls();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllChargesType(loParameter);

                loRtn = GetStream<LMM07000DTOUniversal>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM07000DTOUniversal> GetDiscountTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000DTOUniversal> loRtn = null;
            var loParameter = new LMM07000DTOUniversal();

            try
            {
                var loCls = new LMM07000UniversalCls();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.GetAllDiscountType(loParameter);

                loRtn = GetStream<LMM07000DTOUniversal>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM07000PeriodDTO> GetInvPeriodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000PeriodDTO> loRtn = null;
            var loParameter = new LMM07000PeriodDTO();

            try
            {
                var loCls = new LMM07000UniversalCls();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllInvoicePeriod(loParameter);

                loRtn = GetStream<LMM07000PeriodDTO>(loResult);
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