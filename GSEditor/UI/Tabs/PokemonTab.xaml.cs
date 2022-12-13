using System.Windows;
using System.Windows.Controls;

namespace GSEditor.UI.Tabs;

public partial class PokemonTab : UserControl
{
  public PokemonTab()
  {
    InitializeComponent();
  }

  private void OnSizeChanged(object _, SizeChangedEventArgs __)
  {
    if (EvolutionListView.View is GridView evolutionGridView)
    {
      var totallyWidth = EvolutionListView.ActualWidth - 32;
      evolutionGridView.Columns[0].Width = totallyWidth * 0.2;
      evolutionGridView.Columns[1].Width = totallyWidth * 0.2;
      evolutionGridView.Columns[2].Width = totallyWidth * 0.3;
      evolutionGridView.Columns[3].Width = totallyWidth * 0.3;
    }

    if (LearnMoveListView.View is GridView learnMoveGridView)
    {
      var totallyWidth = LearnMoveListView.ActualWidth - 32;
      learnMoveGridView.Columns[0].Width = totallyWidth * 0.3;
      learnMoveGridView.Columns[1].Width = totallyWidth * 0.7;
    }

    TMHMContainer.Columns = (int)(ActualWidth / 220);
  }
}
