using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Minesweeper
{
    /// <summary>
    /// Converts a string to image
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class StringToImageConverter : IValueConverter
    {
        /// <summary>
        /// A singleton instance of <see cref="StringToImageConverter"/> that we can bind to
        /// </summary>
        public static StringToImageConverter Instance = new StringToImageConverter();

        /// <summary>
        /// Converts a file name to it's respective image
        /// </summary>
        /// <param name="value">the name to convert</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>the image required</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;
            if (string.IsNullOrEmpty(path))
                return null;

            //return if the path is not empty
            return new BitmapImage(new Uri($"pack://application:,,,/Resources/Images/{path}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}