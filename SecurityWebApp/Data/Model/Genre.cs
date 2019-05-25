using System.ComponentModel.DataAnnotations;

namespace SecurityWebApp.Data.Model
{
  public enum Genre
  {
    [Display(Name = "Homem")]
    Male   = 1,
    [Display(Name = "Mulher")]
    Female = 2
  }
}