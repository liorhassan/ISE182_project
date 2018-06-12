using BusinessLogic;
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
using System.Windows.Threading;
using System.ComponentModel;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditMessage : Window
    {
        private ObservableObject _main;
        public EditMessage(ObservableObject main)
        {
            InitializeComponent();
            this._main = main;
            DataContext = _main;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = _main.Edit;
            if (s != null)
            {
                MessageBox.Show(s.Length.ToString());
            }
            else
            {
                MessageBox.Show(s);
            }

            this.Hide();
        }


    }
}