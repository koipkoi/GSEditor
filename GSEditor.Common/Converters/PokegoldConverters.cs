using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace GSEditor.Common.Converters;

public sealed class BytePercentageConverter : MarkupExtension, IValueConverter
{
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is byte b)
      return string.Format("{0:0.00}%", (double)b / 0xff * 100.0);
    return 1.0;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value;
  }
}

public sealed class DescriptionLengthConverter : MarkupExtension, IValueConverter
{
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is string s)
    {
      var maxLength = 0;
      foreach (var line in s.Split("\n"))
      {
        if (line.Length > maxLength)
          maxLength = line.Length;
      }
      return $"설명 ({maxLength}/18)：";
    }
    return "설명 (0/18)：";
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value;
  }
}
