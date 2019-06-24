using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityWebApp.Data.Model
{
  public class Pilot
  {
    public Guid Id { get; set; }
    [Display(Name="Nome")]
    public string Name { get; set; }
    [Display(Name="Data de nascimento")]
    public DateTime BirthDate { get; set; }
    [Display(Name="Gênero")]
    public Genre Genre { get; set; }
  }
}