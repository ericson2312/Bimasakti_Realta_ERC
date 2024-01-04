using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System.Collections.Generic;
using System.Linq;

namespace LMM01000COMMON.Models
{
    public static class GenerateDataModel
    {
        public static LMM01000ResultPrintDTO DefaultDataUtility()
        {
            LMM01000ResultPrintDTO loData = new LMM01000ResultPrintDTO()
            {
                Title = "Utility Charges",
                Header = "JBMPC - Metro Park Residence",
                Column = new LMM01000ColumnPrintDTO()
            };


            int lnTop = 4;
            int lnHeader;
            int lnDetail;
            List<LMM01000PrintDTO> loCollection = new List<LMM01000PrintDTO>();
            for (int i = 1; i <= lnTop; i++)
            {
                lnHeader = (i % 2) + 1;
                for (int j = 1; j <= lnHeader; j++)
                {
                    lnDetail = (j % 3) + 1;
                    for (int k = 1; k <= lnDetail; k++)
                    {
                        loCollection.Add(new LMM01000PrintDTO()
                        {
                            CCHARGES_TYPE_DESCR = $"Type Desc {i}",

                            CCHARGES_ID = $"Charges Id {j}",
                            CCHARGES_NAME = $"Charges Name {j}",
                            CCHARGES_TYPE = $"Type {i}",
                            CSTATUS_DESCRIPTION = $"Status {j}",
                            LACCRUAL = (j % 2 == 0),
                            CUTILITY_JRNGRP_CODE = $"Journal Code {j}",
                            CUTILITY_JRNGRP_NAME = $"Jounal Name {j}",
                            LTAX_EXEMPTION = (i % 2 != 0),
                            CTAX_EXEMPTION_CODE = $"Tax Ex {j}",
                            ITAX_EXEMPTION_PCT = j,
                            LOTHER_TAX = (i % 2 == 0),
                            COTHER_TAX_ID = $"Other Tax {j}",
                            LWITHHOLDING_TAX = (j % 2 == 0),
                            CWITHHOLDING_TAX_TYPE = $"Withholding Tax Type {j}",
                            CWITHHOLDING_TAX_ID = $"Withholding Tax id {j}",
                            CGOA_CODE = $"GOA Code {k}",
                            CGOA_NAME = $"GOA Name {k}",
                            LDEPARTMENT_MODE = (k % 2 != 0),
                            CGLACCOUNT_NO = $"GLAccount Code {k}",
                            CGLACCOUNT_NAME = $"GLAccount Name {k}"
                        });
                    }
                }
            }

            var loTempData = loCollection
                .GroupBy(item => new { item.CCHARGES_TYPE_DESCR, item.CCHARGES_TYPE })
                .Select(group => new LMM01000TopPrintDTO
                {
                    CCHARGES_TYPE_DESCR = group.Key.CCHARGES_TYPE_DESCR,
                    DataCharges = group
                        .GroupBy(headerGroup => new
                        {
                            headerGroup.CCHARGES_TYPE,
                            headerGroup.CCHARGES_ID,
                            headerGroup.CCHARGES_NAME,
                            headerGroup.CSTATUS_DESCRIPTION,
                            headerGroup.LACCRUAL,
                            headerGroup.CUTILITY_JRNGRP_CODE,
                            headerGroup.CUTILITY_JRNGRP_NAME,
                            headerGroup.LTAX_EXEMPTION,
                            headerGroup.LOTHER_TAX,
                            headerGroup.CTAX_EXEMPTION_CODE,
                            headerGroup.ITAX_EXEMPTION_PCT,
                            headerGroup.COTHER_TAX_ID,
                            headerGroup.LWITHHOLDING_TAX,
                            headerGroup.CWITHHOLDING_TAX_TYPE,
                            headerGroup.CWITHHOLDING_TAX_ID
                        })
                        .Select(headerGroup => new LMM01000HeaderPrintDTO
                        {
                            CCHARGES_TYPE = headerGroup.Key.CCHARGES_TYPE,
                            CCHARGES_ID = headerGroup.Key.CCHARGES_ID,
                            CCHARGES_NAME = headerGroup.Key.CCHARGES_NAME,
                            CSTATUS_DESCRIPTION = headerGroup.Key.CSTATUS_DESCRIPTION,
                            LACCRUAL = headerGroup.Key.LACCRUAL,
                            CUTILITY_JRNGRP_CODE = headerGroup.Key.CUTILITY_JRNGRP_CODE,
                            CUTILITY_JRNGRP_NAME = headerGroup.Key.CUTILITY_JRNGRP_NAME,
                            LTAX_EXEMPTION = headerGroup.Key.LTAX_EXEMPTION,
                            LOTHER_TAX = headerGroup.Key.LOTHER_TAX,
                            ITAX_EXEMPTION_PCT = headerGroup.Key.ITAX_EXEMPTION_PCT,
                            LWITHHOLDING_TAX = headerGroup.Key.LWITHHOLDING_TAX,
                            CTAX_EXEMPTION_CODE = headerGroup.Key.CTAX_EXEMPTION_CODE,
                            COTHER_TAX_ID = headerGroup.Key.COTHER_TAX_ID,
                            CWITHHOLDING_TAX_TYPE = headerGroup.Key.CWITHHOLDING_TAX_TYPE,
                            CWITHHOLDING_TAX_ID = headerGroup.Key.CWITHHOLDING_TAX_ID,
                            DetailCharges = headerGroup
                                .Select(detail => new LMM01000DetailPrintDTO
                                {
                                    CGOA_CODE = detail.CGOA_CODE,
                                    CGOA_NAME = detail.CGOA_NAME,
                                    LDEPARTMENT_MODE = detail.LDEPARTMENT_MODE,
                                    CGLACCOUNT_NO = detail.CGLACCOUNT_NO,
                                    CGLACCOUNT_NAME = detail.CGLACCOUNT_NAME
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

            loData.Data = loTempData;

            return loData;
        }

        public static LMM01000ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Utility Charges",
                CUSER_ID = "FMC",
            };

            LMM01000ResultWithBaseHeaderPrintDTO loRtn = new LMM01000ResultWithBaseHeaderPrintDTO();
            loRtn.BaseHeaderData = loParam;
            loRtn.UtilitiesData = GenerateDataModel.DefaultDataUtility();

            return loRtn;
        }
    }

}
