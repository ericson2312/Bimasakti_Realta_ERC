﻿using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.LMM03001
{
    public class GetTenantCategoryResultDTO : R_APIResultBaseDTO
    {
        public GetTenantCategoryDTO Data { get; set; }
    }
}
