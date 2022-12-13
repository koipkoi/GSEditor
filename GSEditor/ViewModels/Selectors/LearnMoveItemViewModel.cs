using GSEditor.Common.Bindings;
using GSEditor.Contracts.Services;
using GSEditor.Models.Pokegold;
using Microsoft.Extensions.DependencyInjection;

namespace GSEditor.ViewModels.Selectors;

public sealed class LearnMoveItemViewModel
{
  private readonly IPokegoldService _pokegold = App.Services.GetRequiredService<IPokegoldService>();

  public string Level { get; } = "";
  public string MoveName { get; } = "";

  public LearnMoveItemViewModel(LearnMove e)
  {
    Level = $"{e.Level}";
    MoveName = $"{_pokegold.Data.Strings.MoveNames[e.MoveNo - 1]}";
  }
}
