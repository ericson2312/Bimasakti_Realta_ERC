﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GSM07000COMMON.DTOs
{
    public class GSM07011ParameterDTO
    {
        public GSM07011DTO Data { get; set; }
        public string LOGIN_COMPANY_ID { get; set; } = "";
        public string SELECTED_APPROVAL_CODE { get; set; } = "";
        public string LOGIN_USER_ID { get; set; } = "";
    }
}
