﻿namespace GSEditor.Core.PokegoldCore;

public sealed class PGEvolution
{
  public byte Type { get; set; }
  public byte PokemonNo { get; set; }
  public byte Level { get; set; }
  public byte ItemNo { get; set; }
  public byte Affection { get; set; }
  public byte BaseStats { get; set; }

  public static List<PGEvolution> FromBytes(byte[] bytes)
  {
    // 바이트 토큰 처리
    var arrays = new List<byte[]>();
    for (var i = 0; i < bytes.Length; i++)
    {
      switch (bytes[i])
      {
        case 1:
        case 2:
        case 3:
        case 4:
          arrays.Add(new byte[] { bytes[i], bytes[i + 1], bytes[i + 2], });
          i += 2;
          break;

        case 5:
          arrays.Add(new byte[] { bytes[i], bytes[i + 1], bytes[i + 2], bytes[i + 3], });
          i += 3;
          break;
      }
    }

    // 항목 파싱
    var result = new List<PGEvolution>();
    foreach (var e in arrays)
    {
      var newItem = ParseItem(e);
      result.Add(newItem);
    }

    return result;
  }

  private static PGEvolution ParseItem(byte[] bytes)
  {
    var newItem = new PGEvolution { Type = bytes[0] };
    switch (newItem.Type)
    {
      case 1:
        newItem.Level = bytes[1];
        newItem.PokemonNo = bytes[2];
        break;
      case 2:
      case 3:
        newItem.ItemNo = bytes[1];
        newItem.PokemonNo = bytes[2];
        break;
      case 4:
        newItem.Affection = bytes[1];
        newItem.PokemonNo = bytes[2];
        break;
      case 5:
        newItem.Level = bytes[1];
        newItem.BaseStats = bytes[2];
        newItem.PokemonNo = bytes[3];
        break;
    }
    return newItem;
  }

  public byte[] ToBytes()
  {
    return Type switch
    {
      1 => new byte[] { Type, Level, PokemonNo, },
      2 or 3 => new byte[] { Type, ItemNo, PokemonNo, },
      4 => new byte[] { Type, Affection, PokemonNo, },
      5 => new byte[] { Type, Level, BaseStats, PokemonNo, },
      _ => new byte[] { 0, },
    };
  }
}
