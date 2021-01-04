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
public partial class ProfilePage : ContentPage
{
        public ProfilePage()
        {
            InitializeComponent();
        }

        async private void signout_click(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            await Application.Current.SavePropertiesAsync();
            var acc = BindingContext as Account;
            Application.Current.MainPage = new MainPage(acc);
        }
    }
}