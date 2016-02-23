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
    [Activity(Label = "Settings")]
    public class Settings : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Settings);
            // Create your application here.
            // I honestly don't know what kind of settings our app is going to have.
            // I just included a setting to use your current location to search for games and a place to link to the about page
        }
    }
}