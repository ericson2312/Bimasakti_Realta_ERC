﻿using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows.Input;
using LMM06500COMMON;

namespace LMM06500BACK
{
    public class LMM06500Cls : R_BusinessObject<LMM06500DTO>
    {
        public List<LMM06500DTOInitial> GetProperty(LMM06500DTOInitial poEntity)
        {
            var loEx = new R_Exception();
            List<LMM06500DTOInitial> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM06500DTOInitial>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM06500DTO> GetAllStaff(LMM06500DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM06500DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                string lcQuery = $"RSP_LM_GET_STAFF_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM06500DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override LMM06500DTO R_Display(LMM06500DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM06500DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_LM_GET_STAFF_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTAFF_ID", DbType.String, 50, poEntity.CSTAFF_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM06500DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(LMM06500DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();

            try
            {
                lcQuery = "RSP_LM_MAINTAIN_STAFF";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;


                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);

                loDb.R_AddCommandParameter(loCmd, "@CSTAFF_ID", DbType.String, 20, poNewEntity.CSTAFF_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTAFF_NAME", DbType.String, 100, poNewEntity.CSTAFF_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 7, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPOSITION", DbType.String, 2, poNewEntity.CPOSITION);
                loDb.R_AddCommandParameter(loCmd, "@CJOIN_DATE", DbType.String, 8, poNewEntity.CJOIN_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CSUPERVISOR", DbType.String, 20, poNewEntity.CSUPERVISOR);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL", DbType.String, 100, poNewEntity.CEMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CMOBILE_PHONE1", DbType.String, 30, poNewEntity.CMOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CMOBILE_PHONE2", DbType.String, 30, poNewEntity.CMOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CGENDER", DbType.String, 2, poNewEntity.CGENDER);
                loDb.R_AddCommandParameter(loCmd, "@CADDRESS", DbType.String, 255, poNewEntity.CADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@DINACTIVE_DATE", DbType.DateTime, 50, poNewEntity.DINACTIVE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CINACTIVE_NOTE", DbType.String, 255, poNewEntity.CINACTIVE_NOTE);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(LMM06500DTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");
            DbCommand loCmd = loDb.GetCommand();

            try
            {
                // set action delete
                poEntity.CACTION = "DELETE";

                lcQuery = "RSP_LM_MAINTAIN_STAFF";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);

                loDb.R_AddCommandParameter(loCmd, "@CSTAFF_ID", DbType.String, 20, poEntity.CSTAFF_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTAFF_NAME", DbType.String, 100, poEntity.CSTAFF_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 7, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPOSITION", DbType.String, 2, poEntity.CPOSITION);
                loDb.R_AddCommandParameter(loCmd, "@CJOIN_DATE", DbType.String, 8, poEntity.CJOIN_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CSUPERVISOR", DbType.String, 20, poEntity.CSUPERVISOR);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL", DbType.String, 100, poEntity.CEMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CMOBILE_PHONE1", DbType.String, 30, poEntity.CMOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CMOBILE_PHONE2", DbType.String, 30, poEntity.CMOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CGENDER", DbType.String, 2, poEntity.CGENDER);
                loDb.R_AddCommandParameter(loCmd, "@CADDRESS", DbType.String, 255, poEntity.CADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@DINACTIVE_DATE", DbType.DateTime, 50, poEntity.DINACTIVE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CINACTIVE_NOTE", DbType.String, 255, poEntity.CINACTIVE_NOTE);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }


        public void LMM06500ActiveInactiveSP(LMM06500DTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = $"RSP_LM_ACTIVE_INACTIVE_STAFF";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTAFF_ID", DbType.String, 50, poEntity.CSTAFF_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                loDb.SqlExecQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }


    }
}
