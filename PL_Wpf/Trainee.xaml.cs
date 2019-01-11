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
    /// Interaction logic for Trainee.xaml
    /// </summary>
    public partial class Trainee : Window
    {
        public Trainee()
        {
            InitializeComponent();
        }

        

        private void AddTrainee_Click(object sender, RoutedEventArgs e)
        {
            new AddTrainee().ShowDialog();
        }

        private void RemoveTrainee_Click(object sender, RoutedEventArgs e)
        {
            new RemoveTraineeWin().ShowDialog();
        }
    }
}
