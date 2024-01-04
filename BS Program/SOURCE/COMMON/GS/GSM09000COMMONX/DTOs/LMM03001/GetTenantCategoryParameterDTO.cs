using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.LMM03001
{
    public class GetTenantCategoryParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CTENANT_CATEGORY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
