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

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class ProgramWindow : Window
    {
        ObservableObject _main = new ObservableObject();
        MainWindow main;
        Chatroom chatroom;
        public ProgramWindow(MainWindow main,Chatroom chatroom) 
        {
            InitializeComponent();
            this.chatroom = chatroom;
            DataContext = _main; 
            this.main = main;
            foreach (String s in chatroom.SortByTimestamp(true)) _main.Messages.Add(s);
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            chatroom.Logout();
            main.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chatroom.SortType = Int32.Parse(_main.SortCombo);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
