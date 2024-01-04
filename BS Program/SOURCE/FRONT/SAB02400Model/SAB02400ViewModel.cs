using Bogus;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Excel;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using SAB02400Common.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SAB02400Model
{
    public class SAB02400ViewModel : R_ViewModel<UserDTO>
    {
        private SAB02400Model _model = new SAB02400Model();

        public ObservableCollection<UserDTO> UserList { get; set; } = new ObservableCollection<UserDTO>();
        public List<GenderDTO> GenderList = new List<GenderDTO>()
        {
            new GenderDTO{Code = 1, Description="Male"},
            new GenderDTO{Code = 2, Description="Female"}
        };

        public List<GenderDTO> DropDownGenderList = new List<GenderDTO>()
        {
            new GenderDTO{Code = 1, Description="Male"},
            new GenderDTO{Code = 2, Description="Female"}
        };

        public int DefaultDropDownValue = 0;

        public List<string> AllowedExtensions { get; set; } = new List<string>() { ".xlsx" };

        public void GetUserList()
        {
            var loEx = new R_Exception();

            try
            {
                var loFake = new Faker<UserDTO>()
                .CustomInstantiator(x => new UserDTO())
                .RuleFor(x => x.FirstName, x => x.Name.FirstName())
                .RuleFor(x => x.Gender, x => x.PickRandom(new[] { 1, 2 }))
                .RuleFor(x => x.Id, x => Guid.NewGuid().ToString());
                var loUserList = loFake.Generate(100);

                UserList = new ObservableCollection<UserDTO>(loUserList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ReadExcel(byte[] poExcelByte)
        {
            var loEx = new R_Exception();

            try
            {
                var loExcel = new R_Excel();
                var loDataSet = loExcel.R_ReadFromExcel(poExcelByte);

                var loResult = R_FrontUtility.R_ConvertTo<UserDTO>(loDataSet.Tables[0]);

                UserList = new ObservableCollection<UserDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public byte[] WriteExcel(List<UserDTO> poListData)
        {
            var loEx = new R_Exception();
            byte[] loResult = null;

            try
            {
                var loDataTable = R_FrontUtility.R_ConvertTo(poListData);

                var loExcel = new R_Excel();
                loResult = loExcel.R_WriteToExcel(loDataTable);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task SampleErrorMultiLangAsync()
        {
            var loEx = new R_Exception();

            try
            {
                await _model.SampleErrorMultiLangAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
