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
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
        {
            InitializeComponent();
            
        }

        private async void Reset_Pass(object sender, EventArgs e)
        {
            if (email.Text == "")
            {
                email.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing fields", "Okay");
            }
            else
            {
                loading.IsVisible = true;
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().ResetPassword(email.Text);

                if (res.Status == true)
                {
                    await DisplayAlert("Success", res.Response, "Okay");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", res.Response, "Okay");
                }
                loading.IsVisible = false;
            }
        }
        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}