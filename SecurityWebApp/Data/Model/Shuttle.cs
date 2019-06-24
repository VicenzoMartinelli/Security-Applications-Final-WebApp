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

    [Display(Name = "Modelo")]
    [Required(ErrorMessage = "Informe o modelo da nave")]
    public string Model { get; set; }
    [Display(Name = "Fabricante")]
    [Required(ErrorMessage = "Informe o fabricante da nave")]
    public string Producer { get; set; }
    [Display(Name = "Valor")]
    [Required(ErrorMessage = "Informe o valor da nave")]
    [Range(1, int.MaxValue, ErrorMessage = "Informe um valor válido!")]
    public double Value { get; set; }
    [Display(Name = "Tipo")]
    [Required(ErrorMessage = "O campo Tipo é obrigatório")]
    public ShuttleType Type { get; set; }
    [Display(Name = "Piloto")]
    public virtual Pilot Pilot { get; set; }
  }
}
