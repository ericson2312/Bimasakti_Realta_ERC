using GLM00400COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLM00400MODEL
{
    public class GLM00401ViewModel : R_ViewModel<GLM00400PrintParamDTO>
    {
        private GLM00400Model _GLM00400Model = new GLM00400Model();

        public List<GLM00400DTO> AllocationJournalFromGrid { get; set; } = new List<GLM00400DTO>();
        public List<GLM00400DTO> AllocationJournalToGrid { get; set; } = new List<GLM00400DTO>();

        public async Task GetAllocationJournalFromToList(GLM00400DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00400Model.GetAllocationJournalHDListAsync(poParam);

                AllocationJournalFromGrid = loResult;
                AllocationJournalToGrid = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationAllocation(GLM00400PrintParamDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CDEPT_CODE);
                if (lCancel)
                {
                    loEx.Add("", "Please select Department!");
                }

                lCancel = string.IsNullOrEmpty(poParam.CFROM_ALLOC_NO);
                if (lCancel)
                {
                    loEx.Add("", "Please fill in From Allocation No.");
                }

                lCancel = string.IsNullOrEmpty(poParam.CTO_ALLOC_NO);
                if (lCancel)
                {
                    loEx.Add("", "Please fill in To Allocation No.");
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
