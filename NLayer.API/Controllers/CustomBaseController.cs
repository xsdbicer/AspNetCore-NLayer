using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        //Nonaction ile bunun bir endpoint olmadığını söylüyoruz. Söylemezsek swagger bunu bir ednpoint olarak algılar ve get veya postu olmadığından hata fırlatır. 
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDTO<T> response) {
            if (response.StatusCode == 204) return new ObjectResult(null)
            {
                StatusCode = response.StatusCode,
            };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
           } 


    }
}
