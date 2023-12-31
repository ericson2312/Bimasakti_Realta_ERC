﻿using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using LMM01000COMMON;
using LMM01000MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace LMM01000FRONT
{
    public partial class LMM01000 : R_Page
    {
        private LMM01000ViewModel _General_viewModel = new LMM01000ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Grid<LMM01000UniversalDTO> _General_gridRef;
        private R_Grid<LMM01002DTO> _UtilityCharges_gridRef;

        private R_Conductor _General_conductorRef;
        private R_Conductor _UtilityCharges_conductorRef;

        private string _Genereal_lcLabel = "Activate";

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyDropdown_ServiceGetListRecord(poParameter);
                await UtilityCharges_Status_ServiceGetListRecord(null);
                await UtilityCharges_TaxExemption_ServiceGetListRecord(null);
                await UtilityCharges_WithholdingTax_ServiceGetListRecord(null);
                await _Universal_viewModel.GetAccrualMethodList();

                if (_General_viewModel.PropertyList.Count > 0)
                {
                    LMM01000DTOPropety loParam = _General_viewModel.PropertyList.FirstOrDefault();
                    _General_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                    await PropertyDropdown_OnChange(loParam.CPROPERTY_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private R_TabPage _tabPageRate;
        private R_TabStripTab _tabStripTabRate;
        private async Task PropertyDropdown_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _General_viewModel.GetPropertyList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();
            var loData = _General_gridRef.CurrentSelectedData;
            var loTempParamUtility = _UtilityCharges_gridRef.CurrentSelectedData;

            try
            {
                _General_viewModel.PropertyValueContext = poParam;
                await _General_gridRef.R_RefreshGrid(null);
                if (loData is not null && !string.IsNullOrWhiteSpace(_General_viewModel.PropertyValueContext))
                {
                    await _UtilityCharges_gridRef.R_RefreshGrid(loData);
                }

                if (_UtilityCharges_conductorRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    if (_TabGeneral.ActiveTab.Id == "Rate" )
                    {
                        if (loTempParamUtility is not null)
                        {
                            await _tabPageRate.InvokeRefreshTabPageAsync(loTempParamUtility);
                        }
                        else
                        {
                            await _tabPageRate.InvokeRefreshTabPageAsync(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ChargesTypeGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };
                await _General_viewModel.GetChargesTypeList(loParam);

                eventArgs.ListEntityResult = _General_viewModel.ChargesTypeGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ChargesTypeGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01000UniversalDTO>(eventArgs.Data);
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;
                await _General_viewModel.GetChargesType(loParam);

                eventArgs.Result = _General_viewModel.UtilityCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ChargesTypeGrid_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var loTempParam = _General_gridRef.CurrentSelectedData;

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (!string.IsNullOrWhiteSpace(_General_viewModel.PropertyValueContext))
                    {
                        await _UtilityCharges_gridRef.R_RefreshGrid(loTempParam);
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Utility 
        private R_TabStrip _TabGeneral;
        private async Task UtilityCharges_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (LMM01000DTO)eventArgs.Data;

                    if (loParam.CSTATUS != "80")
                    {
                        _Genereal_lcLabel = "Activate";
                        _General_viewModel.StatusChange = "80";
                    }
                    else
                    {
                        _Genereal_lcLabel = "Inactive";
                        _General_viewModel.StatusChange = "90";
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task UtilityCharges_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = R_FrontUtility.ConvertObjectToObject<LMM01000UniversalDTO>(eventArgs.Parameter);
                var loParam = new LMM01002DTO()
                {
                    CCHARGES_TYPE = loTempParam.CCODE
                };
                await _General_viewModel.GetChargesUtilityList(loParam);

                eventArgs.ListEntityResult = _General_viewModel.ChargesUtilityGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UtilityCharges_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01000DTO>(eventArgs.Data);
                await _General_viewModel.GetChargesUtility(loParam);

                eventArgs.Result = _General_viewModel.UtilityCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UtilityCharges_Status_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _Universal_viewModel.GetStatusList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task UtilityCharges_TaxExemption_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _Universal_viewModel.GetTaxExemptionList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task UtilityCharges_WithholdingTax_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _Universal_viewModel.GetWithHoldingTaxList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void UtilityCharges_OtherTax_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00100ParameterDTO();

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00100);
        }

        private void UtilityCharges_OtherTax_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            loData.COTHER_TAX_ID = loTempResult.CTAX_ID;
            loData.CTAX_OTHER_NAME = loTempResult.CTAX_NAME;
        }

        private void UtilityCharges_WithholdingTax_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00200ParameterDTO();
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            param.CPROPERTY_ID = _General_viewModel.PropertyValueContext;
            param.CTAX_TYPE_LIST = loData.CWITHHOLDING_TAX_TYPE;

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00200);
        }

        private void UtilityCharges_WithholdingTax_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            loData.CWITHHOLDING_TAX_ID = loTempResult.CTAX_ID;
            loData.CWITHHOLDING_TAX_NAME = loTempResult.CTAX_NAME;
            loData.NTAX_PERCENTAGE_WITHHOLDING = loTempResult.NTAX_PERCENTAGE;
        }

        private void UtilityCharges_JournalGrp_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00400ParameterDTO()
            {
                CPROPERTY_ID = _General_viewModel.PropertyValueContext,
                CJRNGRP_TYPE = "11"
            };

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private void UtilityCharges_JournalGrp_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();
            loData.CUTILITY_JRNGRP_CODE = loTempResult.CJRNGRP_CODE;
            loData.CUTILITY_JRNGRP_NAME = loTempResult.CJRNGRP_NAME;
            loData.LACCRUAL = loTempResult.LACCRUAL;
            _General_viewModel.Accrual = loTempResult.LACCRUAL;
        }

        private bool TaxExemptionEnable = false;
        private void Taxable_OnChnaged(bool poParam)
        {
            _General_viewModel.Data.LTAXABLE = poParam;

            TaxExemptionEnable = (bool)poParam;
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.LTAX_EXEMPTION = false;
                loData.CTAX_EXEMPTION_CODE = "";
                loData.ITAX_EXEMPTION_PCT = 0;
            }
        }

        private bool TaxExemptionCodeEnable = false;
        private void TaxExemption_OnChnaged(bool poParam)
        {
            _General_viewModel.Data.LTAX_EXEMPTION = poParam;

            TaxExemptionCodeEnable = (bool)poParam;
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.CTAX_EXEMPTION_CODE = "";
                loData.ITAX_EXEMPTION_PCT = 0;
            }
        }

        private bool OtherTaxLookupEnable = false;
        private void OtherTax_OnChnaged(bool poParam)
        {
            _General_viewModel.Data.LOTHER_TAX = poParam;

            OtherTaxLookupEnable = (bool)poParam;
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.COTHER_TAX_ID = "";
                loData.CTAX_OTHER_NAME = "";
            }
        }

        private bool WithholdingLookupEnable = false;
        private void WithholdingTax_OnChange(bool poParam)
        {
            _General_viewModel.Data.LWITHHOLDING_TAX = poParam;

            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();
            WithholdingLookupEnable = (bool)poParam;

            if (!(bool)poParam)
            {
                loData.CWITHHOLDING_TAX_TYPE = "";
                loData.CWITHHOLDING_TAX_ID = "";
                loData.CWITHHOLDING_TAX_NAME = "";
            }
        }

        private bool _accrualEnable = false;
        private void AccuralJournal_OnChanged(bool poParam)
        {
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            _General_viewModel.Accrual = poParam;
            _accrualEnable = poParam;

            if (!poParam)
            {
                loData.CACCRUAL_METHOD = "";
            }

        }

        private bool _EnableAddEdit = false;

        private void UtilityCharges_SetAdd(R_SetEventArgs eventArgs)
        {
            _EnableAddEdit = eventArgs.Enable;
        }

        private void UtilityCharges_SetEdit(R_SetEventArgs eventArgs)
        {
            _EnableAddEdit = eventArgs.Enable;
        }

        private void UtilityCharges_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            eventArgs.Allow = !string.IsNullOrEmpty(_General_viewModel.PropertyValueContext);
        }

        private async Task UtilityCharges_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01000DTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    if (!_General_viewModel.Accrual)
                    {
                        if (loData.LACCRUAL)
                        {
                            var loValidate = await R_MessageBox.Show("", "Are you sure to disabled Accrual Journal? ", R_eMessageBoxButtonType.YesNo);

                            eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
                        }
                    }
                }

                await _General_viewModel.ValidationUtility(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            _comboboxEnabled = eventArgs.Enable;
            _pageSupplierOnCRUDmode = !eventArgs.Enable;
        }
        private async Task UtilityCharges_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _General_viewModel.SaveChargesUtility((LMM01000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _General_viewModel.UtilityCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UtilityCharges_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _General_viewModel.DeleteChargesUtility((LMM01000DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool CopyNewBtnEnable = false;
        private bool ActiveInactiveBtnEnable = false;
        private bool PrintBtnEnable = false;
        private bool SetHasData = false;
        private void UtilityCharges_SetHasData(R_SetEventArgs eventArgs)
        {
            CopyNewBtnEnable = eventArgs.Enable;
            ActiveInactiveBtnEnable = eventArgs.Enable;
            PrintBtnEnable = eventArgs.Enable;
            SetHasData = eventArgs.Enable;
        }

        private async Task UtilityCharges_R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = (LMM01000DTO)_General_viewModel.R_GetCurrentData();

                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM01001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _General_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _General_conductorRef.R_SetCurrentData(loGetData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "LMM01001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

        }

        private async Task UtilityCharges_R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (LMM01000DTO)_General_viewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _General_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _General_conductorRef.R_SetCurrentData(loGetData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void UtilityCharges_R_Before_Open_Popup_Print(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = _General_viewModel.PropertyList.FirstOrDefault(p => p.CPROPERTY_ID == _General_viewModel.PropertyValueContext);

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LMM01002);
        }

        private async Task UtilityCharges_R_After_Open_Popup_Print(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (LMM01000DTO)_General_viewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        private void UtilityCharges_CopyFromBtn_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loData = (LMM01000DTO)_General_viewModel.R_GetCurrentData();
            var loParam = new LMM01003DTO()
            {
                CPROPERTY_ID = loData.CPROPERTY_ID,
                CCHARGES_TYPE = loData.CCHARGES_TYPE,
                CCURRENT_CHARGES_ID = loData.CCHARGES_ID,
                CCURRENT_CHARGES_NAME = loData.CCHARGES_NAME,

            };

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LMM01003);
        }

        private async Task UtilityCharges_CopyFromBtn_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loTempData = (LMM01003DTO)eventArgs.Result;

            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }

                var loData = new LMM01000DTO()
                {
                    CCHARGES_ID = loTempData.CNEW_CHARGES_ID,
                    CCHARGES_TYPE = loTempData.CCHARGES_TYPE,
                    CPROPERTY_ID = loTempData.CPROPERTY_ID,
                };

                var loTempParam = _General_gridRef.CurrentSelectedData;
                await _UtilityCharges_gridRef.R_RefreshGrid(loTempParam);

                await _UtilityCharges_conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #endregion
        private string _ChargesTypeVal = "";
        private string _ChargesIDVall = "";

        private void _General_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loTempParam = _General_gridRef.CurrentSelectedData;
            var loTempParamUtility = _UtilityCharges_gridRef.CurrentSelectedData;
            if (loTempParamUtility is not null)
            {
                eventArgs.Parameter = loTempParamUtility;
            }


            if (loTempParam.CCODE == "01" || loTempParam.CCODE == "02")
            {
                eventArgs.TargetPageType = typeof(LMM01010);
            }
            else if (loTempParam.CCODE == "03" || loTempParam.CCODE == "04")
            {
                eventArgs.TargetPageType = typeof(LMM01020);
            }
            else if (loTempParam.CCODE == "05" || loTempParam.CCODE == "06")
            {
                eventArgs.TargetPageType = typeof(LMM01030);
            }
            else if (loTempParam.CCODE == "07")
            {
                eventArgs.TargetPageType = typeof(LMM01040);
            }
            else if (loTempParam.CCODE == "08")
            {
                eventArgs.TargetPageType = typeof(LMM01050);
            }
        }

        private bool _comboboxEnabled = true;
        private bool _pageSupplierOnCRUDmode = false;
        private void R_TabEventCallback(object poValue)
        {
            _comboboxEnabled = (bool)poValue;
            _pageSupplierOnCRUDmode = !(bool)poValue;
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageSupplierOnCRUDmode;
        }

        private void UtilityCharges_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            TaxExemptionEnable = false;
            TaxExemptionCodeEnable = false;
            OtherTaxLookupEnable = false;
            WithholdingLookupEnable = false;
            _accrualEnable = false;
        }

        private void UtilityCharges_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            TaxExemptionEnable = false;
            TaxExemptionCodeEnable = false;
            OtherTaxLookupEnable = false;
            WithholdingLookupEnable = false;
            _accrualEnable = false; 
        }

    }
}
