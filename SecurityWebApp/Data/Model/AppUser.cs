using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityWebApp.Data.Model
{
  public class AppUser : IdentityUser
  {
    public string Sector { get; set; }
    public DateTime StartDate { get; set; }
  }
}
