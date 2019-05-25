using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityWebApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using SecurityWebApp.Data.Model;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SecurityWebApp
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbSqlServer")));

      services
          .AddIdentity<AppUser, IdentityRole>(options =>
          {
            options.Password.RequireDigit = false;
            //TODO: uncomment after some tests
            //options.Password.RequiredLength = 12; 
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
          })
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultUI(UIFramework.Bootstrap4)
          .AddDefaultTokenProviders();

      services.AddSingleton<IEmailSender, AppEmailSender>();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
          .AddRazorPagesOptions(options =>
          {
            options.AllowAreas = true;
            options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
            options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
          })
          .AddMvcOptions((x) => {
            x.CacheProfiles.Add(new string("lel"), new CacheProfile()
            {
              Duration = 5000
            });

            x.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
          });

      services.ConfigureApplicationCookie(options =>
      {
        options.LoginPath = $"/Identity/Account/Login";
        options.LogoutPath = $"/Identity/Account/Logout";
        options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
      });
    }

    
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      var cultureInfo = new CultureInfo("pt-BR");

      CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
      CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

      var supportedCultures = new[] { cultureInfo };
      app.UseRequestLocalization(new RequestLocalizationOptions
      {
        DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
        SupportedCultures     = supportedCultures,
        SupportedUICultures   = supportedCultures
      });

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
