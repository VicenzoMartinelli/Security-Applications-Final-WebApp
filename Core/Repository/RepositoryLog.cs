using Core.Entity;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
  public class RepositoryLog : IRepositoryLog
  {
    private readonly IRepository _repository;

    public RepositoryLog(IRepository repository)
    {
      _repository = repository;
    }

    public async Task<IList<Log>> GetByDate(DateTime date)
    {
      var filterObj = Builders<Log>.Filter.Where(x => x.Date.Date == date.Date);

      return await _repository.GetByFilter(filterObj);
    }
  }
}
