using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            //Service katmanında yazdığım validation kodları çalışırken çıkan hatalar direkt olarak modelstate ile entegre çalışıyor ve yükleniyor.
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDTO<NoContentDTO>.Fail(errors, 400));


            }
        }
    }
}
