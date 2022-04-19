using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace Webapi.Middlewares
{

    public class CustomExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            var watch = Stopwatch.StartNew();

            // Request de ne olduğunu yazdıralım.
            try
            {
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                System.Console.WriteLine(message);

                // Request yazdırıldıkdan sonra bir sonraki middleware'e geç dedik.
                await _next(context);
                watch.Stop();

                // Şimdide Response'da ne olduğunu yazdıralım.
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.Milliseconds;
                System.Console.WriteLine(message);
            }
            catch (Exception ex)
            {   
                // Eğer await _next(context) içinde hata alırsa watch stop edilmeyecek
                // O yüzden burada tekrar stop etmemiz lazım.
                watch.Stop();
                // Biz eğer try-cath yazmazsak validation veya handle metholarında oluşacak hataları yakalamayız.
                // HttpContext de hep OK() ; döner bunun önüne geçmek için yapmak zorundayız.
                await HandleException(context,ex,watch);



                
            }




        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {   
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            string message = "[Error] HTTP "+context.Request.Method+" - "+context.Response.StatusCode+ " Error Message "+ ex.Message+ " in "+watch.Elapsed.TotalMilliseconds;

            
            // Newton json package'na ihtiyaç olmadan yazdırma D: NET6 gelişiyor.
            return context.Response.WriteAsJsonAsync(message);
        }
    }


    public static class CustomExceptionMiddlewareExtension
    {

        // Hangi isimle erişeceğimizi belirtiyoruz.
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }

    }

}