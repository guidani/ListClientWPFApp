using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ListClients1
{
    public partial class MainWindow : Window
    {
        List<User> items = new List<User>();
        public MainWindow()
        {
            InitializeComponent();

            items.Add(new User() { Age = 12, Sex = "M" });
            items.Add(new User() { Age = 13, Sex = "F" });
            items.Add(new User() { Age = 12, Sex = "M" });
            items.Add(new User() { Age = 24, Sex = "M" });
            items.Add(new User() { Age = 10, Sex = "F" });
            lvDataBinding.ItemsSource = items;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource);
        }

        public void FilterUsers()
        {
            if (CheckMale.IsChecked == true)
            {
                List<User> results = items.FindAll(FindMales);
                if (results.Count != 0)
                {
                    lvDataBinding.ItemsSource = results;
                }
            }

            if (CheckFemale.IsChecked == true)
            {
                List<User> results = items.FindAll(FindFemales);
                if (results.Count != 0)
                {
                    lvDataBinding.ItemsSource = results;
                }
            }


            if (CheckMale.IsChecked == true && CheckFemale.IsChecked == true)
            {
                lvDataBinding.ItemsSource = items;
            }
            if (CheckMale.IsChecked == false && CheckFemale.IsChecked == false)
            {
                lvDataBinding.ItemsSource = items;
            }
            CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();


        }

        private static bool FindMales(User user)
        {
            if (user.Sex.Equals("M"))
            {
                return true;
            }
            return false;

        }

        private static bool FindFemales(User user)
        {
            if (user.Sex.Equals("F"))
            {
                return true;
            }
            return false;

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String selectedGender = SelectGender.Text;

            if (InputIdade.Text.Length <= 0)
            {
                MessageBox.Show("Digite um valor para a idade");
                return;
            }

            int inputIdade = int.Parse(InputIdade.Text);
            items.Add(new User() { Age = inputIdade, Sex = selectedGender });
            CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();
            return;

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FilterUsers();
        }

        private void CheckFemale_Checked(object sender, RoutedEventArgs e)
        {
            FilterUsers();
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            CheckFemale.IsChecked = false;
            CheckMale.IsChecked = false;
            lvDataBinding.ItemsSource = items;
            CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();
        }

        private void SelectGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String selectedGender = SelectGender.Text;

        }

        private void InputIdade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }

    public class User : INotifyPropertyChanged
    {
        public int Age { get; set; }

        public string Sex { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
