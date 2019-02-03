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
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для AddProjecrWindow.xaml
    /// </summary>
    public partial class AddProjecrWindow : Window
    {
        ApplicationViewModel avm; 
        public AddProjecrWindow(ApplicationViewModel a)
        {
            InitializeComponent();
            avm = a;
            this.DataContext = avm; 
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
