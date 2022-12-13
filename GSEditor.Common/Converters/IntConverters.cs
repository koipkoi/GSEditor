using GSEditor.Common.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace GSEditor.Common.Converters;

public sealed class CompareIntConverter : MarkupExtension, IValueConverter
{
  public int Value { get; set; } = 0;
  public bool Inverse { get; set; } = false;
  public bool DesignerMode { get; set; } = true;

  public CompareIntConverter() { }
  public CompareIntConverter(int value) { Value = value; }

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (Platforms.IsDesignerMode)
      return DesignerMode;
    if (value is int i)
      return Inverse ? i != Value : i == Value;
    return false;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value;
  }
}
