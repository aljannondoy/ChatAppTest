using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp_Ondoy
{
    public class ContactModel : INotifyPropertyChanged
    {
        string _id { get; set; }
        public string id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(id)); } }
        string[] _ContactID { get; set; }
        public string[] ContactID { get { return _ContactID; } set { _ContactID = value; OnPropertyChanged(nameof(ContactID)); } }
        string[] _ContactEmail { get; set; }
        public string[] ContactEmail { get { return _ContactEmail; } set { _ContactEmail = value; OnPropertyChanged(nameof(ContactEmail)); } }

        string[] _ContactName { get; set; }
        public string[] ContactName { get { return _ContactName; } set { _ContactName = value; OnPropertyChanged(nameof(ContactName)); } }

        DateTime _created_at { get; set; }
        public DateTime created_at { get { return _created_at; } set { _created_at = value; OnPropertyChanged(nameof(created_at)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
