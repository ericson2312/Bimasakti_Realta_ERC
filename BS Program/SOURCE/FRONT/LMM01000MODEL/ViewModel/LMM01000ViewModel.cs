using LMM01000COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01000ViewModel : R_ViewModel<LMM01000DTO>
    {
        private LMM01000Model _LMM01000Model = new LMM01000Model();
        private LMM01000UniversalModel _LMM01000UniversalModel = new LMM01000UniversalModel();

        public ObservableCollection<LMM01000UniversalDTO> ChargesTypeGrid { get; set; } = new ObservableCollection<LMM01000UniversalDTO>();
        public ObservableCollection<LMM01002DTO> ChargesUtilityGrid { get; set; } = new ObservableCollection<LMM01002DTO>();
        public List<LMM01000DTOPropety> PropertyList { get; set; } = new List<LMM01000DTOPropety>();

        public LMM01000DTO UtilityCharges = new LMM01000DTO();
        public LMM01000UniversalDTO ChargesType = new LMM01000UniversalDTO();

        public string PropertyValueContext = "";
        public string StatusChange = "00";
        public bool Accrual = false;
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000Model.GetPropertyAsync();
                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesTypeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetChargesTypeListAsync();

                ChargesTypeGrid = new ObservableCollection<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesType(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000Model.GetChargesTypeAsync();

                ChargesType = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesUtilityList(LMM01002DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);

                var loResult = await _LMM01000Model.GetChargesUtilityListAsync();

                ChargesUtilityGrid = new ObservableCollection<LMM01002DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesUtility(LMM01000DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _LMM01000Model.R_ServiceGetRecordAsync(poParam);

                Accrual = loResult.LACCRUAL != null ? loResult.LACCRUAL : Accrual;

                UtilityCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationUtility(LMM01000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CCHARGES_ID);
                if (lCancel)
                {
                    loEx.Add("", "Charges Id is required");
                }

                lCancel = string.IsNullOrEmpty(poParam.CCHARGES_NAME);

                if (lCancel)
                {
                    loEx.Add("", "Charges Name is required");

                }

                lCancel = string.IsNullOrEmpty(poParam.CDESCRIPTION);

                if (lCancel)
                {
                    loEx.Add("", "Charges Description is required");

                }

                lCancel = string.IsNullOrEmpty(poParam.CUTILITY_JRNGRP_CODE);

                if (lCancel)
                {
                    loEx.Add("", "Journal Group is required");

                }


                if (poParam.LTAXABLE)
                {
                    if (poParam.LTAX_EXEMPTION)
                    {
                        lCancel = string.IsNullOrEmpty(poParam.CTAX_EXEMPTION_CODE);

                        if (lCancel)
                        {
                            loEx.Add("", "Tax Exemption Code is required");
                        }

                        lCancel = poParam.ITAX_EXEMPTION_PCT == 0 || poParam.ITAX_EXEMPTION_PCT < 0 || poParam.ITAX_EXEMPTION_PCT > 100;

                        if (lCancel)
                        {
                            loEx.Add("", "Tax Exemption in range 1 - 100 %");
                        }
                    }
                }

                if (poParam.LOTHER_TAX)
                {
                    lCancel = string.IsNullOrEmpty(poParam.COTHER_TAX_ID);

                    if (lCancel)
                    {
                        loEx.Add("", "Other Tax is required");
                    }
                }

                if (poParam.LWITHHOLDING_TAX)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CWITHHOLDING_TAX_TYPE);

                    if (lCancel)
                    {
                        loEx.Add("", "Withholding Tax type is required");
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CWITHHOLDING_TAX_ID);

                    if (lCancel)
                    {
                        loEx.Add("", "Withholding Tax is required");
                    }
                }

                if (Accrual)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CACCRUAL_METHOD);

                    if (lCancel)
                    {
                        loEx.Add("", "Accrual Method Name is required");
                    }
                }

               await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveChargesUtility(LMM01000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.LACCRUAL = Accrual;
                var loResult = await _LMM01000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                UtilityCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteChargesUtility(LMM01000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM01000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ActiveInactiveProcessAsync(LMM01000DTO poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                poParameter.CSTATUS = StatusChange;

                await _LMM01000Model.LMM01000ActiveInactiveAsync(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

    }

    public class RadioButton
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
