
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
            //copyResources();
            InitializeComponent();
            DataContext = _main;
            myChatRoom = new Chatroom();
            pw = new ProgramWindow(this,myChatRoom);
            
        }

        //copies the images to the debug filter(becouse we coudnt find out how to make his looking in the resources folder).
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

        //closses the app when the user closses the window
        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            myChatRoom.exit();
            Application.Current.Shutdown();

        }

        //tries to login with the nickname typed and show a messageBox if failes
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            String nickname = _main.NicknameL;
            if (nickname.Contains('@'))
            {
                MessageBox.Show("Nickname Can't contain the char '@'");
                return;
            }
            String group =Int32.Parse(_main.GroupL).ToString();
            if (nickname == ""|group=="")
            {
                MessageBox.Show("Please enter a Nickname and a GroupID");
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
            if (nickname.Contains('@'))
            {
                MessageBox.Show("Nickname Can't contain the char '@'");
                return;
            }
            String group = Int32.Parse(_main.GroupR).ToString();
            if (nickname == ""|group=="")
            {
                MessageBox.Show("Please enter a Nickname and a GroupID");
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
    }
}
