using BlazorClientHelper;
using GSM02200COMMON;
using GSM02200MODEL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM02200FRONT
{
    public partial class GSM02200 : R_Page
    {
        private GSM02200ViewModel _viewModel = new GSM02200ViewModel();
        private R_Grid<GSM02200DTO> _gridRef;
        private R_Conductor _conductorRef;

        [Inject] IJSRuntime JS { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }

        private string _lcLabelActivate = "Activate";
        private R_TextBox _CodeGeographyTextBox;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ShowInactiveCheckbox_OnChange(bool poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.ShowInactiveGeography = poParam;

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Geography 
        private async Task Geography_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetGeographyList();

                eventArgs.ListEntityResult = _viewModel.GeographyList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Geography_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02200DTO)eventArgs.Data;

                if (loParam.LACTIVE)
                {
                    _lcLabelActivate = "Inactive";
                    _viewModel.StatusChange = false;
                }
                else
                {
                    _lcLabelActivate = "Activate";
                    _viewModel.StatusChange = true;
                }
            }
        }

        private void Geography_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loData = (GSM02200DTO)eventArgs.Data;

            eventArgs.Allow = !string.IsNullOrWhiteSpace(loData.CCODE) & loData.LACTIVE;
        }

        private void Geography_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
           var loParentData =  _gridRef.CurrentSelectedData;

           var loData = (GSM02200DTO)eventArgs.Data;

            loData.CPARENT_CODE = loParentData.CCODE;
            loData.CPARENT_NAME = loParentData.CNAME;
        }
        private async Task Geography_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetGeography((GSM02200DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.Geography;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Geography_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.ValidationGeography((GSM02200DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Geography_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveGeography((GSM02200DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.Geography;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        private async Task Geography_TemplateBtn_OnClick()
        {
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _viewModel.DownloadTemplate();

                    var saveFileName = $"Geography Template.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task Geography_ActiveInactive_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM02200DTO)_conductorRef.R_GetCurrentData();

                var loValidate = await R_MessageBox.Show("", $"Are you sure {_lcLabelActivate} this?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.ActiveInactiveProcessAsync(loData);

                    if (_viewModel.ShowInactiveGeography)
                    {
                        await _conductorRef.R_SetCurrentData(_viewModel.Geography);
                    }
                    else
                    {
                        await _gridRef.R_RefreshGrid(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

       
        private void Geography_Upload_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM02201);
        }

        private async Task Geography_Upload_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
    }
}
