﻿using BlazorClientHelper;
using GLM00400COMMON;
using GLM00400MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GLM00400FRONT
{
    public partial class GLM00410 : R_Page
    {
        private GLM00410ViewModel _AllocationJournalDT_viewModel = new GLM00410ViewModel();
        private R_Conductor _AllocationJournalDT_conductorRef;
        private R_Grid<GLM00411DTO> _AllocationAccount_gridRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GLM00400DTO)poParameter;
                if (!string.IsNullOrWhiteSpace(loData.CALLOC_NO))
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GLM00410DTO>(poParameter);
                    loParam.CREC_ID_ALLOCATION_ID = loParam.CREC_ID;

                    await _AllocationJournalDT_conductorRef.R_GetEntity(loParam);
                    await _AllocationAccount_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task AllocationJournalDT_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _AllocationJournalDT_viewModel.GetAllocationJournalDT((GLM00410DTO)eventArgs.Data);

                eventArgs.Result = _AllocationJournalDT_viewModel.AllocationJournalDT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        private async Task Allocation_Account_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00411DTO>(eventArgs.Parameter);
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;

                await _AllocationJournalDT_viewModel.GetAllocationAccountList(loParam);

                eventArgs.ListEntityResult = _AllocationJournalDT_viewModel.AllocationAccountGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Allocation_OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00410DTO)_AllocationJournalDT_conductorRef.R_GetCurrentData();
                if (eventArgs.TabStripTab.Id == "Account")
                {
                    await _AllocationAccount_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Allocation_Depart_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL00700ParameterDTO();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void Allocation_Depart_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (GLM00410DTO)_AllocationJournalDT_conductorRef.R_GetCurrentData();

                loData.CDEPT_CODE = loTempResult.CDEPT_CODE;
                loData.CDEPT_NAME = loTempResult.CDEPT_NAME;
                loData.CSOURCE_CENTER_CODE = loTempResult.CCENTER_CODE;
                loData.CSOURCE_CENTER_NAME = loTempResult.CCENTER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Allocation_Center_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = (GLM00410DTO)_AllocationJournalDT_conductorRef.R_GetCurrentData();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GLM00411);
        }

        private void Allocation_Period_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = (GLM00410DTO)_AllocationJournalDT_conductorRef.R_GetCurrentData();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GLM00412);
        }


        private void Allocation_Account_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = (GLM00410DTO)_AllocationJournalDT_conductorRef.R_GetCurrentData();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GLM00430);
        }

        private async Task Allocation_Account_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loParam = (GLM00410DTO)_AllocationJournalDT_conductorRef.R_GetCurrentData();
            await _AllocationAccount_gridRef.R_RefreshGrid(loParam);
        }

        private void AllocationJournalDT_SetOther(R_SetEventArgs eventArgs)
        {
            _AllocationJournalDT_viewModel.OnCRUDMode = eventArgs.Enable;
        }
        private async Task AllocationJournalDT_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00410DTO)eventArgs.Data;
                
                if (loParam.LALLOW_EDIT == false)
                {
                    var loResult = await R_MessageBox.Show("", "Department’s center has been changed! Not allowed to modify!”", R_eMessageBoxButtonType.OK);
                    eventArgs.Cancel = loResult == R_eMessageBoxResult.OK;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task AllocationJournalDT_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await R_MessageBox.Show("", "You haven’t saved your changes. Are you sure want to cancel?”", R_eMessageBoxButtonType.YesNo);

                eventArgs.Cancel = loResult == R_eMessageBoxResult.No;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task AllocationJournalDT_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _AllocationJournalDT_viewModel.ValidationAllocationJournalDT((GLM00410DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private R_TabStrip _tabAllocationChill;
        private R_TabPage _CenterTabPage;
        private R_TabPage _PeriodTabPage;
        private async Task AllocationJournalDT_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _AllocationJournalDT_viewModel.SaveAllocationJournalDT((GLM00410DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _AllocationJournalDT_viewModel.AllocationJournalDT;

                if (_tabAllocationChill.ActiveTab.Id == "Account")
                {
                    await _AllocationJournalDT_conductorRef.R_GetEntity(_AllocationJournalDT_viewModel.AllocationJournalDT);

                    await _AllocationAccount_gridRef.R_RefreshGrid(_AllocationJournalDT_viewModel.AllocationJournalDT);
                }
                else if (_tabAllocationChill.ActiveTab.Id == "Center")
                {
                    await _CenterTabPage.InvokeRefreshTabPageAsync(_AllocationJournalDT_viewModel.AllocationJournalDT);
                }
                else if (_tabAllocationChill.ActiveTab.Id == "Period")
                {
                    await _PeriodTabPage.InvokeRefreshTabPageAsync(_AllocationJournalDT_viewModel.AllocationJournalDT);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task AllocationJournalDT_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _AllocationJournalDT_viewModel.DeleteAllocationJournalDT((GLM00410DTO)eventArgs.Data);

                if (_tabAllocationChill.ActiveTab.Id == "Account")
                {
                    await _AllocationJournalDT_conductorRef.R_GetEntity(_AllocationJournalDT_viewModel.AllocationJournalDT);

                    await _AllocationAccount_gridRef.R_RefreshGrid(_AllocationJournalDT_viewModel.AllocationJournalDT);
                }
                else if (_tabAllocationChill.ActiveTab.Id == "Center")
                {
                    await _CenterTabPage.InvokeRefreshTabPageAsync(_AllocationJournalDT_viewModel.AllocationJournalDT);
                }
                else if (_tabAllocationChill.ActiveTab.Id == "Period")
                {
                    await _PeriodTabPage.InvokeRefreshTabPageAsync(_AllocationJournalDT_viewModel.AllocationJournalDT);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
