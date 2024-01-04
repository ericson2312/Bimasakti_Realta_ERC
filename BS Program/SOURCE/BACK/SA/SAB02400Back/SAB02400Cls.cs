using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using SAB02400Common.DTOs;
using System.Data.Common;

namespace SAB02400Back
{
    public class SAB02400Cls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            var loEx = new R_Exception();

            try
            {
                var liFinishFlag = 1; //0=Process, 1=Success, 9=Fail
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UserDTO>>(poBatchProcessPar.BigObject);

                R_Db loDb = new R_Db();

                //TODO Save to Database

                var lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                    $"'{poBatchProcessPar.Key.USER_ID}', " +
                    $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                    $"{loObject.Count}, 'Process Finish', {liFinishFlag}";

                loDb.SqlExecNonQuery(lcQuery);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void SampleErrorMultiLang()
        {
            var loEx = new R_Exception();
            DbConnection loConn = null;

            try
            {
                R_Db loDb = new R_Db();
                loConn = loDb.GetConnection();

                R_ExternalException.R_SP_Init_Exception(loConn);
                var lcQuery = $"EXEC SampleErrorMultiLang 1 ";

                try
                {
                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                    loConn.Close();
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}