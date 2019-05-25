using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityWebApp.Data.Model
{
  public enum ShuttleType
  {
    [Display(Name = "Nave de Guerra")]
    War,
    [Display(Name = "Nave de Turismo")]
    Turism,
    [Display(Name = "Nave de Exploração")]
    Exploration,
  }
}
