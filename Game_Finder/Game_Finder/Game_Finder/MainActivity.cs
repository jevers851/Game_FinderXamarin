using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using SQLite;
using Game_Finder.Database;

namespace Game_Finder
{
    [Activity(Label = "Game Finder", MainLauncher = true, Icon = "@drawable/new_new_icon_because_josh_hated_the_old_one", 
        Theme = "@style/DrawerTheme")]
    public class MainActivity : Activity
    {
        private EditText txtuser;
        private EditText txtpass;
        DBRepository dbr;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Setup DB
            dbr = new DBRepository();
            dbr.createDB();
            dbr.createTables();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            txtuser = FindViewById<EditText>(Resource.Id.txtusername);
            txtpass = FindViewById<EditText>(Resource.Id.txtpassword);

            // Get our button from the layout resource,
            // and attach an event to it
            Button btnSignIn = FindViewById<Button>(Resource.Id.btnsignin);
            Button btnCreateAccount = FindViewById<Button>(Resource.Id.btncreateacct);

            btnSignIn.Click += delegate
            {
                //If signin == correct then proceed to Home page
                //Connect to database and validate
                login(txtuser.Text, txtpass.Text);
            };

            
            btnCreateAccount.Click += delegate
            {
                StartActivity(typeof(Registration));
            };
        }

        public void login(string username, string password)
        {
            if(checkLoginFields())
            {
                if(dbr.confirmLogin(username, password))
                {
                    Intent intent = new Intent(this, typeof(Home));
                    intent.PutExtra("Acct_Email", username);
                    StartActivity(intent);
                }
                else
                {
                    Android.App.AlertDialog.Builder loginalert = new Android.App.AlertDialog.Builder(this);
                    loginalert.SetTitle("Incorrect Log-in Information");
                    loginalert.SetMessage("Username or Password is incorrect.");
                    loginalert.SetPositiveButton("Okay", (s, ev) =>
                    {
                        //Do nothing
                    });
                    loginalert.Show();
                }                
            }
        }

        public bool checkLoginFields()
        {
            bool isValid = true;

            if(txtuser.Text.Length == 0)
            {
                txtuser.SetError("A username must be entered.", null);
                txtuser.RequestFocus();
                isValid = false;
            }

            if(txtpass.Text.Length == 0)
            {
                txtpass.SetError("A password must be entered.", null);
                txtpass.RequestFocus();
                isValid = false;
            }

            return isValid;
        }
        
    }
}

