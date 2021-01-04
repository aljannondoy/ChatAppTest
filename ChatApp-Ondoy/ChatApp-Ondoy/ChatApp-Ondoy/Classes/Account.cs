using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatApp_Ondoy
{
    public class Account
    {
        private string username;
        private string email;
        private string password;



        public Account()
        {
            username = "Aljann Ondoy";
            email = "aljannondoy@gmail.com";
            password = "qwert";

        }
        public Account(string user, string Email, string passw)
        {
            username = user;
            email = Email;
            password = passw;

        }
        public string uname { get { return username; } set { username = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string pass { get { return password; } set { password = value; } }




    }
}
