﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04030
{
    public class LMM04030DTO
    {
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public string CBANK_CODE { get; set; } = "";
        public string CBANK_NAME { get; set; } = "";
        public string CBANK_ACCOUNT_NO { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; } = DateTime.Now;
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; } = DateTime.Now;
    }
}
