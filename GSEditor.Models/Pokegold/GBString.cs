using GSEditor.Models.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSEditor.Models.Pokegold;

public sealed class GBString : ICloneable, IEnumerable, IEnumerable<byte>, IEquatable<GBString>
{
  private static readonly Dictionary<int, string> _charmap = new();
  private static readonly Dictionary<string, int> _charmapReverse = new();

  private readonly byte[] _bytes;

  public int Length => _bytes.Length;

  public byte this[int index]
  {
    get => _bytes[index];
    set => _bytes[index] = value;
  }

  static GBString()
  {
    var lines = Resources.Charmap.Replace("\r\n", "\n").Split("\n");
    foreach (var line in lines)
    {
      var keyValue = line.Split('=');
      if (keyValue.Length == 2)
      {
        try
        {
          var key = Convert.ToInt32(keyValue[0], 16);
          _charmap[key] = keyValue[1];
          _charmapReverse[keyValue[1]] = key;
        }
        catch { }
      }
    }
  }

  public GBString()
  {
    _bytes = Array.Empty<byte>();
  }

  public GBString(byte[] bytes)
  {
    _bytes = (byte[])bytes.Clone();
  }

  public GBString(string str)
  {
    if (!TryFromString(str, out var gbstr) || gbstr is null)
      throw new InvalidOperationException();
    _bytes = gbstr._bytes;
  }

  public static bool TryFromString(string str, out GBString? gbstr)
  {
    var bytes = new List<byte>();
    str = str.Replace("\r\n", "\n");
    gbstr = null;

    for (var i = 0; i < str.Length;)
    {
      var seek = 0;

      for (var j = 0; j < 16; j++)
      {
        seek++;

        if (i + j >= str.Length)
          return false;

        var sub = str.Substring(i, j + 1);

        // 등록된 문자 존재
        if (_charmapReverse.TryGetValue(sub, out var value))
        {
          var n = value;
          if (n > 0xff)
          {
            bytes.Add((byte)((n & 0xff00) >> 8));
            bytes.Add((byte)(n & 0xff));
          }
          else
          {
            bytes.Add((byte)n);
          }
          break;
        }

        // 숫자 처리
        if (sub.StartsWith("[") && sub.EndsWith("]"))
        {
          var numStr = sub[1..^1];
          try
          {
            var num = Convert.ToInt32(numStr, 16);
            if (num <= 0xff)
            {
              bytes.Add((byte)num);
              break;
            }
            else
            {
              return false;
            }
          }
          catch
          {
            return false;
          }
        }
      }

      i += seek;
    }

    gbstr = new(bytes.ToArray());
    return true;
  }

  public GBString(GBString gbstr)
  {
    _bytes = (byte[])gbstr._bytes.Clone();
  }

  public object Clone()
  {
    return new GBString(_bytes);
  }

  public IEnumerator GetEnumerator()
  {
    return new ByteEnumerator(_bytes);
  }

  IEnumerator<byte> IEnumerable<byte>.GetEnumerator()
  {
    return new ByteEnumerator(_bytes);
  }

  public bool Equals(GBString? other)
  {
    if (other is null)
      return false;
    return _bytes.SequenceEqual(other._bytes);
  }

  public override bool Equals(object? obj)
  {
    if (obj == null)
      return false;
    if (obj is GBString gbstr)
      return Equals(gbstr);
    if (obj is byte[] bytes)
      return _bytes.SequenceEqual(bytes);
    return false;
  }

  public override int GetHashCode()
  {
    return _bytes.GetHashCode();
  }

  public override string ToString()
  {
    var sb = new StringBuilder();
    for (var i = 0; i < _bytes.Length; i++)
    {
      var b = _bytes[i];
      if (b >= 0x1 && b <= 0xb)
      {
        var charmapIndex = (b << 8) | _bytes[i + 1];
        sb.Append(_charmap[charmapIndex]);
        i++;
      }
      else
      {
        if (_charmap.TryGetValue(b, out var value))
          sb.Append(value);
        else
          sb.Append($"[{b.ToString("x2")}]");
      }
    }
    return sb.ToString();
  }

  public byte[] ToBytes()
  {
    return _bytes;
  }

  public static bool operator ==(GBString a, GBString b)
  {
    return a.Equals(b);
  }

  public static bool operator !=(GBString a, GBString b)
  {
    return !a.Equals(b);
  }

  public static bool operator ==(GBString a, string b)
  {
    return a.ToString() == b;
  }

  public static bool operator !=(GBString a, string b)
  {
    return a.ToString() != b;
  }

  public static bool operator ==(string a, GBString b)
  {
    return a == b.ToString();
  }

  public static bool operator !=(string a, GBString b)
  {
    return a != b.ToString();
  }

  public static GBString operator +(GBString a, GBString b)
  {
    var bytes = new List<byte>();
    bytes.AddRange(a.ToBytes());
    bytes.AddRange(b.ToBytes());
    return new GBString(bytes.ToArray());
  }

  public static GBString operator +(GBString a, string b)
  {
    var bytes = new List<byte>();
    bytes.AddRange(a.ToBytes());
    bytes.AddRange(new GBString(b).ToBytes());
    return new GBString(bytes.ToArray());
  }

  public static GBString operator +(string a, GBString b)
  {
    var bytes = new List<byte>();
    bytes.AddRange(new GBString(a).ToBytes());
    bytes.AddRange(b.ToBytes());
    return new GBString(bytes.ToArray());
  }
}

public sealed class ByteEnumerator : IEnumerator, IEnumerator<byte>, IDisposable, ICloneable
{
  private byte[]? _bytes;
  private int _index;
  private byte _currentElement;

  internal ByteEnumerator(byte[] bytes)
  {
    _bytes = bytes;
    _index = -1;
  }

  public object Clone()
  {
    return MemberwiseClone();
  }

  public bool MoveNext()
  {
    if (_index < (_bytes!.Length - 1))
    {
      _index++;
      _currentElement = _bytes[_index];
      return true;
    }
    else
    {
      _index = _bytes.Length;
    }
    return false;
  }

  public void Dispose()
  {
    if (_bytes != null)
      _index = _bytes.Length;
    _bytes = null;
  }

  object? IEnumerator.Current => Current;

  public byte Current
  {
    get
    {
      if (_index == -1)
        throw new InvalidOperationException();
      if (_index >= _bytes!.Length)
        throw new InvalidOperationException();
      return _currentElement;
    }
  }

  public void Reset()
  {
    _currentElement = 0;
    _index = -1;
  }
}
