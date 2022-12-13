using GSEditor.Models.Pokegold;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace GSEditor.Common.Converters;

public sealed class GBColorConverter : MarkupExtension, IValueConverter
{
  private static readonly Color _defaultColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
  private static readonly GBColor _defaultGBColor = GBColor.GBWhite;

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is GBColor color)
      return color.ToColor();
    return _defaultColor;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is Color color)
      return new GBColor(color);
    return _defaultGBColor;
  }
}
