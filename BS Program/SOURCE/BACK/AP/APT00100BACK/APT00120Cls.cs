﻿using APT00100COMMON.DTOs.APT00120;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APT00100COMMON.Loggers;

namespace APT00100BACK
{
    public class APT00120Cls
    {
        private LoggerAPT00120 _logger;
        public APT00120Cls()
        {
            _logger = LoggerAPT00120.R_GetInstanceLogger();
        }
        public void CloseFormProcess(OnCloseProcessParameterDTO poParam)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_GENERATE_WH_TAX_DEDUCTION " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "CLOGIN_USER_ID, " +
                    "@CREC_ID";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParam.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_DELETE_TRANS_HD {@Parameters} || R_Deleting(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
