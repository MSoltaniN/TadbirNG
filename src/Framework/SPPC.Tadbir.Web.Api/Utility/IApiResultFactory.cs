using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SPPC.Tadbir.Web.Api.Utility
{
    public interface IApiResultFactory
    {
        OkResult Ok();

        OkObjectResult Ok(object value);

        BadRequestObjectResult BadRequest(object error);

        BadRequestObjectResult BadRequest(ModelStateDictionary modelState);

        JsonResult Json(object value);

        NotFoundResult NotFound();
    }
}
