using GSEditor.Common.Bindings;
using GSEditor.Contracts.Services;
using GSEditor.Models.Pokegold;
using Microsoft.Extensions.DependencyInjection;

namespace GSEditor.ViewModels.Tabs;

 public sealed class UnownTabViewModel
{
  private readonly IPokegoldService _pokegold = App.Services.GetRequiredService<IPokegoldService>();
  private readonly IDialogService _dialogs = App.Services.GetRequiredService<IDialogService>();
  private readonly IPopupMenuService _popupMenu = App.Services.GetRequiredService<IPopupMenuService>();

  public BindingProperty<int> CurrentPokemon { get; } = new(-1);

  public BindingProperty<GBImage?> FrontImage { get; } = new();
  public BindingProperty<GBImage?> BackImage { get; } = new();
  public BindingProperty<GBImage?> ShinyFrontImage { get; } = new();
  public BindingProperty<GBImage?> ShinyBackImage { get; } = new();
  public BindingProperty<GBColor?> Color1 { get; } = new();
  public BindingProperty<GBColor?> Color2 { get; } = new();
  public BindingProperty<GBColor?> ShinyColor1 { get; } = new();
  public BindingProperty<GBColor?> ShinyColor2 { get; } = new();

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
    var index = CurrentPokemon.Value;
    if (index != -1) { }
  }

  public void ApplyCurrentInfo() { }

  public void ShowImageMenu(string what) { }
}
