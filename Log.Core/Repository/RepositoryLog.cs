using Log.Core.Model;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.Repository
{
  public class RepositoryLog : IRepositoryLog
  {
    private readonly IRepository _repository;

    public RepositoryLog(IRepository repository)
    {
      _repository = repository;
    }

    public async Task<IList<Entity.Log>> GetByDate(DateTime date)
    {
      var filterObj = Builders<Entity.Log>.Filter.Where(x => x.Date.Date == date.Date);

      return await _repository.GetByFilter(filterObj);
    }

    public async Task<IList<Entity.Log>> GetByFilterLog(FilterLogDTO filter)
    {
      var filters = new List<FilterDefinition<Entity.Log>>();
      var fBuilder = Builders<Entity.Log>.Filter;

      if(filter.DateStart.HasValue && filter.DateStart.Value != DateTime.MinValue)
      {
        filters.Add(fBuilder.Where(x => x.Date >= filter.DateStart.Value.Date));
      }

      if (filter.DateEnd.HasValue && filter.DateEnd.Value != DateTime.MinValue)
      {
        filters.Add(fBuilder.Where(x => x.Date <= filter.DateEnd.Value.Date));
      }

      if(!string.IsNullOrEmpty(filter.Text))
      {
        filters.Add(fBuilder.Text(filter.Text, new TextSearchOptions()
        {
          CaseSensitive = false,
          DiacriticSensitive = false
        }));
      }

      if (filters.Count == 0)
        filters.Add(fBuilder.Empty);


      return await _repository.GetByFilter(fBuilder.And(filters), Builders<Entity.Log>.Sort.Descending(x => x.Date));
    }
  }
}
