using System.ComponentModel.DataAnnotations;

namespace SecurityWebApp.Data.Model
{
  public enum StarClass
  {
    [Display(Name = "Anã Branca")]
    AnaBranca,
    [Display(Name = "Gigante Vermelha")]
    GiganteVermelha,
    [Display(Name = "Supernova")]
    Supernova,
    [Display(Name = "Buraco Negro")]
    BuracoNegro
  }
}