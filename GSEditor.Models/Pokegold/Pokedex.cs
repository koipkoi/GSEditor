namespace GSEditor.Models.Pokegold;

public sealed class Pokedex
{
  public GBString SpecificName { get; set; } = new();
  public byte Height { get; set; }
  public int Weight { get; set; }
  public GBString Description { get; set; } = new();
}
