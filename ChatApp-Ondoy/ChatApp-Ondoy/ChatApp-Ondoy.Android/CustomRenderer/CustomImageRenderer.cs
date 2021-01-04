using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChatApp_Ondoy;
using ChatApp_Ondoy.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(CustomImage), typeof(CustomImageRenderer))]
namespace ChatApp_Ondoy.Droid
{
    class CustomImageRenderer : ImageRenderer
    {
        public CustomImageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            var view = (CustomImage)Element;
            var outline = new GradientDrawable();
            if (view.IsCurvedCornersEnabled)
            {
                outline.SetCornerRadius(15f);
                Control.SetBackground(outline);
            }

        }
    }
}