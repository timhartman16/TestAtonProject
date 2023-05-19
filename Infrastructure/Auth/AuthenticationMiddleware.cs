using Infrastructure.Exceptions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Infrastructure.Auth
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthorizationService authorizationService)
        {
            try
            {
                authorizationService.isInSystem(context);
                await _next(context);
            }
            catch (HttpErrorException e)
            {
                context.Response.StatusCode = e.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(e.Message));
            }

        }
    }
}
