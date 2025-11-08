using System.Globalization;

namespace MHiker_CrossPlatform.Converters
{
    public class BoolToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Chuyển bool (Model) sang string (Picker/Label)
            if (value is bool boolValue)
            {
                return boolValue ? "Yes" : "No";
            }
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Chuyển string (Picker) về bool (Model)
            return value is string strValue && strValue == "Yes";
        }
    }
}