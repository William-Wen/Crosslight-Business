using System;
using System.Globalization;
using Intersoft.Crosslight;

namespace Business
{
    /// <summary>
    ///     Converter class that negates boolean value.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.IValueConverter" />
    public class BooleanNegateConverter : IValueConverter
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
                return !(bool)value;

            return value;
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