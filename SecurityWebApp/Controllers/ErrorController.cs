using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecurityWebApp.Controllers
{
  [AllowAnonymous]
  [Route("/Error")]
  public class ErrorController : Controller
  {
    public IActionResult Index() => View();

    [HttpGet("forbidden")]
    public IActionResult Forbidden()
    {
      return View();
    }
  }
}