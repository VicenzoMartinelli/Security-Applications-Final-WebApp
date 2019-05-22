using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityWebApp.Data.Model
{
  public class AppEmailSender : IEmailSender
  {
    private readonly ILogger<AppEmailSender> _logger;

    public AppEmailSender(ILogger<AppEmailSender> logger)
    {
      _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
      _logger.LogWarning("Dummy IEmailSender implementation is being used!!!");
      _logger.LogDebug($"{email}{Environment.NewLine}{subject}{Environment.NewLine}{htmlMessage}");
      return Task.CompletedTask;
    }
  }
}
