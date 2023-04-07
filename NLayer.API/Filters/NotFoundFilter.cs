using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IServices<T> _service;
        public NotFoundFilter(IServices<T> service)
        {
            _service = service;
        }

        //next -> herhangi bir filtera takılmıyorsa next ile devam ettireceğiz.
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Global Exception Handler ile getById metotunda verilen idye sahip data var mı diye kontrol ediyordum. 
            //ama ben daha service.cs deki metota gelmeden kontrol yapılmasını istiyorsam bunu filter ile gerçekleştirmek !! BP !!

            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }
            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDTO<NoContentDTO>.Fail($"{typeof(T).Name}({id}) not found.", 404));
        }
    }
}
