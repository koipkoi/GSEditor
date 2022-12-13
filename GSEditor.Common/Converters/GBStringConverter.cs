using GSEditor.Models.Pokegold;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace GSEditor.Common.Converters;

public sealed class GBStringConverter : MarkupExtension, IValueConverter
{
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is GBString str)
      return str.ToString();
    if (value is List<GBString> list)
      return list.Select(e => e.ToString()).ToList();
    return "";
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value;
  }
}
