using GSEditor.Common.Bindings;
using GSEditor.Common.Utilities;
using GSEditor.Contracts.Services;
using GSEditor.Models.Pokegold;
using GSEditor.ViewModels.Selectors;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace GSEditor.ViewModels.Tabs;

public sealed class PokemonTabViewModel
{
  private readonly IPokegoldService _pokegold = App.Services.GetRequiredService<IPokegoldService>();
  private readonly IDialogService _dialogs = App.Services.GetRequiredService<IDialogService>();
  private readonly IPopupMenuService _popupMenu = App.Services.GetRequiredService<IPopupMenuService>();

  private readonly LockAction _lockAction = new();

  public BindingProperty<List<GBString>> PokemonNames { get; } = new(new());
  public BindingProperty<List<GBString>> ItemNames { get; } = new(new());
  public BindingProperty<List<GBString>> TypeNames { get; } = new(new());

  public BindingProperty<int> CurrentPokemon { get; } = new(-1);
  public BindingProperty<bool> IsUnown { get; } = new(false);
  public BindingProperty<string> PokemonNo { get; } = new("-");
  public BindingProperty<string> Name { get; } = new("-");
  public BindingProperty<int> GenderRateType { get; } = new(0);
  public BindingProperty<int> GrowthRateType { get; } = new(0);
  public BindingProperty<int> Type1 { get; } = new(0);
  public BindingProperty<int> Type2 { get; } = new(0);
  public BindingProperty<int> Item1 { get; } = new(0);
  public BindingProperty<int> Item2 { get; } = new(0);
  public BindingProperty<int> EggGroup1 { get; } = new(0);
  public BindingProperty<int> EggGroup2 { get; } = new(0);

  public BindingProperty<byte> HP { get; } = new(0);
  public BindingProperty<byte> Attack { get; } = new(0);
  public BindingProperty<byte> Defence { get; } = new(0);
  public BindingProperty<byte> SpAttack { get; } = new(0);
  public BindingProperty<byte> SpDefence { get; } = new(0);
  public BindingProperty<byte> Speed { get; } = new(0);
  public BindingProperty<byte> EXP { get; } = new(0);
  public BindingProperty<byte> CatchRate { get; } = new(0);

  public BindingProperty<GBImage?> FrontImage { get; } = new();
  public BindingProperty<GBImage?> BackImage { get; } = new();
  public BindingProperty<GBImage?> ShinyFrontImage { get; } = new();
  public BindingProperty<GBImage?> ShinyBackImage { get; } = new();
  public BindingProperty<GBColor?> Color1 { get; } = new();
  public BindingProperty<GBColor?> Color2 { get; } = new();
  public BindingProperty<GBColor?> ShinyColor1 { get; } = new();
  public BindingProperty<GBColor?> ShinyColor2 { get; } = new();

  public BindingProperty<string> SpecificName { get; } = new("-");
  public BindingProperty<double> Height { get; } = new(0);
  public BindingProperty<double> Weight { get; } = new(0);
  public BindingProperty<string> Description { get; } = new("-");

  public BindingProperty<List<EvolutionItemViewModel>> Evolutions { get; } = new(new());
  public BindingProperty<int> CurrentEvolution { get; } = new(-1);
  public BindingProperty<List<LearnMoveItemViewModel>> LearnMoves { get; } = new(new());
  public BindingProperty<int> CurrentLearnMove { get; } = new(-1);

  public void RefreshTab()
  {
    _pokegold.RomChanged += OnDataChanged;
    _pokegold.DataChanged += OnDataChanged;
    UpdateCurrentPokemonInfo();
  }

  public void UnloadTab()
  {
    _pokegold.RomChanged -= OnDataChanged;
    _pokegold.DataChanged -= OnDataChanged;
  }

