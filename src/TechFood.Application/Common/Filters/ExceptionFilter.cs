﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechFood.Domain.Shared.Exceptions;

namespace TechFood.Application.Common.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var requestId = context.HttpContext.TraceIdentifier;

            if (context.Exception is DomainException domainException)
            {
                context.Result = new BadRequestObjectResult(
                    new
                    {
                        requestId,
                        message = domainException.Message
                    });
            }
            else
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ExceptionFilter>>();

                logger.LogError(
                    context.Exception,
                    "One or more error has occurried. RequestId {requestId}",
                    requestId);

                var result = new ObjectResult(
                    new
                    {
                        requestId,
                        message = context.Exception.ToString(),
                    })
                {
                    StatusCode = 500
                };

                context.Result = result;
            }
        }
    }
}
