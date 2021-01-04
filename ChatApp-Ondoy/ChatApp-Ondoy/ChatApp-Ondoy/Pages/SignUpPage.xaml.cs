using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_Ondoy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SignUpPage : ContentPage
{
        MainPage mainpage;
        Account account = new Account();
        public SignUpPage()
        {
            InitializeComponent();
        }
        public SignUpPage(MainPage mpage)
        {
            mainpage = mpage;

            InitializeComponent();
        }


        async private void signup_click(object sender, EventArgs e)
        {

            if (email.Text != "" && user.Text != "" && pass.Text != "")
            {
                if (pass.Text == pass2.Text)
                {
                    account = new Account(user.Text, email.Text, pass.Text);
                    mainpage.getAccount(account);
                    newt.IsRunning = true;
                    newtab.BackgroundColor = Color.FromRgba(0, 0, 0, 0.50);
                    newtab.IsVisible = true;
                    await Task.Delay(1000);
                    newtab.IsVisible = false;
                    newt.IsRunning = false;
                    await DisplayAlert("Success", "Sign up successful. Verification email sent", "OKAY");
                    await Navigation.PopModalAsync();

                }
                else
                {
                    
                    await DisplayAlert("Error", "Password don't match", "Okay");
                    pass2.Text = "";
                }
            }
            else

            {
                if (user.Text == "")
                {
                    user.BorderColor = Color.Red;
                }
                if (email.Text == "")
                {
                    email.BorderColor = Color.Red;
                }
                if (pass.Text == "")
                {
                    pass.BorderColor = Color.Red;
                }
                if (pass2.Text == "")
                {
                    pass2.BorderColor = Color.Red;
                }
                await DisplayAlert("Error", "Missing Fields", "Okay");



            }




        }

        private void changetxt(object sender, TextChangedEventArgs e)
        {
            var txt = (CustomEntry)sender;
            txt.BorderColor = Color.Black;
        }

        private void showpassw(object sender, EventArgs e)
        {

            if (showpass.Text == "Hide" || showpass2.Text == "Hide")
            {
                showpass.Text = "Show";
                showpass2.Text = "Show";
                pass.IsPassword = true;
                pass2.IsPassword = true;
            }
            else
            {
                showpass.Text = "Hide";
                showpass2.Text = "Hide";
                pass.IsPassword = false;
                pass2.IsPassword = false;
            }
            if (pass.IsFocused)
            {
                pass.CursorPosition = pass.MaxLength;
            }
            if (pass2.IsFocused)
            {
                pass2.CursorPosition = pass2.MaxLength;
            }
        }

        private void backsignin(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        private void otherop(object sender, EventArgs e)
        {
            DisplayAlert("Success", "Sign up successful. Verification email sent", "OKAY");
            Navigation.PopModalAsync();
        }


    }
}