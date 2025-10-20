﻿using Microsoft.AspNetCore.Mvc;

namespace RecruitmentToDoApp.Exceptions
{
    internal sealed class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandler> logger;
        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occured");

                context.Response.StatusCode = ex switch
                {
                    ApplicationException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                await context.Response.WriteAsJsonAsync(
                    new ProblemDetails
                    {
                        Type = ex.GetType().Name,
                        Title = "An error occured",
                        Detail = ex.Message
                    });
            }
        }
    }
}
