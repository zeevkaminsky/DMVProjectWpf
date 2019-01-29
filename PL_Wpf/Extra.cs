using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PL_Wpf
{
    public static class Extra
    {
        //validate for numbers
        public static void NumbersValidate(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers");
            }
        }
        //convert to enum
        public static T ToEnum<T>(string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }
    }
}
