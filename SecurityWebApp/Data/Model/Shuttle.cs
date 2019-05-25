using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityWebApp.Data.Model
{
  public class Shuttle
  {
    public Guid Id { get; set; }
    public string Model { get; set; }
    public string Producer { get; set; }
    public double Value { get; set; }
    public ShuttleType Type { get; set; }
    public virtual Pilot Piloto { get; set; }
  }
}
