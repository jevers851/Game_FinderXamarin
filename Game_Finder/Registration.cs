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

namespace Game_Finder
{
    [Activity(Label = "Activity1")]
    public class Registration : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Registration);   //This tells the application to use the Registration.axml for the layout of this activity
            // Create your application here
        }
    }
}