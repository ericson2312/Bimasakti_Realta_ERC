using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.LMM03001
{
    public class GetTenantCategoryListResultDTO : R_APIResultBaseDTO
    {
        public List<GetTenantCategoryDTO> Data { get; set; }
    }
}
