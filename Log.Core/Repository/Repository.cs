using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Log.Core.Repository
{
  public class Repository : IRepository
  {
    private readonly IMongoDatabase db;
    private readonly string yearCollection;

    public Repository(IMongoDatabase db)
    {
      this.db = db;
      yearCollection = $"_{DateTime.Now.Year}";

      var logBuilder = Builders<Entity.Log>.IndexKeys;
      var indexModel = new CreateIndexModel<Entity.Log>(logBuilder.Text(x => x.Description).Text(x => x.FormDataSended).Text(x => x.UserName));

      db.GetCollection<Entity.Log>(typeof(Entity.Log).Name + yearCollection)
        .Indexes.CreateOneAsync(indexModel);
    }

    public async Task<T> GetByIdAsync<T>(string id) where T : class
    {
      if (!id.Length.Equals(24))
        return default(T);

      var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));

      var x = await db.GetCollection<T>(typeof(T).Name + yearCollection)
          .Find(filter).FirstOrDefaultAsync();
      return x;
    }

    public async Task<List<T>> GetAll<T>() where T : class
    {
      var lst = await db.GetCollection<T>(typeof(T).Name + yearCollection).FindAsync("{}");
      return lst.ToList();
    }

    public async Task<List<T>> GetByFilter<T>(FilterDefinition<T> filter, SortDefinition<T> sort = null) where T : class
    {
      var lst = db.GetCollection<T>(typeof(T).Name + yearCollection).Find(filter);

      if(sort != null)
        lst.Sort(sort);

      return await lst.ToListAsync();
    }

    public async Task<T> AddAsync<T>(T source) where T : class
    {
      var collection = db.GetCollection<T>(typeof(T).Name + yearCollection);

      try
      {
        await collection.InsertOneAsync(source);

        return source;
      }
      catch (Exception e)
      {
        throw e;
      }

    }

    public async Task<T> UpdateAsync<T>(T source, string id) where T : class
    {
      var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));

      await db.GetCollection<T>(typeof(T).Name + yearCollection).ReplaceOneAsync(filter, source);
      return source;
    }

    public async Task<bool> DeleteAsync<T>(string id) where T : class
    {
      try
      {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        await db.GetCollection<T>(typeof(T).Name + yearCollection).DeleteOneAsync(filter);

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public Task<T> SaveOrUpdateAsync<T>(T source, string id) where T : class
    {
      if (string.IsNullOrEmpty(id))
        return AddAsync(source);
      else
        return UpdateAsync(source, id);
    }
  }
}
