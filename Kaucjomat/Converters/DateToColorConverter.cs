using System.Globalization;

namespace Kaucjomat.Converters
{
    internal class DateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime expiryDate)
            {
                var daysLeft = (expiryDate.Date - DateTime.Today).TotalDays;

                if (daysLeft > 7)
                    return Colors.Green;
                else if (daysLeft >= 3)
                    return Colors.Orange;
                else if (daysLeft >= 0)
                    return Colors.Red;

                return Colors.Gray;
            }

            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 