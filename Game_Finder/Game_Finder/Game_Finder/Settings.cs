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
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace Game_Finder
{
    [Activity(Label = "Settings", Theme = "@style/DrawerTheme")]
    public class Settings : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private ActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Settings);
            // Create your application here.
            // I honestly don't know what kind of settings our app is going to have.
            // I just included a setting to use your current location to search for games and a place to link to the about page

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            SetSupportActionBar(mToolbar);
            mDrawerToggle = new ActionBarDrawerToggle(
                this,       //Host Activity
                mDrawerLayout,
                Resource.String.ApplicationName,
                Resource.String.ApplicationName);
            mLeftDataSet = new List<string>(); //adding data to the left drawer
            mLeftDataSet.Add("My Profile");
            mLeftDataSet.Add("Settings");
            mLeftDataSet.Add("Search Listings");
            mLeftDataSet.Add("Log Out");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet); //adds the items to the left drawer listview
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += DrawerListOnItemClick;
            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            //SupportActionBar.SetHomeButtonEnabled(true);
            //SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            mDrawerToggle.SyncState();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {//this gets called when the hamburger button is selected. 
            mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }
        private void DrawerListOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            switch (itemClickEventArgs.Position)
            {
                case 0:
                    //StartActivity(typeof(MyProfile)); Do nothing, we are already here. 
                    break;
                case 1:
                    StartActivity(typeof(Settings));
                    break;
                case 2:
                    StartActivity(typeof(GeneralList));
                    break;
                case 3:
                    Intent i = new Intent(this, typeof(MainActivity));
                    i.SetFlags(ActivityFlags.ClearTop | ActivityFlags.ClearTask | ActivityFlags.NewTask);
                    StartActivity(i);
                    break;
            }
        }
    }
}