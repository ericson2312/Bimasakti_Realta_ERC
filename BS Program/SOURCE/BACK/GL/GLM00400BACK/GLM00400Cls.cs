using GLM00400COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace GLM00400BACK
{
    public class GLM00400Cls
    {
        public GLM00400InitialDTO GetInitial(GLM00400InitialDTO poEntity)
        {
            var loEx = new R_Exception();
            GLM00400InitialDTO loResult = poEntity;
            string lcQuery;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            GLM00400InitialDTO loTempResult = null;
            DataTable loDataTable = null;
            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = $"RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 50, poEntity.CMODE);

                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loTempResult = R_Utility.R_ConvertTo<GLM00400InitialDTO>(loDataTable).FirstOrDefault();

                loResult.IMAX_YEAR = loTempResult.IMAX_YEAR;
                loResult.IMIN_YEAR = loTempResult.IMIN_YEAR;

                lcQuery = "SELECT dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID) AS DTODAY";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loTempResult = R_Utility.R_ConvertTo<GLM00400InitialDTO>(loDataTable).FirstOrDefault();

                loResult.DTODAY = loTempResult.DTODAY;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
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
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLM00400GLSystemParamDTO GetSystemParam(GLM00400GLSystemParamDTO poEntity)
        {
            var loEx = new R_Exception();
            GLM00400GLSystemParamDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GL_GET_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00400GLSystemParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLM00400DTO> GetAllAllocationJournalHD(GLM00400DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00400DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GL_GET_ALLOCATION_HD_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00400DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLM00400PrintHDDTO> GetAllPrintAllocationHeader(GLM00400PrintParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00400PrintHDDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GL_REP_ALLOCATION_HD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_ALLOC_NO", DbType.String, 50, poEntity.CFROM_ALLOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTO_ALLOC_NO", DbType.String, 50, poEntity.CTO_ALLOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00400PrintHDDTO>(loDataTable).ToList();
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLM00400PrintAccountDTO> GetAllPrintAllocationAccount(GLM00400PrintParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00400PrintAccountDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GL_REP_ALLOCATION_ACCOUNT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 50, poEntity.CALLOC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00400PrintAccountDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<GLM00400PrintCenterDTO> GetAllPrintAllocationAccountCenter(GLM00400PrintParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00400PrintCenterDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GL_REP_ALLOCATION_CENTER";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 50, poEntity.CALLOC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00400PrintCenterDTO>(loDataTable).ToList();

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
