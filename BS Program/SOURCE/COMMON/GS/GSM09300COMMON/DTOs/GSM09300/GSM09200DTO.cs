using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09300COMMON.DTOs.GSM09300
{
    public class GSM09300DTO
    {
        public string CCATEGORY_ID { get; set; } = "";
        public string CCATEGORY_NAME { get; set; } = "";
        public string CDISPLAY { get; set; } = "";
        public string CPARENT { get; set; } = "";
        public int ILEVEL { get; set; } = 0;
        public bool LHAS_CHILD { get; set; } = false;
    }
}
