using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Log.Core.Model;
using Log.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecurityWebApp.Attributes;
using SecurityWebApp.Data;
using SecurityWebApp.Data.Model;
using SecurityWebApp.Models;

namespace SecurityWebApp.Controllers
{
  [Roles(DefaultRoles.Max)]
  public class LogController : Controller
  {
    private readonly IRepository _rep;
    private readonly IRepositoryLog _repLog;

    public LogController(IRepository rep, IRepositoryLog repLog)
    {
      _rep    = rep;
      _repLog = repLog;
    }

    public async Task<IActionResult> Index(FilterLogDTO filter)
    {
      return View(new LogViewModel
      {
        Logs      = await _repLog.GetByFilterLog(filter),
        DateEnd   = filter.DateEnd,
        DateStart = filter.DateStart,
        Text      = filter.Text
      });
    }
  }
}
