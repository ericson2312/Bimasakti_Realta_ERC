using LMM03500COMMON.DTOs.LMM03501;
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
using LMM03500COMMON;

namespace LMM03500BACK
{
    public class ValidateUploadTenantCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            string lcQuery;
            string lcCmd;
            Dictionary<string, string> loMapping = new Dictionary<string, string>();
            List<UploadTenantDTO> loResult = null;
            List<UploadTenantSaveDTO> loParam = null;
            int count = 1;
            string PropertyId;

            try
            {
                DbConnection loConn = null;
                DbCommand loCmd = null;
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadTenantDTO>>(poBatchProcessPar.BigObject);


                loParam = new List<UploadTenantSaveDTO>();

                foreach (var item in loTempObject)
                {
                    loParam.Add(new UploadTenantSaveDTO()
                    {
                        NO = count,
                        TenantId = item.TenantId,
                        TenantName = item.TenantName,
                        Address = item.Address,
                        Email = item.Email,
                        PhoneNo1 = item.PhoneNo1,
                        PhoneNo2 = item.PhoneNo2,
                        TenantGroup = item.TenantGroup,
                        TenantCategory = item.TenantCategory,
                        TenantType = item.TenantType,
                        JournalGroup = item.JournalGroup,
                        PaymentTerm = item.PaymentTerm,
                        Currency = item.Currency,
                        Salesman = item.Salesman,
                        LineOfBusiness = item.LineOfBusiness,
                        FamilyCard = item.FamilyCard
                    });
                    count++;
                };

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_TENANT_PROPERTY_ID_CONTEXT)).FirstOrDefault().Value;
                PropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        loConn = loDb.GetConnection();
                        loCmd = loDb.GetCommand();

                        lcQuery = $"CREATE TABLE #TENANT " +
                            $"(NO INT, " +
                            $"TenantId VARCHAR(20), " +
                            $"TenantName NVARCHAR(100), " +
                            $"Address NVARCHAR(255), " +
                            $"Email VARCHAR(100), " +
                            $"PhoneNo1 VARCHAR(30), " +
                            $"PhoneNo2 VARCHAR(30), " +
                            $"TenantGroup VARCHAR(20), " +
                            $"TenantCategory VARCHAR(20), " +
                            $"TenantType VARCHAR(20), " +
                            $"JournalGroup VARCHAR(8), " +
                            $"PaymentTerm VARCHAR(8), " +
                            $"Currency CHAR(3), " +
                            $"Salesman VARCHAR(8), " +
                            $"LineOfBusiness VARCHAR(20), " +
                            $"FamilyCard VARCHAR(40))";

                        loDb.SqlExecNonQuery(lcQuery, loConn, false);

                        loDb.R_BulkInsert<UploadTenantSaveDTO>((SqlConnection)loConn, "#TENANT", loParam);

                        lcQuery = $"EXEC RSP_LM_VALIDATE_UPLOAD_TENANT " +
                            $"@CCOMPANY_ID, " +
                            $"@CPROPERTY_ID, " +
                            $"@CUSER_ID, " +
                            $"@KEY_GUID, " +
                            $"'01'";

                        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
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
                    loException.ThrowExceptionIfErrors();
                    TransScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public List<UploadTenantErrorDTO> GetErrorProcess(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            var loEx = new R_Exception();
            List<UploadTenantErrorDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<UploadTenantErrorDTO>(loDataTableResult).ToList();
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
