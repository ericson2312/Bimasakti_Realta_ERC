using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using SAB02400Common;
using SAB02400Common.DTOs;
using System;
using System.Threading.Tasks;

namespace SAB02400Model
{
    public class SAB02400Model : ISAB02400
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/SAB02400";

        #region SampleErrorMultiLang
        public SAB02400ResultDTO SampleErrorMultiLang()
        {
            throw new System.NotImplementedException();
        }

        public async Task<SAB02400ResultDTO> SampleErrorMultiLangAsync()
        {
            var loEx = new R_Exception();
            SAB02400ResultDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<SAB02400ResultDTO>(
                    DEFAULT_SERVICEPOINT_NAME,
                    nameof(ISAB02400.SampleErrorMultiLang),
                    "",
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
