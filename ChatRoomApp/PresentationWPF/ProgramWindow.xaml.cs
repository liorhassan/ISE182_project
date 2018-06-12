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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class ProgramWindow : Window
    {
        private ObservableObject _main = new ObservableObject(); //binding object
        private MainWindow main; //the login window
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();//the timer responiseable for retreiveing messages and updating the display
        private Chatroom chatroom;//the chatroom object
        public ProgramWindow(MainWindow main,Chatroom chatroom) 
        {
            InitializeComponent();
            this.chatroom = chatroom;
            DataContext = _main; 
            this.main = main;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);
        }

        //on first start starts the timer and updates the display
        public void startWindow()
        {
            dispatcherTimer.Start();
            UpdateView();
        }

        //clears the messages panel and populates it with the right messages as given from the chatroom
        private void UpdateView()
        {
            _main.Messages.Clear();
            foreach (String s in chatroom.GetAllMessages()) _main.Messages.Add(s);
            
        }

        //stops this window, logout the chatroom and reshow the login window
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            chatroom.Logout();
            main.Show();
            this.Hide();
        }

        //sets the filter and order when the user presses the applay button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chatroom.SetFilterAndSort(Int32.Parse(_main.SortCombo), Int32.Parse(_main.FilterCombo), Boolean.Parse(_main.IsDesc), _main.FilterGroup, _main.FilterUser);
            UpdateView();
        }

        //changes the availability of the filter fields when the combo box changes
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _main.IsFilterGroup = (_main.FilterCombo != "0").ToString();
            _main.IsFilterUser = (_main.FilterCombo == "2").ToString();
        }

        //every 2 seconds, askes the chatroom to retruve 10 messages and updates the display if needed
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (chatroom.Retrieve10Messages() != 0) UpdateView();
        }

        //attempt to send a message and shows a warning if failes
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int send = chatroom.WriteMessage(_main.MessageText);
            if (send == 1)
            {
                _main.MessageText = "";
                UpdateView();
            }
            else if (send == -1)
            {
                MessageBox.Show("Can't send message");
            }
            else
            {
                _main.MessageText = "";
            }

        }

        //closses the chatroom and the app when the user closes the window
        private void ProgramWindow_Closing(object sender, CancelEventArgs e)
        {
            chatroom.Logout();
            chatroom.exit();
            Application.Current.Shutdown();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            //int index = Int32.Parse(_main.SelectedMessage);
            int index = _main.Messages.IndexOf(_main.SelectedListItem);
            if (index < 0) return;
            //var message = _main.Messages.ElementAt(index).ToString();
            //char[] chars = { ' ', '-', ' ' };
            //string[] parts = message.Split(chars);
            Boolean isOwner = chatroom.isOwner(index);
            if (isOwner)
            {
                Window edit = new EditMessage(_main);
                edit.ShowDialog();
                String newMessage = _main.Edit;
                chatroom.EditMesage(index, newMessage);
            }
            //string s = index.ToString();
            //Guid guid = chatroom.MessageGuid.ElementAt(index);
           // MessageBox.Show(chatroom.recievedMessages[guid].ToString());


            //MessageBox.Show(_main.EditMessageText);
            //string k = _main.EditMessageText;

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
