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

using Game_Finder.Database;
using SQLite;

namespace Game_Finder
{
    [Activity(Label = "CreateAccount", Theme = "@style/DrawerTheme")]
    public class Registration : Activity
    {
        private EditText firstName;
        private EditText lastName;
        private EditText zipCode;
        private EditText phone;
        private EditText email;
        private EditText pass1;
        private EditText pass2;
        private Button btnCreateAccount;
        private DBRepository dbr;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Registration);   //This tells the application to use the Registration.axml for the layout of this activity
                                                            // Create your application here

            firstName = FindViewById<EditText>(Resource.Id.txtFirstName);
            lastName = FindViewById<EditText>(Resource.Id.txtLastName);
            zipCode = FindViewById<EditText>(Resource.Id.txtZipCode);
            phone = FindViewById<EditText>(Resource.Id.txtPhone);
            email = FindViewById<EditText>(Resource.Id.txtEmail);
            pass1 = FindViewById<EditText>(Resource.Id.txtPassword);
            pass2 = FindViewById<EditText>(Resource.Id.txtPasswordConfirm);

            btnCreateAccount = FindViewById<Button>(Resource.Id.btnCreateAccount);
            btnCreateAccount.Click += delegate
            {
                CreateAccount();
            };
        }
        private bool CheckRequirements()
        {
            bool validated = true;

            if (pass2.Text.ToString().Length == 0 || pass2.Text != pass1.Text)
            {
                pass2.SetError("Passwords do not match", null);
                pass2.RequestFocus();
                validated = false;
            }
            if (pass1.Text.ToString().Length == 0)
            {
                pass1.SetError("Password is required", null);
                pass1.RequestFocus();
                validated = false;
            }
            else
            {
                if(!checkPasswordRequirements(pass1.Text))
                {
                    pass1.SetError("Password must contain the following: at least 8 characters long, at least 1 uppercase letter, at least 1 lowercase letter", null);
                    pass1.RequestFocus();
                    validated = false;
                }
            }
            if (email.Text.ToString().Length == 0)
            {
                email.SetError("A valid email is required", null);
                email.RequestFocus();
                validated = false;
            }
            if (phone.Text.ToString().Length == 0)
            {
                phone.SetError("Phone number is required", null);
                phone.RequestFocus();
                validated = false;
            }
            if (zipCode.Text.ToString().Length == 0)
            {
                zipCode.SetError("Zip code is required", null);
                zipCode.RequestFocus();
                validated = false;
            }
            if (lastName.Text.ToString().Length == 0)
            {
                lastName.SetError("Last name is required", null);
                lastName.RequestFocus();
                validated = false;
            }
            if (firstName.Text.ToString().Length == 0)
            {
                firstName.SetError("First name is required", null);                
                firstName.RequestFocus();
                validated = false;
            }

            return validated;
        }

        private bool checkPasswordRequirements(string pass)
        {
            bool hasUppercase = false;
            bool hasLowercase = false;

            if(pass.Length < 8)
            {
                return false;
            }
            else
            {
                foreach (char c in pass)
                {
                    if (c >= 65 && c <= 90)
                        hasUppercase = true;

                    if (c >= 97 && c <= 122)
                        hasLowercase = true;
                }
            }

            if (hasUppercase == true && hasLowercase == true)
                return true;
            else
                return false;
        }

        public void CreateAccount()
        {
            if (CheckRequirements())
            {
                UserAccount user = new UserAccount();
                user.UserFirst = firstName.Text;
                user.UserLast = lastName.Text;
                user.UserZip = zipCode.Text;
                user.UserPhone = phone.Text;
                user.UserEmail = email.Text;
                user.UserPassword = pass1.Text;

                dbr = new DBRepository();

                if (dbr.retrieveAccountByEmail(user.UserEmail) == null)
                {
                    dbr.insertAccountRecord(user);

                    Intent i = new Intent(this, typeof(MainActivity));
                    i.SetFlags(ActivityFlags.ClearTop | ActivityFlags.ClearTask | ActivityFlags.NewTask);
                    StartActivity(i);
                }
                else
                {
                    Android.App.AlertDialog.Builder exceptionalert = new Android.App.AlertDialog.Builder(this);
                    exceptionalert.SetTitle("Account Already Exists.");
                    exceptionalert.SetMessage("The email address: \n" + user.UserEmail + "\nis already being used.");
                    exceptionalert.SetPositiveButton("Okay", (s, ev) =>
                    {
                        //Do nothing
                    });
                    exceptionalert.Show();
                }


            }
        }


    }
}