using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ContextFrontEnd;
using R_ProcessAndUploadFront;
using SAB02400Common.DTOs;
using SAB02400Model;

namespace SAB02400Front
{
    public partial class SAB02400 : R_Page, R_IProcessProgressStatus
    {
        [Inject] public IJSRuntime JS { get; set; }
        [Inject] public IClientHelper ClientHelper { get; set; }
        [Inject] private R_ContextHeader ContextHeader { get; set; }

        private R_ConductorGrid _conGridEmployeeRef;
        private R_Grid<UserDTO> _gridRef;
        private SAB02400ViewModel _viewModel = new();

        //protected override async Task R_Init_From_Master(object poParameter)
        //{
        //    var loGroupDescriptor = new List<R_GridGroupDescriptor>()
        //    {
        //        new R_GridGroupDescriptor() {FieldName = "Gender"}
        //    };

        //    await _gridRef.R_GroupBy(loGroupDescriptor);
        //}

        private void R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Parameter.GetType() == typeof(byte[]))
                    _viewModel.ReadExcel((byte[])eventArgs.Parameter);
                else
                    _viewModel.GetUserList();

                eventArgs.ListEntityResult = _viewModel.UserList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Display()
        {

        }

        private void R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (UserDTO)eventArgs.Data;

            loData.Id = Guid.NewGuid().ToString();
            loData.FirstName = "Sihol";
            loData.Gender = 1;
        }

        #region Lookup
        private void R_Before_Open_Grid_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            eventArgs.Parameter = "ea";
            eventArgs.TargetPageType = typeof(TestLookup);
        }

        private void R_After_Open_Grid_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnData != null)
            {
                var loData = (UserDTO)eventArgs.ColumnData;
                loData.FirstName = "Sihol";
            }
        }
        #endregion

        #region Upload
        private async Task OnSelect(/*FileSelectEventArgs args*/)
        {/*
            var loEx = new R_Exception();

            try
            {
                //import from excel
                var loBuffer = new byte[args.Files[0].Stream.Length];
                await args.Files[0].Stream.ReadAsync(loBuffer);

                await _gridRef.R_RefreshGrid(loBuffer);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();*/
        }

        private async Task OnClickBatch()
        {
            await _conGridEmployeeRef.R_SaveBatch();
        }

        private void R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            var loData = (List<UserDTO>)events.Data;

            events.Cancel = loData.Count == 0;
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //Read Excel
                //var loUploadClient = new R_ProcessAndUploadClient(poProcessProgressStatus: this);

                //var loBatchParameter = new R_BatchParameter()
                //{
                //    COMPANY_ID = ClientHelper.CompanyId,
                //    USER_ID = ClientHelper.UserId,
                //    ClassName = "SAB02400Back.SAB02400Cls",
                //    BigObject = eventArgs.Data
                //};

                //var loData = eventArgs.Data;
                //var liDataCount = ((List<UserDTO>)eventArgs.Data).ToList().Count;
                //await loUploadClient.R_BatchProcess<List<UserDTO>>(loBatchParameter, liDataCount);

                //Write Excel
                var loByteFile = _viewModel.WriteExcel((List<UserDTO>)eventArgs.Data);
                var saveFileName = $"{Guid.NewGuid().ToString()}.xlsx";

                await JS.downloadFileFromStreamHandler(saveFileName, loByteFile);
            }
            catch (R_APIException apiex)
            {
                apiex.ErrorList.ForEach(x => loEx.Add(x.ErrNo, x.ErrDescp));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {

        }

        public Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            return Task.CompletedTask;
        }

        public Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            return Task.CompletedTask;
        }

        public Task ReportProgress(int pnProgress, string pcStatus)
        {
            return Task.CompletedTask;
        }
        #endregion

        private async Task OnClickGenerate()
        {
            await _gridRef.R_RefreshGrid("generate");
        }

        private async Task OnClickWriteExcel()
        {
            await _conGridEmployeeRef.R_SaveBatch();
        }

        private async Task OnClickGrouping()
        {
            var loGroupDescriptor = new List<R_GridGroupDescriptor>()
            {
                new R_GridGroupDescriptor() {FieldName = "Gender"}
            };

            await _gridRef.R_GroupBy(loGroupDescriptor);
        }

        private async Task OnClickErrorSP()
        {
            var loEx = new R_Exception();

            try
            {
                ContextHeader.R_Context.R_SetContext("TEST", new UserDTO { Id = "aaa", FirstName = "sihol" });
                await _viewModel.SampleErrorMultiLangAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
