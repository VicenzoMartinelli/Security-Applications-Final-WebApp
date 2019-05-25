using System;

namespace SecurityWebApp.Data.Model
{
  public class Pilot
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Genre Genre { get; set; }
  }
}