using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChatApp_Ondoy
{
    public class CustomImage : Image
    {
        public static readonly BindableProperty IsCurvedCornersEnabledProperty = BindableProperty.Create(nameof(IsCurvedCornersEnabled), typeof(bool), typeof(CustomEntry), true);
        // Gets or sets IsCurvedCornersEnabled value  
        public bool IsCurvedCornersEnabled
        {
            get => (bool)GetValue(IsCurvedCornersEnabledProperty);
            set => SetValue(IsCurvedCornersEnabledProperty, value);
        }
    }
}
