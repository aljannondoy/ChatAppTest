using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;


namespace ChatApp_Ondoy
{
    public class IDGenerator
    {
        public static string generateID()
        {
            var samp = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var num = "0123456789";
            var IDgen = new char[20];
            var IDran = new Random();
            int ctr;

            for (ctr = 0; ctr < 20; ctr++)
            {
                var arran = IDran.Next(IDgen.Length);
                IDgen[ctr] = samp[IDran.Next(samp.Length)];
                if(arran != ctr)
                {
                    IDgen[arran] = num[IDran.Next(num.Length)];
                }

            }
            return new string(IDgen);
        }
    }
}
