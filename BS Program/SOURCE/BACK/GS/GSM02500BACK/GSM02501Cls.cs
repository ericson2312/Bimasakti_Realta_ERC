﻿using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02501;
using GSM02500COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500BACK
{
    public class GSM02501Cls : R_BusinessObject<GSM02501ParameterDTO>
    {
        private LoggerGSM02501 _logger;
        public GSM02501Cls()
        {
            _logger = LoggerGSM02501.R_GetInstanceLogger();
        }

        public List<GSM02501PropertyDTO> GetPropertyList(GetPropertyListDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM02501PropertyDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_PROPERTY_LIST " +
                    $"@CCOMPANY_ID, " +
                    $"@CUSER_ID";
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_LIST {@Parameters} || GetPropertyList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02501PropertyDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(GSM02501ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_PROPERTYMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override GSM02501ParameterDTO R_Display(GSM02501ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02501ParameterDTO loResult = new GSM02501ParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_PROPERTY_DETAIL " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.Data.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02501DetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02501ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_PROPERTYMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        public void RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(GSM02500ActiveInactiveParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_ACTIVE_INACTIVE_PROPERTY " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@LACTIVE, " +
                    $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.String, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_PROPERTY {@Parameters} || RSP_GS_ACTIVE_INACTIVE_PROPERTYMethod(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        private void RSP_GS_MAINTAIN_PROPERTYMethod(GSM02501ParameterDTO poEntity)
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

                lcQuery = $"RSP_GS_MAINTAIN_PROPERTY " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@CPROPERTY_NAME, " +
                                 $"@CADDRESS, " +
                                 $"@CPROVINCE, " +
                                 $"@CCITY, " +
                                 $"@CDISTRICT, " +
                                 $"@CSUBDISTRICT, " +
                                 $"@CSALES_TAX_ID, " +
                                 $"@CCURRENCY, " +
                                 $"@CUOM, " +
                                 $"@LACTIVE, " +
                                 $"@CACTION, " +
                                 $"@CUSER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.Data.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_NAME", DbType.String, 50, poEntity.Data.CPROPERTY_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CADDRESS", DbType.String, 50, poEntity.Data.CADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CPROVINCE", DbType.String, 50, poEntity.Data.CPROVINCE);
                loDb.R_AddCommandParameter(loCmd, "@CCITY", DbType.String, 50, poEntity.Data.CCITY);
                loDb.R_AddCommandParameter(loCmd, "@CDISTRICT", DbType.String, 50, poEntity.Data.CDISTRICT);
                loDb.R_AddCommandParameter(loCmd, "@CSUBDISTRICT", DbType.String, 50, poEntity.Data.CSUBDISTRICT);
                loDb.R_AddCommandParameter(loCmd, "@CSALES_TAX_ID", DbType.String, 50, poEntity.Data.CSALES_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY", DbType.String, 50, poEntity.Data.CCURRENCY);
                loDb.R_AddCommandParameter(loCmd, "@CUOM", DbType.String, 50, poEntity.Data.CUOM);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_PROPERTY {@Parameters} || RSP_GS_MAINTAIN_PROPERTYMethod(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
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
