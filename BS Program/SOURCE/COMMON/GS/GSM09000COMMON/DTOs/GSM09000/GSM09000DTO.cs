    using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.GSM09000
{
    public class GSM09000DTO 
    {
        public string CCATEGORY_ID { get; set; } = "";
        public string CCATEGORY_NAME { get; set; } = "";
        public string CDISPLAY { get; set; } = "";
        public string CPARENT { get; set; } = "";
        public int ILEVEL { get; set; } = 0;
        public bool LHAS_CHILD { get; set; } = false;
    }
}
