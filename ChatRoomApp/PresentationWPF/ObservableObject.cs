using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationWPF
{
    public class ObservableObject : INotifyPropertyChanged
    {
        //property changed event for the INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        
        public ObservableObject()
        {
            Messages.CollectionChanged += Messages_CollectionChanged;
        }

        //binding for the messages panel
        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Messages");
        }

        /*
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
        */

        //binding for the register text field
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

        //binding for the login text field
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

        //binding for the sort type combo box
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

        //binding for the Descending checkbox
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

        //binding for the filter combo box
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

        //binding for the filter group availability
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

        //binding for the filter group text field
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

        //binding for the filter user availability
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

        //binding for the filter user text field
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

        //binding for the send message text field
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