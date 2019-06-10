using SecurityWebApp.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityWebApp.Models
{
  public class ShuttleViewModel
  {
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Informe o modelo da nave")]
    public string Model { get; set; }
    [Required(ErrorMessage = "Informe o fabricante da nave")]
    public string Producer { get; set; }
    [Required(ErrorMessage = "Informe o valor da nave")]
    [Range(1, int.MaxValue, ErrorMessage = "Informe um valor válido!")]
    public double Value { get; set; }
    [Required]
    public ShuttleType Type { get; set; }
    public Guid? PilotId { get; set; }
  }
}
