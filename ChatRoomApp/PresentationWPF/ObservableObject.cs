using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationWPF
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableObject()
        {
            Messages.CollectionChanged += Messages_CollectionChanged;
        }

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Messages");
        }

        private string messageContent = "";
        public string MessageContent
        {
            get
            {
                return messageContent;
            }
            set
            {
                messageContent = value;
                OnPropertyChanged("MessageContent");
            }
        }
        private string nicknameR="";
        public string NicknameR
        {
            get
            {
                return nicknameR;
            }
            set
            {
                nicknameR = value;
                OnPropertyChanged("NicknameR");
            }
        }
        private string nicknameL="";
        public string NicknameL
        {
            get
            {
                return nicknameL;
            }
            set
            {
                nicknameL = value;
                OnPropertyChanged("NicknameL");
            }
        }
        private string sortCombo="";
        public string SortCombo
        {
            get
            {
                return sortCombo;
            }
            set
            {
                sortCombo = value;
                OnPropertyChanged("SortCombo");
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}