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
using Android.Graphics;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Game_Finder.Database;


namespace Game_Finder
{
    [Activity(Label = "GeneralList", Theme = "@style/DrawerTheme")]
    public class GeneralList : ActionBarActivity, ListView.IOnItemClickListener
    {
        Activity context;
        string[] items;
        string[] items2;

        private SupportToolbar mToolbar;
        private ActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
        private ListView generalList;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Create your application here
            SetContentView(Resource.Layout.GeneralList);
            //Making an example list of games
            DBRepository db = new DBRepository();
            IEnumerable<Listing> list;
            list = db.retrieveAllListings();
            int num = list.Count();

            items = new String[num];
            int i = 0;
            foreach( Listing l in list)
            {
                items[i] = l.Title.ToString();
                i++;
            }

  
            items2 = new String[] { "Info", "Info", "Info",
                "Info", "Info", "Info", "Info", "Info",
                "Info", "Info" };

            generalList = FindViewById<ListView>(Resource.Id.generalList);

            genListAdapter genList = new genListAdapter(this, items, items2);

            generalList.Adapter = genList;
            generalList.FastScrollEnabled = true;
            
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

            generalList.OnItemClickListener = this;

           /* generalList.ItemClick += delegate
            {
                var selectedValue = generalList.GetItemAtPosition(position)
                var intent = new Intent(this, typeof(Detail_Listing));
                //intent.PutExtra("Listing_ID", (string)generalList.SelectedItem);                
                intent.PutExtra("Listing_ID", ); //I don't know why this appears to be -1...
                string testListing = generalList.SelectedItem.ToString(); //for debugging
                StartActivity(intent);
            };//End GeneralList Delegate*/


        }//End OnCreate

        //On click listener event onitemclick
        //Passes the correct position value from the array. Starts from 0 going up. 
        //adding one to the value to correct value
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            //var selectedValue = position;
            var intent = new Intent(this, typeof(Detail_Listing));
            //intent.PutExtra("Listing_ID", (string)generalList.SelectedItem);                
            intent.PutExtra("Listing_ID", (id + 1)); //I don't know why this appears to be -1...
            //string testListing = generalList.SelectedItem.ToString(); //for debugging
            StartActivity(intent);
        }//End OnListItemClick


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
        public override bool OnOptionsItemSelected(IMenuItem item)
        {//this gets called when the hamburger button is selected. 
            mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }



    }//End GeneralList Class
    public class genListAdapter : BaseAdapter<string>
    {
        string[] items;
        string[] items2;
        Activity context;

        public genListAdapter(Activity context, string[] items, string[] items2) : base()
        {
            this.context = context;
            this.items = items;
            this.items2 = items2;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override string this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Length; }
        }

        //Method GETVIEW - Uses implemented Inflater to reuse Rowviews. If our data set is larger than an android device can view
        //it will load new data and save previous data (to a point). This limits total space usage and won't pull 
        //thousands of lines of data from the database at once. 
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
            {
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);
            }
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];
            //view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items2[position];


            //ImageView image = view.FindViewById<ImageView>(Android.Resource.Id.Icon);

            //image.SetImageResource(Resource.Drawable.error);
            


            TextView tv = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            tv.SetTextColor(new Color(255, 255, 255));
            //view.FindViewById<ImageView>(Resource.Id.icon).SetImageResource(items.ImageResourceID); Working on adding images. Currently need to change to get working
            return view;
        }
    }

}