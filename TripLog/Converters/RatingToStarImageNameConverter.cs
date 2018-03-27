using System;
using System.Globalization;
using Xamarin.Forms;

namespace TripLog.Converters
{
    public class RatingToStarImageNameConverter : IValueConverter
    {
        public RatingToStarImageNameConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int) {
                var rating = (int)value;
                if (rating <= 1)
                    return "star_1";

                if (rating >= 5)
                    return "stars_5";

                return "stars_" + rating;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
