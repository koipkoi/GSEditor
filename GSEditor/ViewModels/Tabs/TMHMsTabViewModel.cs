using GSEditor.Common.Bindings;
using GSEditor.Contracts.Services;
using GSEditor.Models.Pokegold;
using Microsoft.Extensions.DependencyInjection;

namespace GSEditor.ViewModels.Tabs;

public sealed class TMHMsTabViewModel
{
  private readonly IPokegoldService _pokegold = App.Services.GetRequiredService<IPokegoldService>();
  private readonly IDialogService _dialogs = App.Services.GetRequiredService<IDialogService>();

  public BindingProperty<int> CurrentTMHM { get; } = new(-1);

  public void RefreshTab()
  {
    _pokegold.RomChanged += OnDataChanged;
    _pokegold.DataChanged += OnDataChanged;
    UpdateCurrentInfo();
  }

  public void UnloadTab()
  {
    _pokegold.RomChanged -= OnDataChanged;  
    _pokegold.DataChanged -= OnDataChanged;
  }

  private void OnDataChanged(object? _, EventArgs __)
  {
    UpdateCurrentInfo();
  }

  public void UpdateCurrentInfo()
  {
    var index = CurrentTMHM.Value;
    if (index != -1) { }
  }

  public void ApplyCurrentInfo() { }

  public void PokemonsAction(string what) { }
}
