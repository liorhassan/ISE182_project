
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
            copyResources();
            InitializeComponent();
            DataContext = _main;
            myChatRoom = new Chatroom();
            pw = new ProgramWindow(this,myChatRoom);
            
        }

        private void copyResources()
        {
            string sourcePath = Directory.GetCurrentDirectory();
            sourcePath = sourcePath.Substring(0, sourcePath.Length - 10);
            string targetPath = sourcePath + "\\bin\\Debug\\Resources";
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }
            if (System.IO.Directory.Exists(sourcePath + "\\Resources"))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath + "\\Resources");

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    string fileName = System.IO.Path.GetFileName(s);
                    string destFile = System.IO.Path.Combine(targetPath, fileName);
                    if(!File.Exists(destFile)) System.IO.File.Copy(s, destFile, true);
                }
            }
        }

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();

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
                _main.NicknameL = "";
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




        private void StartProgram()
        {
            pw.Show();
            this.Hide();
            pw.startWindow();
        }
    }
}
