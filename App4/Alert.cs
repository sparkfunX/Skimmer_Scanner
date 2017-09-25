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
    [Activity(Label = "Alert")]
    public class Alert : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Red);

            FindViewById<ImageView>(Resource.Id.imageView2).Click += delegate {
                var uri = Android.Net.Uri.Parse("https://twitter.com/intent/tweet?text=I%20just%20found%20a%20credit%20card%20skimmer%20using%20the%20%23SkimmerScanner%20app%20from%20%40sparkfun%21");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };

        }
    }
}