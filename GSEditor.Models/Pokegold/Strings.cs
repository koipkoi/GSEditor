using System.Collections.Generic;

namespace GSEditor.Models.Pokegold;

public sealed class Strings
{
  public List<GBString> ItemNames { get; } = new();
  public List<GBString> TrainerClassNames { get; } = new();
  public List<GBString> PokemonNames { get; } = new();
  public List<GBString> MoveNames { get; } = new();
  public List<GBString> MoveTypeNames { get; } = new();

  public List<GBString> ItemDescriptions { get; } = new();
  public List<GBString> MoveDescriptions { get; } = new();
}