  private void OnDataChanged(object? _, EventArgs __)
  {
    var previousSelection = CurrentPokemon.Value!;

    _lockAction.Run(() =>
    {
      PokemonNames.Value = _pokegold.Data.Strings.PokemonNames.Take(251).ToList();

      ItemNames.Value = new() { new("-"), };
      ItemNames.Value.AddRange(_pokegold.Data.Strings.ItemNames);
      ItemNames.NotifyPropertyChanged();

      TypeNames.Value = _pokegold.Data.Strings.MoveTypeNames;
    });

    CurrentPokemon.Value = previousSelection;
  }

  public void UpdateCurrentPokemonInfo()
  {
    _lockAction.Run(() =>
    {
      var index = CurrentPokemon.Value;
      if (index != -1)
      {
        PokemonNo.Value = $"{_pokegold.Data.Pokemons[index].No}";
        Name.Value = _pokegold.Data.Strings.PokemonNames[index].ToString();
        GenderRateType.Value = _pokegold.Data.Pokemons[index].GenderRate;
        GrowthRateType.Value = _pokegold.Data.Pokemons[index].GrowthRate;
        Type1.Value = _pokegold.Data.Pokemons[index].Type1;
        Type2.Value = _pokegold.Data.Pokemons[index].Type2;
        Item1.Value = _pokegold.Data.Pokemons[index].Item1;
        Item2.Value = _pokegold.Data.Pokemons[index].Item2;
        EggGroup1.Value = _pokegold.Data.Pokemons[index].EggGroup1;
        EggGroup2.Value = _pokegold.Data.Pokemons[index].EggGroup2;

        HP.Value = _pokegold.Data.Pokemons[index].HP;
        Attack.Value = _pokegold.Data.Pokemons[index].Attack;
        Defence.Value = _pokegold.Data.Pokemons[index].Defence;
        SpAttack.Value = _pokegold.Data.Pokemons[index].SpAttack;
        SpDefence.Value = _pokegold.Data.Pokemons[index].SpDefence;
        Speed.Value = _pokegold.Data.Pokemons[index].Speed;
        EXP.Value = _pokegold.Data.Pokemons[index].EXP;
        CatchRate.Value = _pokegold.Data.Pokemons[index].CatchRate;

        Color1.Value = _pokegold.Data.Colors.Pokemons[index][0];
        Color2.Value = _pokegold.Data.Colors.Pokemons[index][1];
        ShinyColor1.Value = _pokegold.Data.Colors.ShinyPokemons[index][0];
        ShinyColor2.Value = _pokegold.Data.Colors.ShinyPokemons[index][1];

        SpecificName.Value = _pokegold.Data.Pokedex[index].SpecificName.ToString();
        Height.Value = _pokegold.Data.Pokedex[index].Height / 10.0;
        Weight.Value = _pokegold.Data.Pokedex[index].Weight / 10.0;
        Description.Value = _pokegold.Data.Pokedex[index].Description.ToString().Replace("[59]", "\n");

        Evolutions.Value = _pokegold.Data.Pokemons[index].Evolutions.Select(e => new EvolutionItemViewModel(e)).ToList();
        LearnMoves.Value = _pokegold.Data.Pokemons[index].LearnMoves.Select(e => new LearnMoveItemViewModel(e)).ToList();

        RefreshImages();
      }

      IsUnown.Value = index == 200;
    });
  }

