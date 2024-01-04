using LMM01500BACK;
using LMM01500COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01530Controller : ControllerBase, ILMM01530
    {
        [HttpPost]
        public IAsyncEnumerable<LMM01530DTO> GetAllOtherChargerList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01530DTO> loRtn = null;
            List<LMM01530DTO> loTempRtn = null;
            var loParameter = new LMM01530DTO();

            try
            {
                var loCls = new LMM01530Cls();
                loTempRtn = new List<LMM01530DTO>();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CINVGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CINVGRP_CODE);
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loTempRtn = loCls.GetAllOtherCharges(loParameter);

                loRtn = GetAllOtherChargerListtStream<LMM01530DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetAllOtherChargerListtStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01531DTO> GetOtherChargesLookup()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01531DTO> loRtn = null;

            try
            {
                var loCls = new LMM01530Cls();
                var poParam = new LMM01531DTO();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllOtherChargesLookup(poParam);

                loRtn = GetAllOtherChargerListtStream<LMM01531DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01530DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CINVGRP_CODE = R_Utility.R_GetContext<string>(ContextConstant.CINVGRP_CODE);


                var loCls = new LMM01530Cls();

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
        public R_ServiceGetRecordResultDTO<LMM01530DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01530DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01530DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01530DTO>();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CINVGRP_CODE = R_Utility.R_GetContext<string>(ContextConstant.CINVGRP_CODE);

                var loCls = new LMM01530Cls();

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
        public R_ServiceSaveResultDTO<LMM01530DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01530DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01530DTO> loRtn = new R_ServiceSaveResultDTO<LMM01530DTO>();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CINVGRP_CODE = R_Utility.R_GetContext<string>(ContextConstant.CINVGRP_CODE);

                var loCls = new LMM01530Cls();

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
