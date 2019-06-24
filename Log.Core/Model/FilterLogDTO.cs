using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log.Core.Model
{
  public class FilterLogDTO
  {
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public string Text { get; set; }
  }
}
