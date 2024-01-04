using LMM03500MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using LMM03500COMMON.DTOs.LMM03500;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;

namespace LMM03500FRONT
{
    public partial class LookUpCurrency : R_Page
    {
        [Inject] private R_ILocalizer<LMM03500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private LookUpCurrencyViewModel loViewModel = new LookUpCurrencyViewModel();

        private R_Grid<GetCurrencyDTO> _gridRef;

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

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetCurrencyListAsync();

                eventArgs.ListEntityResult = loViewModel.loCurrencyList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = _gridRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}
