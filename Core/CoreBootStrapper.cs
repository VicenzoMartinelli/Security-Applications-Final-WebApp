using Core.Context;
using Core.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
  public class CoreBootStrapper
  {
    public static void InitializeCore(IServiceCollection services)
    {
      services.AddSingleton<IDBContext, DBContext>();

      services.AddSingleton<IMongoDatabase>((x) =>
      {
        return x.GetService<IDBContext>().GetDatabase(x.GetService<IConfiguration>().GetConnectionString("DbMongo"));
      });

      services.AddSingleton<IRepository, Repository.Repository>();
      services.AddSingleton<IRepositoryLog, RepositoryLog>();
    }
  }
}
