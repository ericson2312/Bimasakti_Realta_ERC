﻿using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
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
    public class GSM02540UnitPromotionTypeCls : R_BusinessObject<GSM02540ParameterDTO>
    {
        private LoggerGSM02540 _logger;
        public GSM02540UnitPromotionTypeCls()
        {
            _logger = LoggerGSM02540.R_GetInstanceLogger();
        }

        public void RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTION_TYPEMethod(GSM02500ActiveInactiveParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTION_TYPE " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@CUNIT_PROMOTION_TYPE_ID, " +
                                 $"@LACTIVE, " +
                                 $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_PROMOTION_TYPE_ID", DbType.String, 50, poEntity.CUNIT_PROMOTION_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTION_TYPE {@Parameters} || RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTION_TYPEMethod(Cls) ", loDbParam);

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

        private void RSP_GS_MAINTAIN_UNIT_PROMOTION_TYPEMethod(GSM02540ParameterDTO poEntity)
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

                lcQuery = $"EXEC RSP_GS_MAINTAIN_UNIT_PROMOTION_TYPE " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CUNIT_PROMOTION_TYPE_ID, " +
                                 $"@CUNIT_PROMOTION_TYPE_NAME, " +
                                 $"@LACTIVE, " +
                                 $"@CDESCRIPTION, " +
                                 $"@NGROSS_AREA_SIZE, " +
                                 $"@NNET_AREA_SIZE, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_PROMOTION_TYPE_ID", DbType.String, 50, poEntity.Data.CUNIT_PROMOTION_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_PROMOTION_TYPE_NAME", DbType.String, 50, poEntity.Data.CUNIT_PROMOTION_TYPE_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 10, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.Data.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@NGROSS_AREA_SIZE", DbType.Decimal, 10, poEntity.Data.NGROSS_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@NNET_AREA_SIZE", DbType.Decimal, 10, poEntity.Data.NNET_AREA_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_UNIT_PROMOTION_TYPE {@Parameters} || RSP_GS_MAINTAIN_UNIT_PROMOTION_TYPEMethod(Cls) ", loDbParam);

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

        public List<GSM02540DTO> GetUnitPromotionTypeList(GetUnitPromotionTypeListParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM02540DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_PROMOTION_TYPE " +
                    $"@CLOGIN_COMPANY_ID," +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CUNIT_PROMOTION_TYPE_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_PROMOTION_TYPE_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_PROMOTION_TYPE {@Parameters} || GetUnitPromotionTypeList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02540DTO>(loDataTable).ToList();

                loResult.ForEach(x => x.CUNIT_PROMOTION_TYPE_ID_NAME = x.CUNIT_PROMOTION_TYPE_NAME + '(' + x.CUNIT_PROMOTION_TYPE_ID + ')');
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Deleting(GSM02540ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_UNIT_PROMOTION_TYPEMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override GSM02540ParameterDTO R_Display(GSM02540ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02540ParameterDTO loResult = new GSM02540ParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_PROMOTION_TYPE " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CUNIT_PROMOTION_TYPE_ID, " +
                                 $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_PROMOTION_TYPE_ID", DbType.String, 50, poEntity.Data.CUNIT_PROMOTION_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_PROMOTION_TYPE {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02540DetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02540ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_UNIT_PROMOTION_TYPEMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
