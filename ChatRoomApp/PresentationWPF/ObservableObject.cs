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

        //binding for the selected index
        private string selectedmessage = "";
        public string SelectedMessage
        {
            get
            {
                return selectedmessage;
            }
            set
            {
                selectedmessage = value;
                OnPropertyChanged("SelectedMessage");
            }
        }
        //binding for the nickname register text field
        private string nicknameR = "";
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

        //binding for the group register text field
        private string groupR = "24";
        public string GroupR
        {
            get
            {
                return groupR;
            }
            set
            {
                groupR = value;
                OnPropertyChanged("GroupR");
            }
        }

        //binding for the nickname login text field
        private string nicknameL = "";
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

        //binding for the group login text field
        private string groupL = "24";
        public string GroupL
        {
            get
            {
                return groupL;
            }
            set
            {
                groupL = value;
                OnPropertyChanged("GroupL");
            }
        }

        //binding for the passward register field
        private string passR;
        public string PassR
        {
            get
            {
                return passR;
            }
            set
            {
                passR = value;
                OnPropertyChanged("PassR");
            }
        }

        //binding for the passward login field
        private string passL;
        public string PassL
        {
            get
            {
                return passL;
            }
            set
            {
                passL = value;
                OnPropertyChanged("PassL");
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
        //binding for the edit message text field
        private string edit = "";
        public string Edit
        {
            get
            {
                return edit;
            }
            set
            {
                edit = value;
                OnPropertyChanged("Edit");
            }
        }
        //binding for the selected index
        public const string SelectedStickPropertyName = "SelectedListItem";
        private string _selectedItem;
        public string SelectedListItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if (_selectedItem == value || value == null)
                {
                    return;
                }
                var oldValue = _selectedItem;
                _selectedItem = value;
            }
        }

        private bool pressEdit=false;
        public bool PressEdit
        {
            get
            {
                return pressEdit;
            }
            set
            {
                pressEdit = value;
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}