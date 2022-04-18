namespace Webapi.Middlewares
{

    public class CustomExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context){

            string message = "[Request] HTTP "+ context.Request.Method + " - "+ context.Request.Path;
            System.Console.WriteLine(message);

            // Request yazdırıldıkdan sonra bir sonraki middleware'e geç dedik.
            await _next(context);




        }


    }


    public static class CustomExceptionMiddlewareExtension{

        // Hangi isimle erişeceğimizi belirtiyoruz.
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder){
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }

    }

}