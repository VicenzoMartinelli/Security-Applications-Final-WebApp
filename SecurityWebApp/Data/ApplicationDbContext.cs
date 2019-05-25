using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecurityWebApp.Data.Model;

namespace SecurityWebApp.Data
{
  public class ApplicationDbContext : IdentityDbContext<AppUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Star> Stars { get; set; }
    public virtual DbSet<Pilot> Pilots { get; set; }
    public virtual DbSet<Shuttle> Shuttles { get; set; }
  }
}
