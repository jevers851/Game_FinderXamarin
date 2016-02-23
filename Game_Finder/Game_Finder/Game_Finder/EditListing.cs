using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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
    [Activity(Label = "Edit Listing", Theme = "@style/DrawerTheme")]
    public class EditListing : ActionBarActivity
    {
        private Listing listing = new Listing();
        private TextView txtemail;
        private TextView txtphone;
        private EditText editlistingtitle;
        private EditText editname;
        private EditText editsystem;
        private EditText editpublisher;
        private EditText editquality;
        private EditText editstatus;
        private EditText editprice;
        private EditText editdescription;

        private SupportToolbar mToolbar;
        private ActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
        DBRepository db = new DBRepository();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.EditListing);

            string productID = Intent.GetStringExtra("Listing_ID") ?? "1";

            //Set up and create the left drawer

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

            //Retrieve information from MyListings activity regarding listing to show.
            //listing.setUserEmail("text");
            //listing.setUserPhone("");
            listing.Title = "";
            listing.Description = "";
            listing.Price = 0.0;
            listing.GameTitle = "";
            listing.GamePublisher = "";
            listing.GameSystem = "";
            listing.Condition = "";

            listing = db.retrieveListingRecordByID(long.Parse(productID));

            txtemail = FindViewById<TextView>(Resource.Id.txtemail);
            txtphone = FindViewById<TextView>(Resource.Id.txtphone);
            editlistingtitle = FindViewById<EditText>(Resource.Id.editlistingtitle);
            editname = FindViewById<EditText>(Resource.Id.editname);
            editsystem = FindViewById<EditText>(Resource.Id.editsystem);
            editpublisher = FindViewById<EditText>(Resource.Id.editpublisher);
            editquality = FindViewById<EditText>(Resource.Id.editquality);
            editstatus = FindViewById<EditText>(Resource.Id.editstatus);
            editprice = FindViewById<EditText>(Resource.Id.editprice);
            editdescription = FindViewById<EditText>(Resource.Id.editdescription);
            Button btnupdate = FindViewById<Button>(Resource.Id.btnupdatelisting);
            Button btndelete = FindViewById<Button>(Resource.Id.btndeletelisting);

            //txtemail.Text = listing.getUserEmail();
            //txtphone.Text = listing.getUserPhone();
            editlistingtitle.Text = listing.Title;
            editname.Text = listing.GameTitle;
            editsystem.Text = listing.GameSystem;
            editpublisher.Text = listing.GamePublisher;
            editquality.Text = listing.Condition;
            editstatus.Text = listing.Status;
            editprice.Text = listing.Price.ToString();
            editdescription.Text = listing.Description;

            btnupdate.Click += delegate
            {
                updateListing(listing);
            };

            btndelete.Click += delegate
            {
                deleteListing(listing);
            };
        }

        public void updateListing(Listing list)
        {
            if(checkListingFields())
            {
                //Attempt connection to DB
                try
                {
                    listing.Title = editlistingtitle.Text;
                    listing.GameTitle = editname.Text;
                    listing.GameSystem = editsystem.Text;
                    listing.GamePublisher = editpublisher.Text;
                    listing.Condition = editquality.Text;
                    listing.Status = editstatus.Text;
                    listing.Price = Double.Parse(editprice.Text);
                    listing.Description = editdescription.Text;

                    db.updateListing(list);

                    var intent = new Intent(this, typeof(EditListing));
                    intent.PutExtra("Listing_ID", listing.Id.ToString());
                    StartActivity(intent);
                }
                catch
                {

                }
            }
        }

        public void deleteListing(Listing list)
        {
            Android.App.AlertDialog.Builder deletealert = new Android.App.AlertDialog.Builder(this);          

            deletealert.SetTitle("Delete Listing");
            deletealert.SetMessage("Are you sure you wish to delete this listing?");
            deletealert.SetPositiveButton("Yes", (s, ev) =>
            {
                //Connect to DB
                
                //Perform update on listing's deleted status.
                db.deleteListing(list.Id);
            });
            deletealert.SetNegativeButton("No", (s, ev) =>
            {
                //Do Nothing
            });
            deletealert.Show();
        }

        public bool checkListingFields()
        {
            bool isValid = true;

            if (editlistingtitle.Text.Length == 0)
            {
                editlistingtitle.SetError("A listing title is required", null);
                editlistingtitle.RequestFocus();
                isValid = false;
            }

            if (editname.Text.Length == 0)
            {
                editname.SetError("A game name is required.", null);
                editname.RequestFocus();
                isValid = false;
            }

            if (editsystem.Text.Length == 0)
            {
                editsystem.SetError("A system the game is played on is required.", null);
                editsystem.RequestFocus();
                isValid = false;
            }

            if(editpublisher.Text.Length == 0)
            {
                editpublisher.SetError("A publisher of the game is required.", null);
                editpublisher.RequestFocus();
                isValid = false;
            }

            if(editquality.Text.Length == 0)
            {
                editquality.SetError("The quality of the game's physical condition is required.", null);
                editquality.RequestFocus();
                isValid = false;
            }

            if(editstatus.Text.Length == 0)
            {
                editstatus.SetError("The current status of the listing is required.", null);
                editstatus.RequestFocus();
                isValid = false;
            }

            if(editprice.Text.Length == 0)
            {
                editprice.SetError("A price for the game must be entered", null);
                editprice.RequestFocus();
                isValid = false;
            }

            if(editdescription.Text.Length == 0)
            {
                editdescription.SetError("You must enter a description for the listing.", null);
                editdescription.RequestFocus();
                isValid = false;
            }

            return isValid;
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