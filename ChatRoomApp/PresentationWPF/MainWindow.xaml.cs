
using System;
using BusinessLogic;
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
using System.Windows.Threading;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableObject _main = new ObservableObject(); //binding object
        private Chatroom myChatRoom; //chatroom object
        private ProgramWindow pw; //the next window to show after login
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _main;
            myChatRoom = new Chatroom();
            pw = new ProgramWindow(this,myChatRoom);
        }

        
        //closes the app when the user closses the window
        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            myChatRoom.exit();
            Application.Current.Shutdown();

        }

        //tries to login with the nickname typed and show a messageBox if failes
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            String nickname = _main.NicknameL;
            String group =Int32.Parse(_main.GroupL).ToString();
            if (nickname == ""|group=="")
            {
                MessageBox.Show("Please enter a Nickname , a GroupID and Passward");
                return;
            }

            if (!myChatRoom.isLPasswordValid())
            {
                MessageBox.Show("Passward invalid");
                return;
            }

            Boolean login = myChatRoom.Login(nickname,group);
            if (!login)
            {
                MessageBox.Show("User doesn't exist");
            }
            else
            {
                _main.NicknameL = "";
                _main.GroupL = "24";
                StartProgram();
            }
        }


        //tries to register with the nickname typed and show a messageBox if failes
        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            String nickname = _main.NicknameR;
            String group = Int32.Parse(_main.GroupR).ToString();
            if (nickname == ""|group=="")
            {
                MessageBox.Show("Please enter a Nickname and a GroupID");
                return;
            }
            if (!myChatRoom.isRPasswordValid())
            {
                MessageBox.Show("Passward invalid");
                return;
            }
            Boolean reg = myChatRoom.Register(nickname,group);
            if (!reg)
            {
                MessageBox.Show("Nickname already exists, please choose another one");
            }
            else
            {
                MessageBox.Show("user " + nickname + " created in group "+group+" succesfuly♥");
                _main.NicknameR = "";
                _main.GroupR = "24";
            }
        }

        //makes sure only numbers go to the groupID fields
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //when login succefuly hides this window and shows the next
        private void StartProgram()
        {
            pw.Show();
            this.Hide();
            pw.startWindow();
        }

        private void LPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = sender as PasswordBox;
            myChatRoom.updateLPassword(pb.Password);
        }

        private void RPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = sender as PasswordBox;
            myChatRoom.updateRPassword(pb.Password);
        }

    }
}
