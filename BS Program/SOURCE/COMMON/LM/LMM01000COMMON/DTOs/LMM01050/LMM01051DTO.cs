﻿namespace LMM01000COMMON
{
    public class LMM01051DTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CDAY_TYPE { get; set; }
        public string CUSER_ID { get; set; }

        // result
        public int ILEVEL { get; set; }
        public string CLEVEL_DESC { get; set; }
        public int IHOURS_FROM { get; set; }
        public int IHOURS_TO { get; set; }
        public decimal NRATE_HOUR { get; set; }
    }

    public class LMM01051SaveBatchDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CDAY_TYPE { get; set; }

        // result
        public int ILEVEL { get; set; }
        public string CLEVEL_DESC { get; set; }
        public int IHOURS_FROM { get; set; }
        public int IHOURS_TO { get; set; }
        public decimal NRATE_HOUR { get; set; }
    }
}
