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

    public partial class ProjectsControl : UserControl
    {
        ApplicationViewModel avm;

        public ProjectsControl(ApplicationViewModel a)
        {
            InitializeComponent();
            avm = a;
            this.DataContext = avm;
            r1_proj.IsChecked = true; 
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddProjecrWindow pw = new AddProjecrWindow(avm);
            pw.ShowDialog(); 
        }
    }
}
