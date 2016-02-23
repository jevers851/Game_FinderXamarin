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
using SQLite;
using Game_Finder.Database;

namespace Game_Finder
{
    [Activity(Label = "CreateListing", Theme = "@style/DrawerTheme")]
    public class CreateListing : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private ActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
        private Button btnCreateListing;

        public string username;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CreateListing);   //This tells the application to use the Registration.axml for the layout of this activity
            // Create your application here
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            btnCreateListing = FindViewById<Button>(Resource.Id.btnPostingCreate);
            btnCreateListing.Click += delegate
            {
                //Create a listener for the create listing button to create a posting on click
                //I think this works at least...
                AddListing();
            };

            username = Intent.GetStringExtra("Acct_Email");

            SetSupportActionBar(mToolbar);

            mDrawerToggle = new ActionBarDrawerToggle(
                this,       //Host Activity
                mDrawerLayout,
                Resource.String.ApplicationName,
                Resource.String.ApplicationName);

            mLeftDataSet = new List<string>(); //adding data to the left drawer
            mLeftDataSet.Add("My Account");
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
        public void AddListing()
        {
            EditText txtTitle = FindViewById<EditText>(Resource.Id.txtPostingTitle);
            EditText txtPrice = FindViewById<EditText>(Resource.Id.txtPostingPrice);
            EditText txtDescription = FindViewById<EditText>(Resource.Id.txtPostingDescription);
            EditText txtGameTitle = FindViewById<EditText>(Resource.Id.txtPostingGameTitle);
            EditText txtSystem = FindViewById<EditText>(Resource.Id.txtPostingGameSystem);

            if (CheckRequirements())
            {
                
                //add the stuff to the database
                DBRepository db = new DBRepository();
                UserAccount user = db.retrieveAccountByEmail(username);
                Listing list = new Listing(1, user.UserID, txtTitle.Text.ToString(), txtDescription.Text.ToString(), Convert.ToDouble(txtPrice.Text), txtGameTitle.Text.ToString(), txtSystem.Text.ToString(), "Publisher", "Condition", "Good");
                db.insertListingRecord(list);
            }
            else
            {
                //do nothing?
            }
        }
        private bool CheckRequirements()
        {
            bool validated = true;

            EditText txtTitle = FindViewById<EditText>(Resource.Id.txtPostingTitle);
            EditText txtPrice = FindViewById<EditText>(Resource.Id.txtPostingPrice);
            EditText txtDescription = FindViewById<EditText>(Resource.Id.txtPostingDescription);
            EditText txtGameTitle = FindViewById<EditText>(Resource.Id.txtPostingGameTitle);
            EditText txtSystem = FindViewById<EditText>(Resource.Id.txtPostingGameSystem);

            if (txtSystem.Text.ToString().Length == 0)
            {
                txtSystem.SetError((string)"System is required", GetDrawable(Resource.Drawable.error));
                txtSystem.RequestFocus();
                validated = false;
            }
            if (txtGameTitle.Text.ToString().Length == 0)
            {
                txtGameTitle.SetError((string)"Game Title is required", GetDrawable(Resource.Drawable.error));
                txtGameTitle.RequestFocus();
                validated = false;
            }
            if (txtDescription.Text.ToString().Length == 0)
            {
                txtDescription.SetError((string)"Description is required", GetDrawable(Resource.Drawable.error));
                txtDescription.RequestFocus();
                validated = false;
            }
            if (txtPrice.Text.ToString().Length == 0)
            {
                txtPrice.SetError((string)"Price is required", GetDrawable(Resource.Drawable.error));
                txtPrice.RequestFocus();
                validated = false;
            }
            if (txtTitle.Text.ToString().Length == 0)
            {
                txtTitle.SetError((string)"Title is required", GetDrawable(Resource.Drawable.error));
                txtTitle.RequestFocus();                
                validated = false;
            }

            return validated;
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
                    StartActivity(typeof(MyProfile));
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