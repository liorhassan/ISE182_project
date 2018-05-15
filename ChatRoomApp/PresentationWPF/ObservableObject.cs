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

        private string sortCombo="0";
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

        private string isDesc = "True";
        public string IsDesc
        {
            get
            {
                return isDesc;
            }
            set
            {
                isDesc = value;
                OnPropertyChanged("IsDesc");
            }
        }

        private string filterCombo = "0";
        public string FilterCombo
        {
            get
            {
                return filterCombo;
            }
            set
            {
                filterCombo = value;
                OnPropertyChanged("FilterCombo");
            }
        }

        private string isFilterGroup = "False";
        public string IsFilterGroup
        {
            get
            {
                return isFilterGroup;
            }
            set
            {
                isFilterGroup = value;
                OnPropertyChanged("IsFilterGroup");
            }
        }

        private string filterGroup = "";
        public string FilterGroup
        {
            get
            {
                return filterGroup;
            }
            set
            {
                filterGroup = value;
                OnPropertyChanged("FilterGroup");
            }
        }

        private string isFilterUser = "False";
        public string IsFilterUser
        {
            get
            {
                return isFilterUser;
            }
            set
            {
                isFilterUser = value;
                OnPropertyChanged("IsFilterUser");
            }
        }

        private string filterUser = "";
        public string FilterUser
        {
            get
            {
                return filterUser;
            }
            set
            {
                filterUser = value;
                OnPropertyChanged("FilterUser");
            }
        }

        private string messageText = "";
        public string MessageText
        {
            get
            {
                return messageText;
            }
            set
            {
                messageText = value;
                OnPropertyChanged("MessageText");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}