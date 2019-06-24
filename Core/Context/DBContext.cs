using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;
using MongoDB.Bson;
using Core.Context;

namespace Core.Context
{
  public class DBContext : IDBContext
  {
    private IMongoDatabase db;

    public IMongoDatabase GetDatabase(string connectionString)
    {
      if(db == null)
      {
        var mongoClient = new MongoClient(connectionString);

        db = mongoClient.GetDatabase("security-log");

        db.RunCommandAsync((Command<BsonDocument>)"{ping:1}").GetAwaiter().GetResult();

      }

      return db;
    }
  }
}