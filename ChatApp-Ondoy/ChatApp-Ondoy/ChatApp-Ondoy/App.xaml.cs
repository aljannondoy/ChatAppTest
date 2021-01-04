using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_Ondoy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
