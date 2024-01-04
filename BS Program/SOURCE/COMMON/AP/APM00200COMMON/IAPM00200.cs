using APM00200COMMON.DTOs.APM00200;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON
{
    public interface IAPM00200 : R_IServiceCRUDBase<APM00200ParameterDTO>
    {
        UpdateExpenditureActiveFlagResultDTO UpdateExpenditureActiveFlag(UpdateExpenditureActiveFlagParameterDTO poParameter);
        GetSelectedExpenditureResultDTO GetSelectedExpenditure(GetSelectedExpenditureParameterDTO poParameter);
        IAsyncEnumerable<APM00200DTO> GetExpenditureList();
        IAsyncEnumerable<GetWithholdingTaxTypeDTO> GetWithholdingTaxTypeList();
        IAsyncEnumerable<GetPropertyDTO> GetPropertyList();
        TemplateExpenditureDTO DownloadTemplateExpenditure();
    }
}
