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
        private ObservableObject _main = new ObservableObject();
        private MainWindow main;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Chatroom chatroom;
        public ProgramWindow(MainWindow main,Chatroom chatroom) 
        {
            InitializeComponent();
            this.chatroom = chatroom;
            DataContext = _main; 
            this.main = main;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);
        }
        public void startWindow()
        {
            dispatcherTimer.Start();
            UpdateView();
        }
        private void UpdateView()
        {
            _main.Messages.Clear();
            foreach (String s in chatroom.GetAllMessages()) _main.Messages.Add(s);
            
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            chatroom.Logout();
            main.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            chatroom.SetFilterAndSort(Int32.Parse(_main.SortCombo), Int32.Parse(_main.FilterCombo), Boolean.Parse(_main.IsDesc), _main.FilterGroup, _main.FilterUser);
            UpdateView();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _main.IsFilterGroup = (_main.FilterCombo != "0").ToString();
            _main.IsFilterUser = (_main.FilterCombo == "2").ToString();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (chatroom.Retrieve10Messages() != 0) UpdateView();
        }

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
        private void ProgramWindow_Closing(object sender, CancelEventArgs e)
        {
            chatroom.Logout();
            Application.Current.Shutdown();
        }
    }
}
