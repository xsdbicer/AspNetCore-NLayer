﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web
{
    public class NotFoundFilter<T, OutT> :IAsyncActionFilter where T : BaseEntity where OutT : class
    {
        private readonly IServices<T,OutT> _services;

        public NotFoundFilter(IServices<T, OutT> services)
        {
            _services = services;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity=await _services.AnyAsync(x=>x.Id==id);
            
            if (anyEntity.Data)
            {
                await next.Invoke();
                return;
            }
            var errorViewModel = new ErrorViewModel();
            errorViewModel.Errors.Add($"{typeof(T).Name}({id}) not found.");

            context.Result = new RedirectToActionResult("Error", "Home", errorViewModel);
        }
    }
}
