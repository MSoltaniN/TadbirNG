using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SPPC.Tadbir.Web.Api.Utility
{
    public class ApiResultFactory : IApiResultFactory
    {
        public OkResult Ok()
        {
            return new OkResult();
        }

        public OkObjectResult Ok(object value)
        {
            return new OkObjectResult(value);
        }

        public BadRequestObjectResult BadRequest(object error)
        {
            return new BadRequestObjectResult(error);
        }

        public BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            return new BadRequestObjectResult(modelState) { StatusCode = 404 };
        }

        public JsonResult Json(object value)
        {
            return new JsonResult(value) { StatusCode = 200 };
        }

        public NotFoundResult NotFound()
        {
            return new NotFoundResult();
        }
    }
}
