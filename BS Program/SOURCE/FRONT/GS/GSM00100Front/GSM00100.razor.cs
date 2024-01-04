using BlazorClientHelper;
using GSM00100Common;
using GSM00100Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GSM00100Front
{
    public partial class GSM00100 : R_Page
    {
        private GSM00100ViewModel SMTPViewModel = new GSM00100ViewModel();

        private R_Conductor _conductorRef;

        private R_Grid<GSM00100DTOList> _gridRef;

        [Inject] private IClientHelper _clientHelper { get; set; }

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

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var a = _clientHelper.CompanyId;
                var b = _clientHelper.UserId;

                await SMTPViewModel.GetSMTPList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Conductor
        #region ADD
        private void Conductor_R_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
        }

        private void Conductor_R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (GSM00100DTO)eventArgs.Data;

            loData.LSUPPORT_SSL = true;
            loData.CCREATE_BY = _clientHelper.UserId;
            loData.CUPDATE_BY = _clientHelper.UserId;
            loData.DCREATE_DATE = DateTime.Now;
            loData.DUPDATE_DATE = DateTime.Now;
            loData.LEDIT_CREDENTIAL = true;
        }
        #endregion

        private void Conductor_R_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
        }

        #region Delete
        private void Conductor_R_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            //var loEx = new R_Exception();

            //try
            //{
            //    var loResult = await _clientWrapper.CheckDeleteAsync((GSM00100DTO)eventArgs.Data);
            //    eventArgs.Cancel = loResult.Data;
            //}
            //catch (Exception ex)
            //{
            //    loEx.Add(ex);
            //}

            //loEx.ThrowExceptionIfErrors();
        }

        private async Task Conductor_R_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await SMTPViewModel.DeleteSMTP((GSM00100DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Conductor_R_AfterDelete()
        {
        }
        #endregion

        private void Conductor_R_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.Data == null)
                return;

            var loData = (GSM00100DTO)eventArgs.Data;

            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Add ||
                eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {

            }
        }

        private void Conductor_R_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
        }

        private async Task Conductor_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM00100DTO>(eventArgs.Data);

                await SMTPViewModel.GetSMTP(loParam);
                eventArgs.Result = SMTPViewModel.CurrentSMTP;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region SAVE
        private void Conductor_R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM00100DTO)eventArgs.Data;

                if (string.IsNullOrWhiteSpace(loData.CSMTP_ID))
                    loEx.Add(GetErrorFromResource("_err001"));

                if (string.IsNullOrWhiteSpace(loData.CSMTP_SERVER))
                    loEx.Add(GetErrorFromResource("_err002"));

                if (string.IsNullOrWhiteSpace(loData.CSMTP_PORT))
                    loEx.Add(GetErrorFromResource("_err003"));

                if (string.IsNullOrWhiteSpace(loData.CGENERAL_EMAIL_ADDRESS))
                {
                    loEx.Add(GetErrorFromResource("_err007"));
                }
                else
                {
                    if (!IsEmail(loData.CGENERAL_EMAIL_ADDRESS))
                        loEx.Add(GetErrorFromResource("_err008"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Conductor_R_Saving(R_SavingEventArgs eventArgs)
        {
            var loParam = (GSM00100DTO)eventArgs.Data;

            loParam.CUSER_ID = _clientHelper.UserId;
            loParam.CDATENOW = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private async Task Conductor_R_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await SMTPViewModel.SaveSMTP((GSM00100DTO)(eventArgs.Data), eventArgs.ConductorMode);

                eventArgs.Result = SMTPViewModel.CurrentSMTP;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Conductor_R_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
        }

        private void Conductor_R_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
        }
        #endregion
        #endregion

        private R_Error GetErrorFromResource(string pcMessageId)
        {
            return R_FrontUtility.R_GetError(typeof(GSM00100FrontResources.Resources_Dummy_Class), pcMessageId);
        }

        private bool IsEmail(string pcEmail)
        {
            return R_FrontUtility.IsValidEmail(pcEmail);
        }

        //private void Popup_Before_Open(R_BeforeOpenPopupEventArgs eventArgs)
        //{
        //    eventArgs.FormAccess = "A,U,D,P,V";
        //    eventArgs.TargetPageType = typeof(GSM00100);
        //    eventArgs.Parameter = "ea";
        //}

        //private void Popup_After_Open(R_AfterOpenPopupEventArgs eventArgs)
        //{
        //    //R_MessageBox.Show("ea", (string)eventArgs.Result, R_eMessageBoxButtonType.OK);
        //}

        //#region DETAIL
        //private void R_Before_Open_Form(R_BeforeOpenDetailEventArgs eventArgs)
        //{
        //    eventArgs.TargetPageType = typeof(TestPopup);
        //    eventArgs.Parameter = "ea";
        //}
        //#endregion
    }
}
