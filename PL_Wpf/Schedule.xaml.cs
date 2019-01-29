using BL;
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
using System.Windows.Shapes;

namespace PL_Wpf
{
    /// <summary>
    /// Interaction logic for Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        IBl _bl;
        public Schedule()
        {
            
            InitializeComponent();
            _bl = FactorySingletonBl.GetBl();
            this.idTesters.ItemsSource = from t in _bl.GetTesters()
                                         select t.ID;
            
        }

        private void IdTesters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BE.Tester tester = _bl.FindTesterByID(this.idTesters.SelectedItem as string);
            this.Sched.Text = tester.WeeklySchedule.ToString();
        }
    }
}
