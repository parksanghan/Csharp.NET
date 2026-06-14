using System;
using System.Globalization;
using System.Windows.Data;

namespace UnitConverter.ConverUtil
{
    public class SpeedUnitConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (values == null || values.Length < 5)
            {
                return string.Empty;
            }

            string inputText = values[0]?.ToString();

            if (!double.TryParse(inputText, out double speedMs))
            {
                return string.Empty;
            }

            bool isKm = values[1] is bool km && km;
            bool isMs = values[2] is bool ms && ms;
            bool isKn = values[3] is bool kn && kn;
            bool isMach = values[4] is bool mach && mach;

            double result;

            if (isKm)
            {
                // m/s -> km/s
                result = speedMs / 1000.0;
            }
            else if (isMs)
            {
                // m/s 그대로
                result = speedMs;
            }
            else if (isKn)
            {
                // m/s -> knot
                result = speedMs * 1.943844;
            }
            else if (isMach)
            {
                // m/s -> mach
                // 대략 해수면 기준 음속 340.29 m/s
                result = speedMs / 340.29;
            }
            else
            {
                result = speedMs;
            }

            return result.ToString("0.###");
        }

        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}