using Plugin.CloudFirestore;
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
        DataClass dataClass = DataClass.GetInstance;
        public SignUpPage()
        {
            InitializeComponent();
        }
        
        async private void signup_click(object sender, EventArgs e)
        {

            if (email.Text != "" && user.Text != "" && pass.Text != "")
            {
                if (pass.Text == pass2.Text)
                {
                    loading.IsVisible = true;

                    FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                    res = await DependencyService.Get<iFirebaseAuth>().SignUpWithEmailPassword(user.Text, email.Text, pass.Text);

                    if (res.Status == true)
                    {
                        try
                        {
                            await CrossCloudFirestore.Current
                             .Instance
                             .GetCollection("users")
                             .GetDocument(dataClass.loggedInUser.uid)
                             .SetDataAsync(dataClass.loggedInUser);

                            await DisplayAlert("Success", res.Response, "Okay");
                            await Navigation.PopModalAsync(true);
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Error", ex.Message, "Okay");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", res.Response, "Okay");
                    }
                    loading.IsVisible = false;
                }
                else
                {
                    
                    await DisplayAlert("Error", "Passwords don't match", "Okay");
                    pass2.Text = string.Empty;
                    pass2.Focus();
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