  public void DataChanged()
  {
    _lockAction.Run(() =>
    {
      var index = CurrentPokemon.Value;
      if (index != -1)
      {
        if (GBString.TryFromString(Name.Value!, out var nameStr))
          _pokegold.Data.Strings.PokemonNames[index] = nameStr!;

        _pokegold.Data.Pokemons[index].GenderRate = (byte)GenderRateType.Value!;
        _pokegold.Data.Pokemons[index].GrowthRate = (byte)GrowthRateType.Value!;
        _pokegold.Data.Pokemons[index].Type1 = (byte)Type1.Value!;
        _pokegold.Data.Pokemons[index].Type2 = (byte)Type2.Value!;
        _pokegold.Data.Pokemons[index].Item1 = (byte)Item1.Value!;
        _pokegold.Data.Pokemons[index].Item2 = (byte)Item2.Value!;
        _pokegold.Data.Pokemons[index].EggGroup1 = (byte)EggGroup1.Value!;
        _pokegold.Data.Pokemons[index].EggGroup2 = (byte)EggGroup2.Value!;

        _pokegold.Data.Pokemons[index].HP = HP.Value!;
        _pokegold.Data.Pokemons[index].Attack = Attack.Value!;
        _pokegold.Data.Pokemons[index].Defence = Defence.Value!;
        _pokegold.Data.Pokemons[index].SpAttack = SpAttack.Value!;
        _pokegold.Data.Pokemons[index].SpDefence = SpDefence.Value!;
        _pokegold.Data.Pokemons[index].Speed = Speed.Value!;
        _pokegold.Data.Pokemons[index].EXP = EXP.Value!;
        _pokegold.Data.Pokemons[index].CatchRate = CatchRate.Value!;

        _pokegold.Data.Colors.Pokemons[index][0] = Color1.Value!;
        _pokegold.Data.Colors.Pokemons[index][1] = Color2.Value!;
        _pokegold.Data.Colors.ShinyPokemons[index][0] = ShinyColor1.Value!;
        _pokegold.Data.Colors.ShinyPokemons[index][1] = ShinyColor2.Value!;

        if (GBString.TryFromString(SpecificName.Value!, out var specificNameStr))
          _pokegold.Data.Pokedex[index].SpecificName = specificNameStr!;

        _pokegold.Data.Pokedex[index].Height = (byte)(Height.Value! * 10);
        _pokegold.Data.Pokedex[index].Weight = (int)(Weight.Value * 10);

        var realDescription = Description.Value!.Replace("\r\n", "\n").Replace("\n", "[59]");
        if (GBString.TryFromString(realDescription, out var descriptionStr))
          _pokegold.Data.Pokedex[index].Description = descriptionStr!;

        RefreshImages();
        _pokegold.NotifyDataChanged();
      }
    });
  }

  private void RefreshImages()
  {
    var index = CurrentPokemon.Value;
    if (index != -1)
    {
      FrontImage.Value = new()
      {
        Source = _pokegold.Data.Images.Pokemons[index],
        Rows = _pokegold.Data.Pokemons[index].ImageTileSize,
        Columns = _pokegold.Data.Pokemons[index].ImageTileSize,
        Colors = new GBColor[] {
          GBColor.GBWhite,
          _pokegold.Data.Colors.Pokemons[index][0],
          _pokegold.Data.Colors.Pokemons[index][1],
          GBColor.GBBlack,
        },
      };

      BackImage.Value = new()
      {
        Source = _pokegold.Data.Images.PokemonBacksides[index],
        Rows = 6,
        Columns = 6,
        Colors = new GBColor[] {
          GBColor.GBWhite,
          _pokegold.Data.Colors.Pokemons[index][0],
          _pokegold.Data.Colors.Pokemons[index][1],
          GBColor.GBBlack,
        },
      };

      ShinyFrontImage.Value = new()
      {
        Source = _pokegold.Data.Images.Pokemons[index],
        Rows = _pokegold.Data.Pokemons[index].ImageTileSize,
        Columns = _pokegold.Data.Pokemons[index].ImageTileSize,
        Colors = new GBColor[] {
          GBColor.GBWhite,
          _pokegold.Data.Colors.ShinyPokemons[index][0],
          _pokegold.Data.Colors.ShinyPokemons[index][1],
          GBColor.GBBlack,
        },
      };

      ShinyBackImage.Value = new()
      {
        Source = _pokegold.Data.Images.PokemonBacksides[index],
        Rows = 6,
        Columns = 6,
        Colors = new GBColor[] {
          GBColor.GBWhite,
          _pokegold.Data.Colors.ShinyPokemons[index][0],
          _pokegold.Data.Colors.ShinyPokemons[index][1],
          GBColor.GBBlack,
        },
      };
    }
  }

