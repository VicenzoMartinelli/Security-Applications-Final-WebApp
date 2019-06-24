using Log.Core.Repository;
using Log.Core.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Threading.Tasks;

using LogEntity = Log.Core.Entity.Log;
using Log.Core.Enum;
using SecurityWebApp.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;

namespace SecurityWebApp.Middlewares
{
  public class LogMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IRepository _rep;

    public LogMiddleware(RequestDelegate next, IRepository rep)
    {
      _next    = next;
      _rep     = rep;
    }

    public async Task Invoke(HttpContext httpContext)
    {
      await _next(httpContext);

      var formSended = new List<string>();

      try
      {
        foreach (var item in httpContext.Request.Form)
        {
          if(item.Key != "__RequestVerificationToken")
            formSended.Add($"{item.Key} - {item.Value}");
        }
      }
      catch (InvalidOperationException){}
      finally
      {
        try
        {
          await _rep.AddAsync(new LogEntity
          {
            Description     = $"{httpContext.Request.Method} {httpContext.Request.Path} - {httpContext.Response.StatusCode}",
            RequestIp       = httpContext.Connection.RemoteIpAddress.ToString(),
            RequestMethod   = httpContext.Request.Method,
            RequestProtocol = httpContext.Request.Protocol,
            UserName        = httpContext.User.Identity?.Name ?? "Não Autenticado",
            Url             = httpContext.Connection.LocalIpAddress.ToString() + httpContext.Request.Path,
            FormDataSended  = string.Join(';', formSended)
          });
        }
        catch (Exception){}
      }
    }
  }

  // Extension method used to add the middleware to the HTTP request pipeline.
  public static class LogMiddlewareExtensions
  {
    public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<LogMiddleware>();
    }
  }
}
