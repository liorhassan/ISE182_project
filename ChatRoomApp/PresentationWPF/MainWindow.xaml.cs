
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

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableObject _main = new ObservableObject();
        private Chatroom myChatRoom;
        private ProgramWindow pw;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _main;
            try
            {
                String path = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(path.Substring(0, path.Length - 38) + "\\Data");
            }
            catch (Exception e)
            {
                throw e;
            }
            //Cli Chatroom = new Cli();
            myChatRoom = new Chatroom();
            pw = new ProgramWindow(this,myChatRoom);
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            String nickname = _main.NicknameL;
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
                StartProgram();
            }
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            String nickname = _main.NicknameR;
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
                _main.NicknameR = "";
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Text = "{Binding ElementName=NicknameR, Path=Text}"
        }

        private void StartProgram()
        {
            pw.Show();
            this.Hide();
        }
    }
}
