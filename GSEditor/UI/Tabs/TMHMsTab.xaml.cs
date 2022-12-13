using System.Windows;
using System.Windows.Controls;

namespace GSEditor.UI.Tabs;

public partial class TMHMsTab : UserControl
{
  public TMHMsTab()
  {
    InitializeComponent();
  }

  private void OnSizeChanged(object _, SizeChangedEventArgs __)
  {
    PokemonContainer.Columns = (int)(ActualWidth / 160);
  }
}
