using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Build.Tasks;
using Microsoft.Extensions.Logging;
using NetMQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn.Api.ExceptionHandler
{
    public class ExceptionHandlingMiddleware : Controller
    {
        public RequestDelegate requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
        {
            try
            {
                await auth(context);
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, logger);
            }
        }

        private async Task auth(HttpContext context)
        {
            var uri = context.Request.Path.ToString();
            if (uri.StartsWith("/swagger/index.html") || uri.StartsWith("/swagger/v1/swagger.json") || uri.StartsWith("/swagger/ui/index.html") || uri.StartsWith("/swagger"))
            {
                var jwtToken = context.Session.GetString("_JwtToken");
                if (string.IsNullOrEmpty(jwtToken))
                {
                    var param = context.Request.QueryString.Value;
                    if (!param.Equals("?key=123"))
                    {
                        context.Response.StatusCode = 404;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync("{\"result:\" \"Not Found\"}", Encoding.UTF8);
                    }
                }
            }
            else if (uri.StartsWith("/LogGate/LogIn"))
            {
                var jwtToken = context.Session.GetString("_JwtToken");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    context.Response.Redirect("/swagger/index.html");
                }
            }
        }

        private static Task HandleException(HttpContext context, Exception ex, ILogger<ExceptionHandlingMiddleware> logger)
        {
            logger.LogError(ex.ToString());
            var errorMessageObject = new Error { Text = ex.Message, Code = ex.GetType().ToString() };
            var statusCode = (int)HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case InvalidException:
                    errorMessageObject.Code = "M001";
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

            }

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
