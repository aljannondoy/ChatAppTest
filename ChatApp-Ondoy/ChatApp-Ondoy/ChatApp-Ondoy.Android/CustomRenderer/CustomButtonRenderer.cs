using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ChatApp_Ondoy.Droid;
using Android.Content;
using ChatApp_Ondoy;
[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace ChatApp_Ondoy.Droid
{
    class CustomButtonRenderer : ButtonRenderer
    {
        public CustomButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            CustomButton elem = Element as CustomButton;
            Control.SetAllCaps(elem.AutoCapitalization);

        }
    }
}