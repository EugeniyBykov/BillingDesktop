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
using MaterialDesignThemes;
using MaterialDesignColors;
using Client.ViewModels;

namespace Client.Views
{

    public partial class MainWindow : Window
    {
        ClientsControl ClientControl;
        ProjectsControl ProjectControl;
        PaymentsControl PaymentControl; 
        ApplicationViewModel avm; 

        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            avm = new ApplicationViewModel();
            this.DataContext = avm;

            ClientControl = new ClientsControl(avm);
            GridMain.Children.Add(ClientControl);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
 
                case "clients":
                    try
                    {
                        if (GridMain.Children.Count > 0 && GridMain.Children != null)
                            GridMain.Children.Clear();
                        ClientControl = new ClientsControl(avm);
                        GridMain.Children.Add(ClientControl);
                    }
                    catch(Exception err)
                    {
                    }
                    
                    break;

                case "projects":
                    if (GridMain.Children.Count > 0 && GridMain.Children != null)
                        GridMain.Children.Clear();
                    ProjectControl = new ProjectsControl(avm);
                    GridMain.Children.Add(ProjectControl);
                    break;
                case "payment":
                    if (GridMain.Children.Count > 0 && GridMain.Children != null)
                        GridMain.Children.Clear();
                    PaymentControl = new PaymentsControl(avm);
                    GridMain.Children.Add(PaymentControl);
                    break;
                default:
                    break;
            }

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
   
        private void ListViewMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            t1.Visibility = Visibility.Visible;
            t3.Visibility = Visibility.Visible;
            t2.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            t1.Visibility = Visibility.Collapsed;
            t3.Visibility = Visibility.Collapsed;
            t2.Visibility = Visibility.Collapsed;
        }

        private void Hide_btn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; 
        }
    }
}
