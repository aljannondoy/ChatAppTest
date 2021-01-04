using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ChatApp_Ondoy;
using ChatApp_Ondoy.Droid;
using Android.Graphics.Drawables;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace ChatApp_Ondoy.Droid
{
    class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var view = (CustomEntry)Element;
                var outline = new GradientDrawable();
                outline.SetShape(ShapeType.Rectangle);
                outline.SetColor(view.BackgroundColor.ToAndroid());
                outline.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());
                e.NewElement.Focused += (sender, evt) =>
                {
                    view.BorderColor = Color.Black;
                };
                if (view.IsCurvedCornersEnabled)
                {
                    outline.SetCornerRadius(15f);
                }
                Control.SetPadding(20, Control.PaddingTop, 20, Control.PaddingTop);
                Control.SetBackground(outline);
            }

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (CustomEntry)Element;
            var outline = new GradientDrawable();
            outline.SetShape(ShapeType.Rectangle);
            outline.SetColor(view.BackgroundColor.ToAndroid());
            outline.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

            if (view.IsCurvedCornersEnabled)
            {
                outline.SetCornerRadius(15f);
            }
            Control.SetPadding(20, Control.PaddingTop, 20, Control.PaddingTop);
            Control.SetBackground(outline);
        }

    }
}