using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityWebApp.Models
{
  public class LogViewModel
  {
    public IList<Log.Core.Entity.Log> Logs { get; set; }

    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public string Text { get; set; }
  }
}
