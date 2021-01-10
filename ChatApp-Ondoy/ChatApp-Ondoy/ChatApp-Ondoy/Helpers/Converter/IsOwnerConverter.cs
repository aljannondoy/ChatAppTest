using System;
using System.Globalization;
using Xamarin.Forms;

namespace ChatApp_Ondoy
{
    public class IsOwnerConverter : IValueConverter
    {
        DataClass dataClass = DataClass.GetInstance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = false;
            string[] players = value as string[];

            if (players[0].Equals(dataClass.loggedInUser.uid))
                ret = true;
            
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}