﻿using R_APICommonDTO;

namespace LMM01000COMMON
{
    public class LMM01000UniversalDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }

        // result
        public string CCODE { get; set; } = "";
        public string CDESCRIPTION { get; set; }
    }


}
