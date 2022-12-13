using Microsoft.Win32;
using System.Windows;

namespace GSEditor.Resources;

public partial class Colors
{
  public Colors()
  {
    var application = Application.Current;
    application.Startup += Application_Startup;
  }

  private void Application_Startup(object _, StartupEventArgs __)
  {
    var application = Application.Current;
    application.Exit += Application_Exit;
    application.Startup -= Application_Startup;

    SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;

    UpdateSystemColors();
  }

  private void Application_Exit(object _, ExitEventArgs __)
  {
    var application = Application.Current;
    application.Exit -= Application_Exit;

    SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
  }

  private void SystemEvents_UserPreferenceChanged(object _, UserPreferenceChangedEventArgs e)
  {
    if (e.Category == UserPreferenceCategory.General || e.Category == UserPreferenceCategory.VisualStyle)
      UpdateSystemColors();
  }

  private void UpdateSystemColors()
  {
    var application = Application.Current;

    application.Resources["GSEditor_Color_SystemAccent"] = SystemColors.DesktopColor;
    application.Resources["GSEditor_Brush_SystemAccent"] = SystemColors.DesktopBrush;

    application.Resources["GSEditor_Color_SystemControl"] = SystemColors.ControlColor;
    application.Resources["GSEditor_Brush_SystemControl"] = SystemColors.ControlBrush;
  }
}
