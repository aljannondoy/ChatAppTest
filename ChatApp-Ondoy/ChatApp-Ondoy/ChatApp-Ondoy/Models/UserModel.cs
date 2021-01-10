using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp_Ondoy
{
    public class UserModel : INotifyPropertyChanged
    {
        string _uid { get; set; }
        public string uid { get { return _uid; } set { _uid = value; OnPropertyChanged(nameof(uid)); } }
        string _email { get; set; }
        public string email { get { return _email; } set { _email = value; OnPropertyChanged(nameof(email)); } }

        string _name { get; set; }
        public string name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(name)); } }

        /// <summary>
        /// User Type
        /// 0 - Email
        /// 1 - Social Google
        /// 2 - Social Facebook
        /// </summary>
        int _userType { get; set; }
        public int userType { get { return _userType; } set { _userType = value; OnPropertyChanged(nameof(userType)); } }

        List<string> _contacts { get; set; }

        public List<string> contacts { get { return _contacts; } set { _contacts = value; OnPropertyChanged(nameof(contacts)); } }

        [JsonIgnore]
        DateTime _created_at { get; set; }
        [JsonIgnore]
        public DateTime created_at { get { return _created_at; } set { _created_at = value; OnPropertyChanged(nameof(created_at)); } }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
