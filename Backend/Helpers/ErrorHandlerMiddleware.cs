﻿using HotelBookingApp.Exceptions;
using System.Net;
using System.Text.Json;

namespace HotelBookingApp.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = exception switch
                {
                    InvalidRegistrationException or
                    EntityAlreadyExistsException => (int)HttpStatusCode.Unauthorized,   // 400

                    EntityNotAuthorizedException => (int)HttpStatusCode.Unauthorized,    // 401
                    EntityForbiddenException => (int)HttpStatusCode.Forbidden,               // 403
                    EntityNotFoundException => (int)HttpStatusCode.NotFound,             // 404
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var result = JsonSerializer.Serialize(new { message = exception?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
