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

using System.Data;
using System.IO;
using SQLite;

namespace Game_Finder
{
    [Table("UserAccount")]
    public class UserAccount
    {
        private long userId;
        private string userEmail;
        private string userPhone;
        private string userFirst;
        private string userLast;
        private string userZip;
        private string userPassword;

        public UserAccount()
        {
            userId = 0;
            userEmail = "test@email.com";
            userPhone = "111111111";
            userFirst = "Test";
            userLast = "Testerson";
            userZip = "12345";
            userPassword = "password";
        }
        public UserAccount(long id, string email, string phone, string first, string last, string zip, string password)
        {
            userId = id;
            userEmail = email;
            userPhone = phone;
            userFirst = first;
            userLast = last;
            userZip = zip;
            userPassword = password;
        }

        [PrimaryKey, AutoIncrement, Column("uacc_ID")]
        public long UserID
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        [MaxLength(50), Column("uacc_Email")]
        public string UserEmail
        {
            get
            {
                return userEmail;
            }

            set
            {
                userEmail = value;
            }
        }

        [MaxLength(20), Column("uacc_Phone")]
        public string UserPhone
        {
            get
            {
                return userPhone;
            }

            set
            {
                userPhone = value;
            }
        }

        [MaxLength(30), Column("uacc_FName")]
        public string UserFirst
        {
            get
            {
                return userFirst;
            }

            set
            {
                userFirst = value;
            }
        }

        [MaxLength(50), Column("uacc_LName")]
        public string UserLast
        {
            get
            {
                return userLast;
            }

            set
            {
                userLast = value;
            }
        }

        [MaxLength(11), Column("uacc_Zip")]
        public string UserZip
        {
            get
            {
                return userZip;
            }

            set
            {
                userZip = value;
            }
        }

        [MaxLength(30), Column("uacc_Pass")]
        public string UserPassword
        {
            get
            {
                return userPassword;
            }

            set
            {
                userPassword = value;
            }
        }

        /// <summary>
        /// Turns the UserAccount class into a string, which can be passed between activities.
        /// </summary>
        /// <returns>The UserAccount class in a string form.</returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6}", userId, userEmail, userPhone, userFirst, userLast, userZip, userPassword);
        }
    }
}