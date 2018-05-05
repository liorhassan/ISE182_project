using BusinessLogic;
using DispatcherAndBinding;
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

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class ProgramWindow : Window
    {
        ObservableObject _main = new ObservableObject();
        MainWindow main;
        public ProgramWindow(MainWindow main) 
        {
            InitializeComponent();
            this.main = main;
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            main.Show();
            this.Close();
        }
    }
}
