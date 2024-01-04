using GSM00100Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GSM00100Model
{
    public class GSM00100ViewModel : R_ViewModel<GSM00100DTO>
    {
        private GSM00100Model _GSM00100Model = new GSM00100Model();
        public ObservableCollection<GSM00100DTOList> GridData { get; set; } = new ObservableCollection<GSM00100DTOList>();

        public GSM00100DTO CurrentSMTP { get; set; } = new GSM00100DTO();

        public async Task GetSMTPList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM00100Model.GetSMTPListAsync();
                GridData = new ObservableCollection<GSM00100DTOList>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSMTP(GSM00100DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM00100Model.R_ServiceGetRecordAsync(poEntity);

                CurrentSMTP = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveSMTP(GSM00100DTO poNewEntity, R_eConductorMode conductorMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM00100Model.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)conductorMode);

                CurrentSMTP = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteSMTP(GSM00100DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM00100Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
