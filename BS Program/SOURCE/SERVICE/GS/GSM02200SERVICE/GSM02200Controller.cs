using GSM02200BACK;
using GSM02200COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Reflection;

namespace GSM02200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM02200Controller : ControllerBase, IGSM02200
    {
        [HttpPost]
        public GSM02200UploadFileDTO DownloadTemplateFile()
        {
            var loEx = new R_Exception();
            var loRtn = new GSM02200UploadFileDTO();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");

                var lcResourceFile = "BIMASAKTI_GS_API.Template.Geography.xlsx";
                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02200DTO> GetGeographyList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM02200DTO> loRtn = null;
            GSM02200DTO loParameter = null;

            try
            {
                var loCls = new GSM02200Cls();
                loParameter = new GSM02200DTO();

                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllGeography(loParameter);

                loRtn = GetListStream<GSM02200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<T> GetListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GSM02200SingleResult<GSM02200DTO> GSM02200GeographyChangeStatus(GSM02200DTO poParam)
        {
            R_Exception loException = new R_Exception();
            GSM02200SingleResult<GSM02200DTO> loRtn = new GSM02200SingleResult<GSM02200DTO>();
            GSM02200Cls loCls = new GSM02200Cls();

            try
            {
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.GSM02200ChangeStatusSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02200DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM02200DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02200DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02200DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02200DTO>();

            try
            {
                // Set Param
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM02200Cls();

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
        public R_ServiceSaveResultDTO<GSM02200DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02200DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM02200DTO> loRtn = new R_ServiceSaveResultDTO<GSM02200DTO>();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM02200Cls();

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