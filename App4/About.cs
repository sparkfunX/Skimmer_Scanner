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

namespace SkimmerScanner
{
    [Activity(Label = "About")]
    public class About : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.About);

            var link = FindViewById<TextView>(Resource.Id.textView3);
            link.Click += (sender, e) =>
            {
                var uri = Android.Net.Uri.Parse("https://learn.sparkfun.com/tutorials/gas-pump-skimmers");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };


        }
    }
}