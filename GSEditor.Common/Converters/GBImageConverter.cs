using GSEditor.Models.Pokegold;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace GSEditor.Common.Converters;

public sealed class GBImageConverter : MarkupExtension, IValueConverter
{
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }

  public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is GBImage image)
    {
      var width = image.Columns * 8;
      var height = image.Rows * 8;
      var pixels = new byte[width * height * 4];
      var writeableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);

      if (image.Source.Length / 16 <= image.Columns * image.Rows)
      {
        for (int i = 0; i < image.Source.Length; i += 2)
        {
          byte firstByte = image.Source[i];
          byte secondByte = image.Source[i + 1];
          int cursorX = i / (16 * image.Columns);
          int y = i % (16 * image.Columns) / 2;
          for (int x = 0; x < 8; x++)
          {
            byte highBit = (byte)((secondByte >> (7 - x)) & 1);
            byte lowBit = (byte)((firstByte >> (7 - x)) & 1);
            int palette = (highBit << 1) | lowBit;
            int index = (cursorX * 4 * 8) + (y * image.Columns * 8 * 4) + (x * 4);
            pixels[Math.Min(index + 0, pixels.Length - 1)] = image.Colors[palette].B;
            pixels[Math.Min(index + 1, pixels.Length - 1)] = image.Colors[palette].G;
            pixels[Math.Min(index + 2, pixels.Length - 1)] = image.Colors[palette].R;
            pixels[Math.Min(index + 3, pixels.Length - 1)] = 255;
          }
        }
      }

      writeableBitmap.Lock();
      writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, 4 * image.Columns * 8, 0);
      writeableBitmap.Unlock();

      return writeableBitmap;
    }
    return null;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value;
  }
}
