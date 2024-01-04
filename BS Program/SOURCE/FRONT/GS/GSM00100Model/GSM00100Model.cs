using GSM00100Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM00100Model
{
    public class GSM00100Model : R_BusinessObjectServiceClientBase<GSM00100DTO>, IGSM00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM00100";

        public GSM00100Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, plSendWithContext, plSendWithToken)
        {
        }

        #region GetSMTPList
        public GSM00100GenericResultDTO<List<GSM00100DTOList>> GetSMTPList()
        {
            throw new System.NotImplementedException();
        }

        public async Task<GSM00100GenericResultDTO<List<GSM00100DTOList>>> GetSMTPListAsync()
        {
            var loEx = new R_Exception();
            GSM00100GenericResultDTO<List<GSM00100DTOList>> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM00100GenericResultDTO<List<GSM00100DTOList>>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM00100.GetSMTPList),
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        public GSM00100GenericResultDTO<bool> CheckDelete(GSM00100DTO poParam)
        {
            throw new System.NotImplementedException();
        }

        public GSM00100GenericResultDTO CheckSupportedEmailProvider()
        {
            throw new System.NotImplementedException();
        }

        public GSM00100GenericResultDTO TestSendEmail(EmailDTO poParam)
        {
            throw new System.NotImplementedException();
        }
    }
}
