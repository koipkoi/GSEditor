using GSEditor.Contracts.Services;
using GSEditor.Models.Pokegold;
using Microsoft.Extensions.DependencyInjection;

namespace GSEditor.ViewModels.Selectors;

public sealed class EvolutionItemViewModel
{
  private readonly IPokegoldService _pokegold = App.Services.GetRequiredService<IPokegoldService>();

  public string Pokemon { get; } = "";
  public string Method { get; } = "";
  public string Parameter1 { get; } = "";
  public string Parameter2 { get; } = "";

  public EvolutionItemViewModel(Evolution e)
  {
    Pokemon = _pokegold.Data.Strings.PokemonNames[e.PokemonNo - 1].ToString();

    Method = e.Type switch
    {
      1 or 5 => "레벨업",
      2 => "도구",
      3 => "통신교환",
      4 => "친밀도 MAX",
      _ => "?",
    };

    Parameter1 = e.Type switch
    {
      1 or 5 => $"레벨 {e.Level} 달성",
      2 => $"\"{_pokegold.Data.Strings.ItemNames[e.ItemNo - 1]}\" 사용",
      3 => e.ItemNo != 0xff ? $"\"{_pokegold.Data.Strings.ItemNames[e.ItemNo - 1]}\" 지닌 상태" : "-",
      4 => e.Affection switch
      {
        1 => "레벨업",
        2 => "낮에 레벨업",
        3 => "밤에 레벨업",
        _ => "?",
      },
      _ => "-",
    };

    Parameter2 = e.Type switch
    {
      5 => e.BaseStats switch
      {
        1 => "공격이 방어보다 높음",
        2 => "방어가 공격보다 높음",
        3 => "공격과 방어가 같음",
        _ => "?",
      },
      _ => "-",
    };
  }
}
