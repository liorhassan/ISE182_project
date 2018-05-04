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
    public partial class LoginWindow : Window
    {
        ObservableObject _main = new ObservableObject();
        private Chatroom myChatRoom;
        public LoginWindow()
        {
            InitializeComponent();
            myChatRoom = new Chatroom();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
          //  String nickname = Text
            if ((nickname != "") && (!myChatRoom.Register(nickname)))
            {
                Console.WriteLine("Enter another nickname, or press ENTER to go back to the menu");
                nickname = Console.ReadLine();
            }
            if (nickname != "")
            {
                Console.WriteLine("user " + nickname + " created succesfuly♥");
                Console.WriteLine("Press ENTER to go back to the menu");
                Console.ReadLine();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
