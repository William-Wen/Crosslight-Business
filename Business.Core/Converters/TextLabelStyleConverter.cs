using System;
using System.Globalization;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Drawing;

namespace Business
{
    /// <summary>
    ///     Converter class that converts item condition to a specified text label style.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.IValueConverter" />
    public class TextLabelStyleConverter : IValueConverter
    {
        #region IValueConverter implementation

        /// <summary>
        ///     Convert the specified value, targetType, parameter and culture.
        /// </summary>
        /// <param name="value">The converted value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool isSold = (bool)value;

                if (isSold)
                {
                    StyleAttributes style = new StyleAttributes();
                    style.Strikethrough = true;
                    style.ForegroundColor = Colors.DarkGray;
                    return style;
                }
            }

            return null;
        }

        /// <summary>
        ///     Converts the specified value back to the original type.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        /// <returns>
        ///     The converted value.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}