  public void ShowImageMenu(string what)
  {
    var index = CurrentPokemon.Value;
    if (index == -1)
    {
      // todo 업데이트 (알림 표시 개선)
      // _dialogs.ShowError("오류", "잘못된 접근입니다.");
      return;
    }

    _popupMenu.Show(new PopupMenuItem.Builder()
      .Add("이미지 변경...", () =>
      {
        var fileName = _dialogs.ShowOpenFile("이미지 변경", "png 파일|*png");
        if (fileName != null)
        {
          if (!GBImage.TryLoadFromFile(fileName, out var newImage) || newImage == null)
          {
            // todo 업데이트 (알림 표시 개선)
            // _dialogs.ShowError("오류", "올바른 이미지 파일이 아닙니다!");
            return;
          }

          if (what == "front" || what == "shiny_front")
          {
            if (newImage.Columns != newImage.Rows || newImage.Columns < 5 || newImage.Columns > 7 || newImage.Rows < 5 || newImage.Rows > 7)
            {
              // todo 업데이트 (알림 표시 개선)
              // _dialogs.ShowError("오류", $"이미지 사이즈가 올바르지 않습니다.\n40x40, 48x48, 56x56 사이즈 중 하나에 맞도록 해주세요.");
              _dialogs.ShowError("오류", "이미지 사이즈가 올바르지 않습니다.");
              return;
            }

            _pokegold.Data.Images.Pokemons[index] = newImage.Source;
            _pokegold.Data.Pokemons[index].ImageTileSize = (byte)newImage.Rows;
          }
          else if (what == "back" || what == "shiny_back")
          {
            if (newImage.Columns != 6 || newImage.Rows != 6)
            {
              // todo 업데이트 (알림 표시 개선)
              // _dialogs.ShowError("오류", "이미지 사이즈가 올바르지 않습니다.\n48x48 사이즈에 맞도록 해주세요.");
              _dialogs.ShowError("오류", "이미지 사이즈가 올바르지 않습니다.");
              return;
            }

            _pokegold.Data.Images.PokemonBacksides[index] = newImage.Source;
          }

          if (what == "shiny_front" || what == "shiny_back")
          {
            _pokegold.Data.Colors.ShinyPokemons[index][0] = newImage.Colors[1];
            _pokegold.Data.Colors.ShinyPokemons[index][1] = newImage.Colors[2];
          }
          else
          {
            _pokegold.Data.Colors.Pokemons[index][0] = newImage.Colors[1];
            _pokegold.Data.Colors.Pokemons[index][1] = newImage.Colors[2];
          }

          _pokegold.NotifyDataChanged();
        }
      })
      .Add("-")
      .Add("png 저장...", () =>
      {
        var fileName = _dialogs.ShowSaveFile("png 저장", "png 파일|*png", $"{what}_{index + 1}.png");
        if (fileName != null)
        {
          var image = what switch
          {
            "front" => FrontImage.Value!,
            "back" => BackImage.Value!,
            "shiny_front" => ShinyFrontImage.Value!,
            _ => ShinyBackImage.Value!,
          };
          image.WriteFile(fileName);
        }
      })
      .Add("2bpp 저장...", () =>
      {
        var fileName = _dialogs.ShowSaveFile("2bpp 저장", "2bpp 파일|*2bpp|bin 파일|*.bin|모든 파일|*.*", $"{what}_{index + 1}.2bpp");
        if (fileName != null)
        {
          var source = what switch
          {
            "front" => FrontImage.Value!.Source,
            "back" => BackImage.Value!.Source,
            "shiny_front" => ShinyFrontImage.Value!.Source,
            _ => ShinyBackImage.Value!.Source,
          };
          File.WriteAllBytes(fileName, source);
        }
      })
      .Create());
  }

  public void EvolutionAction(string what)
  {
  }

  public void LearnMoveAction(string what)
  {
  }

  public void TMHMsAction(string what)
  {
  }
}
