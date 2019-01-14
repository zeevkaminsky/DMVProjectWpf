using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL_Wpf
{
    /// <summary>
    /// Interaction logic for NumericUserControl1.xaml
    /// </summary>
    public partial class NumericUserControl1 : UserControl
    {


        private float? num = null;
        public float? Value
        {
            get { return num; }
            set
            {
                if (value > MaxValue)
                    num = MaxValue;
                else if (value < MinValue)
                    num = MinValue;
                else num = value;
                txtNum.Text = num == null ? "" : num.ToString();
            }
        }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public NumericUserControl1()
        {
            InitializeComponent();
            MaxValue = 100;
        }


        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            Value++;
        }
        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            Value--;
        }
        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null || txtNum.Text == "" || txtNum.Text == "-")
            {
                Value = null;
                return;
            }
            float val;
            if (!float.TryParse(txtNum.Text, out val))
                txtNum.Text = Value.ToString();
            else Value = val;
        }
    }
}
