﻿using Nancy;
using System.IO;

namespace CertiPay.Services.PDF.Extensions
{
    public static class ErrorExtension
    {
        public static Response AsError(this IResponseFormatter formatter, HttpStatusCode statusCode, string message)
        {
            return new Response
            {
                StatusCode = statusCode,
                ContentType = "text/plain",
                Contents = stream => (new StreamWriter(stream) { AutoFlush = true }).Write(message)
            };
        }
    }
}
