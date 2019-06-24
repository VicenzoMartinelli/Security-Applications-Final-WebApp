using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Log.Core.Repository
{
  public interface IRepositoryLog
  {
    Task<IList<Log.Core.Entity.Log>> GetByDate(DateTime date);
    Task<IList<Entity.Log>> GetByFilterLog(Model.FilterLogDTO filter);
  }
}