using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;
using MongoDB.Bson;
using Log.Core.Context;
using MongoDB.Bson.Serialization;
using Log.Core.Config;

namespace Log.Core.Context
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

        BsonSerializer.RegisterSerializer(typeof(DateTime), new BsonUtcDateTimeSerializer());
      }

      return db;
    }
  }
}