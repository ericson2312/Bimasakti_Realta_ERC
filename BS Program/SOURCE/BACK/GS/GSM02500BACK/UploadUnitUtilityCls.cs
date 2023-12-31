﻿using GSM02500COMMON.DTOs.GSM02530;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using GSM02500COMMON.DTOs.GSM02531;
using GSM02500COMMON.DTOs;

namespace GSM02500BACK
{
    public class UploadUnitUtilityCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

                while (!loTask.IsCompleted)
                {
                    Thread.Sleep(100);
                }

                if (loTask.IsFaulted)
                {
                    loException.Add(loTask.Exception.InnerException != null ?
                        loTask.Exception.InnerException :
                        loTask.Exception);

                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:

            loException.ThrowExceptionIfErrors();
        }

        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_Exception loException = new R_Exception();
            string lcQuery = "";

            try
            {
                await Task.Delay(100);
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
            
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadUnitUtilityDTO>>(poBatchProcessPar.BigObject);
                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_UNIT_UTILITY_PROPERTY_ID_CONTEXT)).FirstOrDefault().Value;
                string PropertyId = ((System.Text.Json.JsonElement)loVar).GetString();
                var loVar1 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_UNIT_UTILITY_BUILDING_ID_CONTEXT)).FirstOrDefault().Value;
                string BuildingId = ((System.Text.Json.JsonElement)loVar1).GetString();
                var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_UNIT_UTILITY_FLOOR_ID_CONTEXT)).FirstOrDefault().Value;
                string FloorId = ((System.Text.Json.JsonElement)loVar2).GetString();
                var loVar4 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_UNIT_UTILITY_UNIT_ID_CONTEXT)).FirstOrDefault().Value;
                string UnitId = ((System.Text.Json.JsonElement)loVar4).GetString();


                List<UploadUnitUtilitySaveDTO> loParam = new List<UploadUnitUtilitySaveDTO>();

                loParam = loObject.Select(item => new UploadUnitUtilitySaveDTO()
                {
                    NO = item.No,
                    CUTILITY_TYPE = item.UtilityType,
                    CSEQUENCE = item.SeqNo,
                    CMETER_NO = item.MeterNo,
                    NCALCULATION_FACTOR = item.CalculationFactor,
                    NCAPACITY = item.Capacity,
                    IMETER_MAX_RESET = item.MaxResetValue,
                    Active = item.Active,
                    NonActiveDate = item.NonActiveDate
                }).ToList();

                lcQuery = $"CREATE TABLE #UNIT_UTILITIES " +
                    $"(NO INT, " +
                    $"CUTILITY_TYPE VARCHAR(2), " +
                    $"CSEQUENCE VARCHAR(3), " +
                    $"CMETER_NO VARCHAR(50), " +
                    $"NCALCULATION_FACTOR NUMERIC(18,2), " +
                    $"NCAPACITY NUMERIC(18,2), " +
                    $"IMETER_MAX_RESET INT, " +
                    $"Active BIT, " +
                    $"NonActiveDate VARCHAR(8))";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadUnitUtilitySaveDTO>((SqlConnection)loConn, "#UNIT_UTILITIES", loParam);

                lcQuery = $"EXEC RSP_GS_UPLOAD_BUILDING_UNIT_UTILITIES " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CBUILDING_ID, " +
                    $"@CFLOOR_ID, " +
                    $"@CUNIT_ID, " +
                    $"@CUSER_ID, " +
                    $"@KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, BuildingId);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 50, FloorId);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_ID", DbType.String, 50, UnitId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loCmd.CommandText = lcQuery;
                loDb.SqlExecNonQuery(loConn, loCmd, false);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
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
            if (loException.Haserror)
            {
                lcQuery = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                            $"VALUES " +
                            $"('{poBatchProcessPar.Key.COMPANY_ID}', " +
                            $"'{poBatchProcessPar.Key.USER_ID}', " +
                            $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                            $"-100, " +
                            $"'{loException.ErrorList[0].ErrDescp}');";

                loDb.SqlExecNonQuery(lcQuery);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                    $"'{poBatchProcessPar.Key.USER_ID}', " +
                    $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                    $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                loDb.SqlExecNonQuery(lcQuery);
            }
        }
    }
}
