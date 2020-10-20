using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace codxFrank.ABTesting.Services.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next,ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = await FormatRequest(context.Request);
            _logger.LogDebug("Request information : {request} ",  request);

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                var response = await FormatResponse(context.Response);
                _logger.LogDebug("Response information : {response} ",  response);
                
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var bodyAsText = String.Empty;
            request.EnableBuffering();
        
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyAsText = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            var headers = request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
                                         .Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
            var headesAsText = string.Join(" / ", headers);

            //TODO Formate with newline
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {headesAsText} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            //TODO log response headers
            return $"{response.StatusCode}: {text}";
        }
    }
}