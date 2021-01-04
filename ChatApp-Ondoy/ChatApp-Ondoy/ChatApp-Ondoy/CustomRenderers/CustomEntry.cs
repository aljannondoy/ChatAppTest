using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace ChatApp_Ondoy
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry), Color.Gray);
        // Gets or sets BorderColor value  
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(CustomEntry), Device.OnPlatform<int>(1, 2, 2));
        // Gets or sets BorderWidth value  
        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty IsCurvedCornersEnabledProperty = BindableProperty.Create(nameof(IsCurvedCornersEnabled), typeof(bool), typeof(CustomEntry), true);
        // Gets or sets IsCurvedCornersEnabled value  
        public bool IsCurvedCornersEnabled
        {
            get => (bool)GetValue(IsCurvedCornersEnabledProperty);
            set => SetValue(IsCurvedCornersEnabledProperty, value);
        }

        public static readonly BindableProperty IsEmptyEnabledProperty = BindableProperty.Create(nameof(IsEmptyEnabled), typeof(bool), typeof(CustomEntry), true);
        // Gets or sets IsCurvedCornersEnabled value  
        public bool IsEmptyEnabled
        {
            get => (bool)GetValue(IsEmptyEnabledProperty);
            set => SetValue(IsEmptyEnabledProperty, value);
        }
    }
}
