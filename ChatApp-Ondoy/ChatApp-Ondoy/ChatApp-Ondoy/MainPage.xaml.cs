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
        DataClass dataClass = DataClass.GetInstance;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        
        private async void signin_click(object sender, EventArgs e)
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
            else
            {
                loading.IsVisible = true;
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().LoginWithEmailPassword(email.Text, passtxt.Text);

                if (res.Status == true)
                {
                    Application.Current.MainPage = new TabPage();
                    /*
                    //Use this if you want the master detail page
                    Application.Current.MainPage = new MainMasterDetailPage(); 
                    */
                }
                else
                {
                    await DisplayAlert("Error", res.Response,"Okay");
                }
                loading.IsVisible = false;
            }
          
        }
        private async void create_acc(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignUpPage(), true);
        }
        async private void otherop(object sender, EventArgs e)
        {
        
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = await DependencyService.Get<iFirebaseAuth>().LoginWithEmailPassword(email.Text, passtxt.Text);
            Application.Current.MainPage = new TabPage();
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
        private async void forgotPass(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ResetPasswordPage(), true);
        }
        private void changetxt(object sender, TextChangedEventArgs e)
        {

            var txt = (CustomEntry)sender;
            txt.BorderColor = Color.Black;
        }
       
    }
}
