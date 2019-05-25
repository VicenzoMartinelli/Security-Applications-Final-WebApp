using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityWebApp.Data.Model
{
  public class Star
  {
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [EnumDataType(typeof(StarClass))]
    public StarClass Class { get; set; }
  }
}
