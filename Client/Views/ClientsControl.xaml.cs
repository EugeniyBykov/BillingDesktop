using Client.ViewModels;
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

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для ClientsControl.xaml
    /// </summary>
    public partial class ClientsControl : UserControl
    {
        ApplicationViewModel avm; 
            
        public ClientsControl(ApplicationViewModel a)
        {
            InitializeComponent();
            avm = a;
            this.DataContext = avm;
            r1.IsChecked = true;
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
             AddClientWindow add = new AddClientWindow(avm);
            add.ShowDialog();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (avm.SelectedClient != null)
            {
                avm.SelectedProject = avm.ProjectList.Where(p => p.Name == avm.SelectedClient.CompanyName).FirstOrDefault();
                EditClientWindow ew = new EditClientWindow(avm);
                ew.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите клиента для редактирования"); 
            }
        }

        private void ShowDatails_Button_Click(object sender, RoutedEventArgs e)
        {
            if (avm.SelectedClient != null)
            {
                avm.SelectedProject = avm.ProjectList.Where(p => p.Name == avm.SelectedClient.CompanyName).FirstOrDefault();
                EditClientWindow ew = new EditClientWindow(avm);
                ew.save_btn.Visibility = Visibility.Collapsed;
                ew.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите клиента для просмотра");
            }
        }
    }
}
