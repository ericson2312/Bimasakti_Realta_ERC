using GLR00200COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml;

namespace GLR00200MODEL
{
    public class GLR00200ViewModel : R_ViewModel<GLR00200PrintParamDTO>
    {
        private GLR00200Model _GLR00200Model = new GLR00200Model();

        public GLR00200InitialDTO InitialVar = new GLR00200InitialDTO();
        public GLR00200GLSystemParamDTO SystemParam = new GLR00200GLSystemParamDTO();
        public List<GLR00200UniversalDTO> GetPrintMethodList = new List<GLR00200UniversalDTO>();

        public string FromAccountName = "";
        public string ToAccountName = "";
        public string FromCenterName = "";
        public string ToCenterName = "";
        public bool LPrintbyCenter = false;
        public int IYEAR;
        public DateTime IFROMDATE;
        public DateTime ITODATE;

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLR00200Model.GetInitialVarAsync();

                InitialVar = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParam()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLR00200Model.GetSystemParamAsync();

                SystemParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPrintMethod()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLR00200Model.GetPrintMethodListAsync();

                GetPrintMethodList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationGLAccountLedger(GLR00200PrintParamDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CFROM_ACCOUNT_NO);
                if (lCancel)
                {
                    loEx.Add("", "Please select From Account No.");
                }

                lCancel = string.IsNullOrEmpty(poParam.CTO_ACCOUNT_NO);

                if (lCancel)
                {
                    loEx.Add("", "Please select To Account No.");
                }

                if (LPrintbyCenter)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CFROM_CENTER_CODE) ;

                    if (lCancel)
                    {
                        loEx.Add("", "Please select from Center Code!");
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CTO_CENTER_CODE);

                    if (lCancel)
                    {
                        loEx.Add("", "Please select to Center Code!");
                    }
                }

                if (poParam.CPERIOD_MODE == "P")
                {
                    lCancel = string.IsNullOrEmpty(poParam.CFROM_PERIOD_NO);

                    if (lCancel)
                    {
                        loEx.Add("", "Please select from Period Code!");
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CTO_PERIOD_NO);

                    if (lCancel)
                    {
                        loEx.Add("", "Please select to Period Code!");
                    }
                }
                else if (poParam.CPERIOD_MODE == "D")
                {
                    lCancel = string.IsNullOrEmpty(poParam.CFROM_DATE);

                    if (lCancel)
                    {
                        loEx.Add("", "From Date is required!");
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CTO_DATE);

                    if (lCancel)
                    {
                        loEx.Add("", "To Date is required!");
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

        // Current Type Radio Button
        public List<RadioModel> CurrentTypeList { get; set; } = new List<RadioModel>
        {
            new RadioModel { Id = "L", Text = "Local Currency"},
            new RadioModel { Id = "B", Text = "Base Currency" },
        };

        // Period Type Radio Button
        public List<RadioModel> PeriodTypeList { get; set; } = new List<RadioModel>
        {
            new RadioModel { Id = "P", Text = "Period"},
            new RadioModel { Id = "D", Text = "Date" },
        };

        // Period Type Radio Button
        public List<RadioModel> PeriodFromToList { get; set; } = GenerateFromToList();

        private static List<RadioModel> GenerateFromToList()
        {
            List<RadioModel> dummyData = new List<RadioModel>();

            for (int i = 1; i <= 98; i++)
            {
                string id = i.ToString("D2"); // Format as "01", "02", ..., "98"
                string text = i.ToString("D2");
                dummyData.Add(new RadioModel { Id = id, Text = text });
            }

            dummyData.Add(new RadioModel { Id = "99", Text = "Closing" });
            return dummyData;
        }
    }

public class RadioModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
