﻿using LMM01000COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Transactions;
using System.Diagnostics;

namespace LMM01000BACK
{
    public class LMM01000UniversalCls
    {
        public List<LMM01000UniversalDTO> GetAllUsageRateMode(LMM01000UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01000UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', @CCOMPANY_ID , '_USAGE_RATE_MODE', '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01000UniversalDTO> GetAllRateType(LMM01000UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01000UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', @CCOMPANY_ID , '_RATE_TYPE', '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01000UniversalDTO> GetAdminFeeType(LMM01000UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01000UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', @CCOMPANY_ID , '_ADMIN_FEE_TYPE', '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01000UniversalDTO> GetAllChargesType(LMM01000UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01000UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', @CCOMPANY_ID , '_BS_UTILITY_CHARGES_TYPE', '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01000UniversalDTO> GetAllStatus(LMM01000UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01000UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', @CCOMPANY_ID , '_STATUS_FLAG', '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01000UniversalDTO> GetAllTaxExemptionCode(LMM01000UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01000UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', @CCOMPANY_ID , '_TAX_CODE', '07,08', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01000UniversalDTO> GetAllWithholdingTaxType(LMM01000UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01000UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', @CCOMPANY_ID , '_BS_TAX_TYPE', '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01000UniversalDTO> GetAllAccrualMethod(LMM01000UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01000UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', @CCOMPANY_ID , '_BS_ACCRUAL_METHOD', '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
