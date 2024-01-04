using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04020
{
    public class LMM04020DTO
    {
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public string CTAX_TYPE { get; set; } = "";
        public bool LPPH { get; set; } = false;
        public string CTAX_ID { get; set; } = "";
        public string CTAX_NAME { get; set; } = "";
        public string CID_NO { get; set; } = "";
        public string CID_TYPE { get; set; } = "";
        public string CID_EXPIRED_DATE { get; set; } = "";
        public DateTime DID_EXPIRED_DATE { get; set; }
        public string CTAX_CODE { get; set; } = "";
        public decimal NTAX_CODE_LIMIT_AMOUNT { get; set; } = 0;
        public string CTAX_ADDRESS { get; set; } = "";
        public string CTAX_PHONE1 { get; set; } = "";
        public string CTAX_PHONE2 { get; set; } = "";
        public string CTAX_EMAIL { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; } = DateTime.Now;
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; } = DateTime.Now;

        public void Clear()
        {
            CTENANT_ID = "";
            CTENANT_NAME = "";
            CTAX_TYPE = "";
            LPPH = false;
            CTAX_ID = "";
            CTAX_NAME = "";
            CID_NO = "";
            CID_TYPE = "";
            CID_EXPIRED_DATE = "";
            DID_EXPIRED_DATE = default(DateTime);
            CTAX_CODE = "";
            NTAX_CODE_LIMIT_AMOUNT = 0;
            CTAX_ADDRESS = "";
            CTAX_PHONE1 = "";
            CTAX_PHONE2 = "";
            CTAX_EMAIL = "";
            CUPDATE_BY = "";
            DUPDATE_DATE = DateTime.Now;
            CCREATE_BY = "";
            DCREATE_DATE = DateTime.Now;
        }
    }
}
