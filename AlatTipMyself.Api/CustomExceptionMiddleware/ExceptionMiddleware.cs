using AlatTipMyself.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //Invoke an invokeasync method that takes an httpcontext as params
        //Httpcontext contains all info about the request

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next(httpcontext);
            }
            catch (Exception ex)
            {

                switch (ex)
                {
                    case ApplicationException appException:
                        _logger.LogError(appException.Message);
                        httpcontext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        httpcontext.Response.ContentType = "application/json";
                        await httpcontext.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = 400,
                            Message = appException.Message
                        }.ToString());
                        break;
                    case ArgumentNullException argException:
                        _logger.LogError(argException.Message);
                        httpcontext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        httpcontext.Response.ContentType = "application/json";
                        await httpcontext.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = 400,
                            Message = "Fields cannot be empty"
                        }.ToString());
                        break;
                    default:
                        _logger.LogError(ex.Message);
                        httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        httpcontext.Response.ContentType = "application/json";
                        await httpcontext.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = 500,
                            Message = ex.Message
                        }.ToString());
                        break;
                }
            }
        }
    }
}
