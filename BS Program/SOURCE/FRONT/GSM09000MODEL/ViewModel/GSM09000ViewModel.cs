using GSM09000COMMON;
using GSM09000COMMON.DTOs.GSM09000;
using GSM09000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM09000MODEL.ViewModel
{
    public class GSM09000ViewModel : R_ViewModel<GSM09000DetailDTO>
    {
        private GSM09000Model loModel = new GSM09000Model();

        public GSM09000DetailDTO loTenantCategoryDetail = null;

        public List<GSM09000DTO> loTenantCategoryList = new List<GSM09000DTO>();

        public GSM09000ResultDTO loRtn = null;

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public List<GSM09000DTO> loParentPositionList = new List<GSM09000DTO>();

        public GetPropertyListResultDTO loPropertyRtn = null;

        //public GetParentPositionIfEmptyDTO loParentPositionIfEmpty = new GetParentPositionIfEmptyDTO();

        public void UpdateParentPositionList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loParentPositionList = new List<GSM09000DTO>(loTenantCategoryList);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loPropertyRtn = await loModel.GetPropertyListStreamAsync();
                loPropertyList = new List<GetPropertyListDTO>(loPropertyRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public void TenantCategoryValidation(GSM09000DetailDTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CCATEGORY_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V001"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CCATEGORY_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V002"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTenantCategoryListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09000_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loRtn = await loModel.GetTenantCategoryListStreamAsync();
                loParentPositionList = new List<GSM09000DTO>(loRtn.Data);
                loTenantCategoryList = new List<GSM09000DTO>(loRtn.Data);
                loTenantCategoryList.ForEach(x =>
                {
                    x.CDISPLAY = "[" + x.ILEVEL + "] " + x.CCATEGORY_ID + " - " + x.CCATEGORY_NAME;
                    x.CPARENT = string.IsNullOrWhiteSpace(x.CPARENT) ? null : x.CPARENT;
                });

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
/*
        public async Task GetParentPositionIfEmptyAsync()
        {
            R_Exception loEx = new R_Exception();
            GetParentPositionIfEmptyParameterDTO loParam = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.GSM09000_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                loParam = new GetParentPositionIfEmptyParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loProperty.CPROPERTY_ID
                };
                GetParentPositionIfEmptyResultDTO loResult = await loModel.GetParentPositionIfEmptyAsync(loParam);

                loParentPositionIfEmpty = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
*/
        public async Task InsertNewRootIfListEmptyAsync()
        {
            R_Exception loEx = new R_Exception();
            InsertNewRootIfListEmptyParameterDTO loParam = null;

            try
            {
                loParam = new InsertNewRootIfListEmptyParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loProperty.CPROPERTY_ID,
                    CSELECTED_PROPERTY_NAME = loProperty.CPROPERTY_NAME
                };
                await loModel.InsertNewRootIfListEmptyAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetTenantCategoryAsync(GSM09000DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09000_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                GSM09000DetailDTO loResult = await loModel.R_ServiceGetRecordAsync(poEntity);

                loTenantCategoryDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveTenantCategoryAsync(GSM09000DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09000_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                GSM09000DetailDTO loResult = await loModel.R_ServiceSaveAsync(poEntity, peCRUDMode);

                loTenantCategoryDetail = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteTenantCategoryAsync(GSM09000DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09000_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                await loModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
