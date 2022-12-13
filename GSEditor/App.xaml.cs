using GSEditor.Contracts.Services;
using GSEditor.Services;
using Microsoft.Extensions.DependencyInjection;
using PresentationTheme.Aero;
using System.Windows;

namespace GSEditor;

public partial class App
{
  public static IServiceProvider Services { get; } = new ServiceCollection()
      .AddSingleton<ISettingService, SettingService>()
      .AddSingleton<IPokegoldService, PokegoldService>()
      .AddSingleton<IDialogService, DialogService>()
      .AddSingleton<IPopupMenuService, PopupMenuService>()
      .BuildServiceProvider();

  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    AeroTheme.SetAsCurrentTheme();
    Services.GetRequiredService<IDialogService>().ShowMain();
    Shutdown();
  }
}
