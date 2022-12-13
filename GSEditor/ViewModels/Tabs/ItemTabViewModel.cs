using GSEditor.Common.Bindings;
using GSEditor.Contracts.Services;
using GSEditor.Models.Pokegold;
using Microsoft.Extensions.DependencyInjection;

namespace GSEditor.ViewModels.Tabs;

public sealed class ItemsTabViewModel
{
  private readonly IPokegoldService _pokegold = App.Services.GetRequiredService<IPokegoldService>();
  private readonly IDialogService _dialogs = App.Services.GetRequiredService<IDialogService>();

  public BindingProperty<List<GBString>> ItemNames { get; } = new(new());
  public BindingProperty<int> CurrentItem { get; } = new(-1);
  public BindingProperty<string> Name { get; } = new("");

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
    ItemNames.Value = _pokegold.Data.Strings.ItemNames;
    UpdateCurrentInfo();
  }

  public void UpdateCurrentInfo()
  {
    var index = CurrentItem.Value;
    if (index != -1) { }
  }

  public void ApplyCurrentInfo()
  {

  }
}
