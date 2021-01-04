using ChatApp_Ondoy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp_Ondoy
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Account accnt = new Account();
        public MainPage()
        {
            InitializeComponent();
        }
        public MainPage(Account acct)
        {
            accnt = acct;
            InitializeComponent();

        }
        async private void signin_click(object sender, EventArgs e)
        {
            if (email.Text == "" || passtxt.Text == "")
            {
                if (email.Text == "")
                {
                    email.BorderColor = Color.Red;

                }
                if (passtxt.Text == "")
                {
                    passtxt.BorderColor = Color.Red;
                }
                await DisplayAlert("Error", "Missing Fields", "Okay");
            }
            else if (email.Text == accnt.Email && passtxt.Text == accnt.pass)
            {
                newt.IsRunning = true;
                newtab.BackgroundColor = Color.FromRgba(0, 0, 0, 0.50);
                newtab.IsVisible = true;
                await Task.Delay(1000);
                newtab.IsVisible = false;
                newt.IsRunning = false;
                Application.Current.Properties.Clear();
                Application.Current.Properties.Add("email", accnt.Email);
                Application.Current.Properties.Add("password", accnt.pass);
                Application.Current.Properties.Add("username", accnt.uname);
                await Application.Current.SavePropertiesAsync();
                Application.Current.MainPage = new TabPage { BindingContext = accnt };
            }
            else
            {
                await DisplayAlert("Error", "Account doesn't exist", "Okay");
            }
        }
        private void create_acc(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SignUpPage(this));
        }
        async private void otherop(object sender, EventArgs e)
        {
            newt.IsRunning = true;
            newtab.BackgroundColor = Color.FromRgba(0, 0, 0, 0.50);
            newtab.IsVisible = true;
            await Task.Delay(1000);
            newtab.IsVisible = false;
            newt.IsRunning = false;
            Application.Current.Properties.Clear();
            Application.Current.Properties.Add("email", accnt.Email);
            Application.Current.Properties.Add("password", accnt.pass);
            Application.Current.Properties.Add("username", accnt.uname);
            await Application.Current.SavePropertiesAsync();
            Application.Current.MainPage = new TabPage { BindingContext = accnt };
        }
        private void showpassClick(object sender, EventArgs e)
        {
            var shopass = (CustomButton)sender;
            if (shopass.Text == "Show")
            {
                shopass.Text = "Hide";
                passtxt.IsPassword = false;
            }
            else
            {
                shopass.Text = "Show";
                passtxt.IsPassword = true;
            }
            passtxt.CursorPosition = passtxt.MaxLength;
        }
        private void changetxt(object sender, TextChangedEventArgs e)
        {

            var txt = (CustomEntry)sender;
            txt.BorderColor = Color.Black;
        }
        public void getAccount(Account acc)
        {
            accnt = acc;
        }
    }
}
