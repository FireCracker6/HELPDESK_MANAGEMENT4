using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels;

public class PhoneNumberMaskConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string phoneNumber = (string)value;

        // Add your formatting logic here
        if (phoneNumber?.Length == 10)
        {
            return string.Format("{0:000-### ## ##}", double.Parse(phoneNumber));
        }
        else
        {
            return phoneNumber!;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string phoneNumber = (string)value;

        // Remove hyphen and space characters from input phone number
        phoneNumber = phoneNumber.Replace("-", string.Empty).Replace(" ", string.Empty);

        return phoneNumber;
    }

}