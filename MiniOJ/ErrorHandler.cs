using Microsoft.AspNetCore.Http;
using MiniOJ.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MiniOJ
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (MyException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception e)
            {
                Utils.Logger.GetInstance().Error(e.Message);
                await HandleExceptionAsync(context, new MySytemException());
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, MyException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await response.WriteAsync(JsonConvert.SerializeObject(exception.Result));

        }
    }
}
