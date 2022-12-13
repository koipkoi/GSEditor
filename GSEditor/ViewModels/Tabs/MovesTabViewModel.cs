using GSEditor.Common.Bindings;
using GSEditor.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GSEditor.ViewModels.Tabs;

public sealed class MovesTabViewModel
{
  private readonly IPokegoldService _pokegold = App.Services.GetRequiredService<IPokegoldService>();
  private readonly IDialogService _dialogs = App.Services.GetRequiredService<IDialogService>();

  public BindingProperty<int> CurrentMove { get; } = new(-1);

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
    var index = CurrentMove.Value;
    if (index != -1) { }
  }

  public void ApplyCurrentInfo() { }
}
