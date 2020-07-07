using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Minesweeper
{
    /// <summary>
    /// A base value converter every other converter can inherit from
    /// </summary>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        public T mConverter;

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new T());
        }
    }
}