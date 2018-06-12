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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditMessage : Window
    {
        private ObservableObject _main = new ObservableObject();
        public EditMessage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = _main.EditMessageText;
            
            if(s!=null)
            {
                MessageBox.Show(s.Length.ToString());
            }
            else
            {
                MessageBox.Show(s);
            }
            _main.EditMessageText = "";
            this.Hide();
        }
    }
}
