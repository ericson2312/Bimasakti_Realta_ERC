﻿using BlazorClientHelper;
using GLB00600COMMON;
using GLB00600MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace GLB00600FRONT
{
    public partial class GLB00600 : R_Page
    {
        private GLB00600ViewModel _CloseEntries_viewModel = new GLB00600ViewModel();
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await ClosingEntries_SystemParam_ServiceGetListRecord(null);

                if (string.IsNullOrWhiteSpace(_CloseEntries_viewModel.SystemParam.CCLOSE_DEPT_CODE))
                {
                    await R_MessageBox.Show("", $"You must maintain GL System Parameter for property {_CloseEntries_viewModel.SystemParam.CCOMPANY_ID}", R_eMessageBoxButtonType.OK);
                    await this.CloseProgram();
                }
                else
                {
                    if (_CloseEntries_viewModel.SystemParam.CSUSPENSE_ACCOUNT_NO == "")
                    {
                        await R_MessageBox.Show("", "Please setup Suspense Account No.", R_eMessageBoxButtonType.OK);
                        await this.CloseProgram();
                    }
                    else
                    {
                        if (_CloseEntries_viewModel.SystemParam.CRETAINED_ACCOUNT_NO == "")
                        {
                            await R_MessageBox.Show("", "Please setup Retained Account No.", R_eMessageBoxButtonType.OK);
                            await this.CloseProgram();
                        }
                        else
                        {
                            await ClosingEntries_InitialVar_ServiceGetListRecord(null);

                            var loCurrentPeriodRight = _CloseEntries_viewModel.SystemParam.CCURRENT_PERIOD.Substring(_CloseEntries_viewModel.SystemParam.CCURRENT_PERIOD.Length - 2);
                            int ValidationInitial = int.Parse(loCurrentPeriodRight);

                            if (ValidationInitial != _CloseEntries_viewModel.InitialVar.INO_PERIOD)
                            {
                                await R_MessageBox.Show("", "This program is only for Year End Process!", R_eMessageBoxButtonType.OK);
                                await this.CloseProgram();
                            }
                            else
                            {
                                await ClosingEntries_SuspenAmound_ServiceGetListRecord(null);

                                if (_CloseEntries_viewModel.SuspenseAmountDTO.NSUSPENSE != 0)
                                {
                                    await R_MessageBox.Show("", "Suspense Account journal found!", R_eMessageBoxButtonType.OK);
                                    await this.CloseProgram();
                                }
                                else
                                {
                                    await ClosingEntries_GSMTransactionCode_ServiceGetListRecord(null);

                                    if (_CloseEntries_viewModel.SystemParam.CSOFT_PERIOD == _CloseEntries_viewModel.SystemParam.CCURRENT_PERIOD)
                                    {
                                        await R_MessageBox.Show("", "Soft Close GL Period should be done prior to Close GL Period", R_eMessageBoxButtonType.OK);
                                        await this.CloseProgram();
                                    }
                                }
                            }
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

        private async Task ClosingEntries_InitialVar_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLB00600InitialDTO() { CYEAR = _CloseEntries_viewModel.SystemParam.CCURRENT_PERIOD.Substring(0, 4) };
                await _CloseEntries_viewModel.GetInitialVar(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ClosingEntries_SystemParam_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _CloseEntries_viewModel.GetSystemParam();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ClosingEntries_SuspenAmound_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLB00600SuspenseAmountDTO() { CGLACCOUNT_NO = _CloseEntries_viewModel.SystemParam.CSUSPENSE_ACCOUNT_NO, CPERIOD = _CloseEntries_viewModel.SystemParam.CCURRENT_PERIOD };
                await _CloseEntries_viewModel.GetSsuspenAmount(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ClosingEntries_GSMTransactionCode_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLB00600GSMTransactionCodeDTO();
                await _CloseEntries_viewModel.GetGSMTransactionCode(loParam);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ClosingEntries_ValidationResult_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLB00600DTO();
                await _CloseEntries_viewModel.GetGSMValidationResult(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task Button_OnClickPeriodEndAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!_CloseEntries_viewModel.GSMTransactionCode.LINCREMENT_FLAG)
                {
                    await R_MessageBox.Show("", "Cannot process Closing Entries Journal with Manual Numbering! Transaction numbering setting for Closing Entries Journal should be auto increment, not manual numbering!", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    await ClosingEntries_ValidationResult_ServiceGetListRecord(null);

                    if (_CloseEntries_viewModel.ResultClose.Any(x => x.CDEPT_CODE != _CloseEntries_viewModel.SystemParam.CCLOSE_DEPT_CODE))
                    {
                        await R_MessageBox.Show("", $"User {clientHelper.UserId} does not have access to department {_CloseEntries_viewModel.SystemParam.CCLOSE_DEPT_CODE}", R_eMessageBoxButtonType.OK);
                        await this.CloseProgram();
                    }
                    else
                    {
                        var loConfirmation = await R_MessageBox.Show("", "Are you sure want to proceed Closing Entries?", R_eMessageBoxButtonType.YesNo);

                        if (loConfirmation == R_eMessageBoxResult.Yes)
                        {
                            var loParam = new GLB00600DTO();
                            await _CloseEntries_viewModel.GetGSMResult(loParam);

                            await R_MessageBox.Show("", "Closing Entries processed successfully!", R_eMessageBoxButtonType.OK);

                            await this.CloseProgram();
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
        public async Task Button_OnClickCloseAsync()
        {
            await this.CloseProgram();
        }
    }
}
