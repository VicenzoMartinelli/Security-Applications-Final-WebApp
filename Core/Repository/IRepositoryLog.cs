using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Repository
{
  public interface IRepositoryLog
  {
    Task<IList<Log>> GetByDate(DateTime date);
  }
}