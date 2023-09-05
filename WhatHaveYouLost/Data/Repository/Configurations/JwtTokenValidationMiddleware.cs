using Microsoft.AspNetCore.Authorization;

namespace WhatYouHaveLost.Data.Repository.Configurations
{
    public class JwtTokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtTokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.GetEndpoint()?.Metadata.GetMetadata<AuthorizeAttribute>() != null)
            {
                if (context.Request.Headers.Authorization.Any())
                {
                    var token = context.Request.Headers.Authorization.ToString(); 

                    if (IsValidJwtToken(token))
                    {
                        await _next(context); 
                        return;
                    }
                }

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token JWT inválido ou ausente.");
                return;
            }

            await _next(context); 
        }

        private bool IsValidJwtToken(string token)
        {
            return !string.IsNullOrWhiteSpace(token);
        }
    }
}
