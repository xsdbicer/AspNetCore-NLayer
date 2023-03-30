using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        //IApplicationBuilder için bir metot yazmış oluyorum. Yani IApplicationBuilderı miras alan bütün classlarda bu metota erişebilirim.
        public static void UseCustomException(this IApplicationBuilder app)
        {
            // middleware, ben kendi modelimi dönmek istediğim için config ile birlikte içine giriyorum.
            app.UseExceptionHandler(config =>
            {
                // run ile şu gerçekleşliyor: Request buraya girdiği anda daha ileriye yani controller vs. ye gitmeyecek ve
                // buradan geri response dönecek. 
                // middleware içine run ile ikinci bir middleware kullanmış oldum. 
                config.Run(async context =>
                {
                    // ContentType tipinin application/json olacağını belirtiyoruz. 
                    context.Response.ContentType = "application/json";

                    // hatayı alabileceğimiz interface'i tanımlıyoruz. 
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    // uygualama hata fırlatabilir 500 
                    //ben client tarafıyla ilgili hata fırlatabilirim 400
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDTO<NoContentDTO>.Fail(exceptionFeature.Error.Message, statusCode);


                    // controllerda otomatik olarak verdiğimiz tip json'a çevriliyor. Ama ben burada kendim middleware yazıyorum
                    //yani kendim çevirmem gerekiyor. 
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                    //Buradaki işim bitti artık program.cs de middlewarei belirtebilirim.

                });
            });
        }
    }
}
