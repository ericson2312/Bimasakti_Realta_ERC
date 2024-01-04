using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using GLB09900COMMON;

namespace GLB09900BACK
{
    public class GLB09900Cls
    {
        public GLB09900InitialDTO GetInitial(GLB09900InitialDTO poEntity)
        {
            var loEx = new R_Exception();
            GLB09900InitialDTO loResult = poEntity;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT IMIN_YEAR=CAST(MIN(CYEAR) AS INT) " +
                   $",IMAX_YEAR=CAST(MAX(CYEAR) AS INT) " +
                   $",dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID) AS DTODAY " +
                   $"FROM GSM_PERIOD (NOLOCK) " +
                   $"WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                var loTempResult = R_Utility.R_ConvertTo<GLB09900InitialDTO>(loDataTable).FirstOrDefault();

                loResult.DTODAY = loTempResult.DTODAY;
                loResult.IMAX_YEAR = loTempResult.IMAX_YEAR;
                loResult.IMIN_YEAR = loTempResult.IMIN_YEAR;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLB09900GLSystemParamDTO GetSystemParam(GLB09900GLSystemParamDTO poEntity)
        {
            var loEx = new R_Exception();
            GLB09900GLSystemParamDTO loResult = null;

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

                var loTempResult = R_Utility.R_ConvertTo<GLB09900GLSystemParamDTO>(loDataTable).FirstOrDefault();

                loTempResult.CURRENT_PERIOD_DISPLAY = loTempResult.CCURRENT_PERIOD.Substring(0, 4) + "-" + loTempResult.CCURRENT_PERIOD.Substring(4, 2);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public GLB09900ValidateDTO GetValidateResultClosePeriod(GLB09900ValidateDTO poEntity)
        {
            var loEx = new R_Exception();
            GLB09900ValidateDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GL_VALIDATE_CLOSE_GL_PERIOD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 50, poEntity.CPERIOD);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                var loTempResult = R_Utility.R_ConvertTo<GLB09900ValidateDTO>(loDataTable).FirstOrDefault();

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLB09900DTO GetResultClosePeriod(GLB09900DTO poEntity)
        {
            var loEx = new R_Exception();
            GLB09900DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GL_CLOSE_PERIOD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 50, poEntity.CPERIOD);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                var loTempResult = R_Utility.R_ConvertTo<GLB09900DTO>(loDataTable).FirstOrDefault();

                loResult = loTempResult;
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
