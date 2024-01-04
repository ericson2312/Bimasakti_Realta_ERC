using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.LMM03001
{
    public class LMM03001ResultDTO : R_APIResultBaseDTO
    {
        public List<LMM03001DTO> Data { get; set; } 
    }
}
