using Minesweeper.Core;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Minesweeper
{
    [ValueConversion(typeof(ImageType),typeof(BitmapImage))]
    public class EnumToImageConverter : BaseValueConverter<EnumToImageConverter>
    {
        /// <summary>
        /// Converts <see cref="ImageType"/> to it's respective image
        /// </summary>
        /// <param name="value">the <see cref="ImageType"/> to convert</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>the image required</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case ImageType.Flag:
                    return new BitmapImage(new Uri($"pack://application:,,,/Minesweeper.Core;component/Resources/Images/flag.png"));
                case ImageType.Mine:
                    return new BitmapImage(new Uri($"pack://application:,,,/Minesweeper.Core;component/Resources/Images/mine.png"));
                case ImageType.QuestionMark:
                    return new BitmapImage(new Uri($"pack://application:,,,/Minesweeper.Core;component/Resources/Images/questionMark.png"));
                case ImageType.Timer:
                    return new BitmapImage(new Uri($"pack://application:,,,/Minesweeper.Core;component/Resources/Images/timer.png"));
                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
