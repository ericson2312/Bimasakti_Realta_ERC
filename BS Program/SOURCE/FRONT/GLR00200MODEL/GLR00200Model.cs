using GLR00200COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLR00200MODEL
{
    public class GLR00200Model : R_BusinessObjectServiceClientBase<GLR00200DTO>, IGLR00200
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLR00200";
        private const string DEFAULT_MODULE = "GL";

        public GLR00200Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GLR00200InitialDTO GetInitialVar()
        {
            throw new NotImplementedException();
        }
        public async Task<GLR00200InitialDTO> GetInitialVarAsync()
        {
            var loEx = new R_Exception();
            GLR00200InitialDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLR00200InitialDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLR00200.GetInitialVar),
                    DEFAULT_MODULE,
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

        public IAsyncEnumerable<GLR00200UniversalDTO> GetPrintMethodList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GLR00200UniversalDTO>> GetPrintMethodListAsync()
        {
            var loEx = new R_Exception();
            List<GLR00200UniversalDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLR00200UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLR00200.GetPrintMethodList),
                    DEFAULT_MODULE,
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

        public GLR00200GLSystemParamDTO GetSystemParam()
        {
            throw new NotImplementedException();
        }

        public async Task<GLR00200GLSystemParamDTO> GetSystemParamAsync()
        {
            var loEx = new R_Exception();
            GLR00200GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLR00200GLSystemParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLR00200.GetSystemParam),
                    DEFAULT_MODULE,
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

    }
}
