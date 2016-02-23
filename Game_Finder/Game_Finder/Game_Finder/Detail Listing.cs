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
using Game_Finder.Database;

namespace Game_Finder
{
    [Activity(Label = "Detail Listing", Theme = "@style/DrawerTheme")]
    public class Detail_Listing : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private ActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;

        private Button btnContact;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Detailed_Listing);

            DBRepository db = new DBRepository();
            long l = 1;
            Listing list = new Listing();
            long text = Intent.GetLongExtra("Listing_ID", l);
            //long value = Convert.ToInt64(text);


            list = db.retrieveListingRecordByID(text);

            //Finding views by id
            TextView txtname = FindViewById<TextView>(Resource.Id.txtname);
            TextView txtsystem = FindViewById<TextView>(Resource.Id.txtsystem);
            TextView txtpublisher = FindViewById<TextView>(Resource.Id.txtpublisher);
            TextView txtquality = FindViewById<TextView>(Resource.Id.txtquality);
            TextView txtstatus = FindViewById<TextView>(Resource.Id.txtstatus);
            TextView txtdescription = FindViewById<TextView>(Resource.Id.txtdescription);
            TextView txtemail = FindViewById<TextView>(Resource.Id.txtemail);
            TextView lbllistingtitle = FindViewById<TextView>(Resource.Id.lbllistingtitle);
            

            //Setting retrieved data to the correct 
            txtname.Text = list.GameTitle;
            txtsystem.Text = list.GameSystem;
            txtpublisher.Text = list.GamePublisher;
            txtquality.Text = list.Condition;
            txtstatus.Text = list.Status;
            txtdescription.Text = list.Description;
            lbllistingtitle.Text = list.Title;
            
            UserAccount account = db.retrieveAccounttRecordByID(list.Acct_id);

            txtemail.Text = account.UserEmail;


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

            btnContact = FindViewById<Button>(Resource.Id.btncontact);

            btnContact.Click += delegate
            {
                var intent = new Intent(this, typeof(EditListing));
                intent.PutExtra("Listing_ID", text);
                StartActivity(intent);
                //StartActivity(typeof(EditListing)); //This is only here to test the edit listing page. I didn't see another way to get to it..
            };
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

        public void displayContactInfo()
        {
            Android.App.AlertDialog.Builder contactalert = new Android.App.AlertDialog.Builder(this);
            contactalert.SetTitle("Lister's Contact Information");
            contactalert.SetMessage("Email: " + FindViewById<TextView>(Resource.Id.txtemail).Text + "\nPhone: " +
                FindViewById<TextView>(Resource.Id.txtphone).Text);
            contactalert.SetPositiveButton("Okay", (s, ev) =>
            {
                //Do nothing
            });
            contactalert.Show();
        }
    }
}