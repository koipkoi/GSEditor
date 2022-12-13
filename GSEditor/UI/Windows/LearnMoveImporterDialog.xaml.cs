﻿using GSEditor.Contracts.Services;
using GSEditor.Models.Pokegold;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace GSEditor.UI.Windows;

public partial class LearnMoveImporterDialog : Window
{
  private readonly IPokegoldService _pokegold = App.Services.GetRequiredService<IPokegoldService>();

  public List<LearnMove> Result { get; private set; } = new();

  public LearnMoveImporterDialog()
  {
    InitializeComponent();

    for (var i = 0; i < 251; i++)
      PokemonListBox.Items.Add(_pokegold.Data.Strings.PokemonNames[i].ToString());
  }

  private void OnWindowSizeChanged(object _, SizeChangedEventArgs __)
  {
    if (LearnMoveListView.View is GridView learnMoveGridView)
    {
      var totallyWidth = LearnMoveListView.ActualWidth - 32;
      learnMoveGridView.Columns[0].Width = totallyWidth * 0.3;
      learnMoveGridView.Columns[1].Width = totallyWidth * 0.7;
    }
  }

  private void OnPokemonListBoxSelectionChanged(object _, SelectionChangedEventArgs __)
  {
    var index = PokemonListBox.SelectedIndex;
    if (index != -1)
    {
      LearnMoveListView.SelectedIndex = -1;

      Result.Clear();
      LearnMoveListView.Items.Clear();

      foreach (var e in _pokegold.Data.Pokemons[index].LearnMoves)
      {
        Result.Add(e);
        LearnMoveListView.Items.Add(new LearnMoveItem
        {
          Level = $"{e.Level}",
          Move = $"{_pokegold.Data.Strings.MoveNames[e.MoveNo - 1]}",
        });
      }
    }
  }

  private void OnRemoveItemClick(object sender, RoutedEventArgs e)
  {
    var index = LearnMoveListView.SelectedIndex;
    if (index != -1)
    {
      LearnMoveListView.Items.RemoveAt(index);
      Result.RemoveAt(index);
    }
  }

  private void OnConfirmClick(object _, RoutedEventArgs __)
  {
    Close();
  }

  private void OnCancelClick(object _, RoutedEventArgs __)
  {
    Result.Clear();
    Close();
  }
}

public sealed class LearnMoveItem
{
  public string Level { get; set; } = "";
  public string Move { get; set; } = "";
}
