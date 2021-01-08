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
public partial class TabPage : TabbedPage
{
        DataClass dataClass = DataClass.GetInstance;
    public TabPage()
    {
        InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            profilePage.Name = dataClass.loggedInUser.name;
            profilePage.Email = dataClass.loggedInUser.email;
    }
}
}