using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using SAB02400Back;
using SAB02400Common;
using SAB02400Common.DTOs;

namespace SAB02400Service
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class SAB02400Controller : ControllerBase, ISAB02400
    {
        [HttpPost]
        public SAB02400ResultDTO SampleErrorMultiLang()
        {
            var loEx = new R_Exception();
            SAB02400ResultDTO loRtn = new();

            try
            {
                var loCls = new SAB02400Cls();

                loCls.SampleErrorMultiLang();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}