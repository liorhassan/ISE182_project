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
            String nickname = NicknameL.Text;
            if ((nickname == ""))
            {
                MessageBox.Show("Please enter a nickname");
                return;
            }

            Boolean login = myChatRoom.Login(nickname);
            if (!login)
            {
                MessageBox.Show("Nickname doesn't exist");
            }
            else
            {

            }
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
             String nickname = NicknameR.Text;
             if ((nickname == ""))
             {
                MessageBox.Show("Please enter a nickname");
                return;
             }
            Boolean reg = myChatRoom.Register(nickname);
            if (!reg)
            {
                MessageBox.Show("Nickname already exists, please choose another one");
            }
            else
            {
                MessageBox.Show("user " + nickname + " created succesfuly♥");
            }  
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           // Text = "{Binding ElementName=NicknameR, Path=Text}"
        }
    }
}
