using BE;
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
    /// Interaction logic for TraineeView.xaml
    /// </summary>
    public partial class TraineeView : Window
    {
        IBl _bl;
        public TraineeView()
        {
            _bl = FactorySingletonBl.GetBl();
            InitializeComponent();

            this.LVUsers.ItemsSource = _bl.GetTrainees();//show all
            this.cityCB.ItemsSource = Enum.GetValues(typeof(Cities));
            for (int i = 0; i < 30; i++)
            {
                TestsCB.Items.Add(i);
            }

        }
        // show only trainees with a specific tests number
        private void TestsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TestsCB.SelectedItem != null)
            {
                this.LVUsers.ItemsSource = (from Igroup in _bl.TraineesByNumOfTests()
                                            from g in Igroup
                                            where Igroup.Key.ToString() == TestsCB.SelectedItem.ToString()
                                            select g).ToList();
            }
        }
        // show only trainees lives in a specific city
        private void CityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cityCB.SelectedItem != null)
            {
                this.LVUsers.ItemsSource = (from Igroup in _bl.TraineeByCity()
                                            from g in Igroup
                                            where Igroup.Key.ToString() == cityCB.SelectedItem.ToString()
                                            select g).ToList();
            }
        }
        
        //trainees by school
        private void SchoolBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SchoolTB.Text != null)
            {
                this.LVUsers.ItemsSource = (from Igroup in _bl.TraineesBySchool()
                                            from g in Igroup
                                            where Igroup.Key.ToString() == SchoolTB.Text.ToString()
                                            select g).ToList();
            }
        }
        //trainees by teacher
        private void TeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            this.LVUsers.ItemsSource = (from Igroup in _bl.TraineeByTeacher()
                                        from g in Igroup
                                        where Igroup.Key.ToString() == TeacherTB.Text.ToString()
                                        select g).ToList();

        }
        //show all
        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            this.LVUsers.ItemsSource = _bl.GetTrainees();
        }
    }
}
