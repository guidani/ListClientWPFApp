using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ListClients1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<User> items = new List<User>();
        public MainWindow()
        {
            InitializeComponent();

            items.Add(new User() { Name = "joao", Age = 12, Mail="joao@email.com", Sex=SexType.Male });
            items.Add(new User() { Name = "maria", Age = 13 , Mail="maria@email.com", Sex=SexType.Female});
            items.Add(new User() { Name = "jose", Age = 12 , Mail="jose@email.com", Sex=SexType.Male});
            items.Add(new User() { Name = "Gabriel", Age = 24 , Mail="jose@email.com", Sex = SexType.Male });
            items.Add(new User() { Name = "Daniela", Age = 10 , Mail="jose@email.com", Sex = SexType.Female});
            lvDataBinding.ItemsSource = items;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource);


            // Ordenação
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource);
            //view.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
            //view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

        }

        public void FilterUsers()
        {
           if(CheckMale.IsChecked == true)
            {
                List<User> results = items.FindAll(FindMales);
                if(results.Count != 0)
                {
                    lvDataBinding.ItemsSource = results;
                }
                
            } else
            {
                if(CheckFemale.IsChecked==true)
                {
                    List<User> results = items.FindAll(FindFemales);
                    if(results.Count != 0)
                    {
                        lvDataBinding.ItemsSource = results;
                    }
                }
            }
           
        }

         private static bool FindMales(User user)
        {
            if (user.Sex.Equals(SexType.Male))
            {
                return
                    true;
            } else
            {
                return false;
            }
        }

        private static bool FindFemales(User user)
        {
            if (user.Sex.Equals(SexType.Female))
            {
                return true;
            } else
            {
                return false;
            }
        }
        

       

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            items.Add(new User() { Name = "joao", Age = 12, Mail = "joao@email.com", Sex = SexType.Male });
            // lvDataBinding.Items.Add(new User() { Name = "user", Age = 112, Mail = "user@email.com", sex = SexType.Male });
            // lvDataBinding.ItemsSource = "";
            CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();

        }

        private void BtnSortDesc_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FilterUsers();
            if (CheckFemale.IsChecked == true)
            {
                CheckFemale.IsChecked = !CheckFemale.IsChecked;
            }
            CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();
            

        }

        private void CheckFemale_Checked(object sender, RoutedEventArgs e)
        {
            FilterUsers();
            if (CheckMale.IsChecked == true)
            {
                CheckMale.IsChecked = !CheckMale.IsChecked;
            }
            CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();

        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            CheckFemale.IsChecked = false;
            CheckMale.IsChecked = false;

            
        CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();
        }

    }

    public enum SexType { Male, Female}

    public class User : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Mail { get; set; }

        public SexType Sex { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return this.Name + ", " + this.Age + " years old";
        }
    }
}
