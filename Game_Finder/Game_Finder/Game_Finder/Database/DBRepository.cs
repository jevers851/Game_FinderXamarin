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

namespace Game_Finder.Database
{
    public class DBRepository
    {
        private string dbpath = Path.Combine
                (System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                "GameFinder.db3");

        /// <summary>
        /// Create the SQLite Database for our Application.
        /// </summary>
        public void createDB()
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
        }

        /// <summary>
        /// Create the tables for our application.
        /// </summary>
        public void createTables()
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            db.CreateTable<UserAccount>();
            db.CreateTable<Listing>();
        }

        /// <summary>
        /// Inserts a UserAccount object into the UserAccount Table.
        /// </summary>
        /// <param name="acct">The account object you wish to insert.</param>
        public void insertAccountRecord(UserAccount acct)
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            db.Insert(acct);
        }

        /// <summary>
        /// Retrieves a UserAccount record from the UserAccount Table.
        /// </summary>
        /// <param name="ID">The ID of the UserAccount you wish to return.</param>
        /// <returns>A UserAccount object.</returns>
        public UserAccount retrieveAccounttRecordByID(long ID)
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            UserAccount acct = db.Get<UserAccount>(ID);
            return acct;
        }

        /// <summary>
        /// Inserts a Listing record into the Listing table.
        /// </summary>
        /// <param name="list">The listing object you wish to insert.</param>
        public void insertListingRecord(Listing list)
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            db.Insert(list);
        }

        /// <summary>
        /// Retrieves a Listing record from the Listing Table.
        /// </summary>
        /// <param name="ID">The ID of the Listing you wish to return.</param>
        /// <returns>A Listing object.</returns>
        public Listing retrieveListingRecordByID(long ID)
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            Listing list = db.Get<Listing>(ID);
            return list;
        }

        /// <summary>
        /// Retrieves all of the Listing records in the Listing Table.
        /// </summary>
        /// <returns>A TableQuery of Listing objects.</returns>
        public IEnumerable<Listing> retrieveAllListings()
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            var table = db.Table<Listing>();
            return table;
        }

        /// <summary>
        /// Retrieves an Account based on the email address.
        /// </summary>
        /// <param name="acctemail">The email address you wish to retrieve.</param>
        /// <returns>Null if the account doesn't exist. The account if the account exists.</returns>
        public UserAccount retrieveAccountByEmail(string acctemail)
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            var acct = from a in db.Table<UserAccount>()
                       where a.UserEmail.Equals(acctemail)
                       select a;

            if (acct.Count() == 0)
                return null;
            else
                return acct.First();
        }

        /// <summary>
        /// Retrieves an Account based on the email address and password.
        /// </summary>
        /// <param name="acctemail">The email address of the account.</param>
        /// <param name="acctpass">The password of the account.</param>
        /// <returns>True if the login information is correct. False if the login information is incorrect.</returns>
        public bool confirmLogin(string acctemail, string acctpass)
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            var acct = from a in db.Table<UserAccount>()
                       where a.UserEmail.Equals(acctemail) && a.UserPassword.Equals(acctpass)
                       select a;

            if (acct.Count() == 1)
                return true;
            else
                return false;
        }
        public void deleteListing(long id)
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            db.Delete<Listing>(id);
        }
        public void updateListing(Listing list)
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);
            db.Update(list);            
        }
        public void createListings()
        {
            SQLiteConnection db = new SQLiteConnection(dbpath);

            Listing list = new Listing();
            db.Insert(list);
            list = new Listing();
            db.Insert(list);
            list = new Listing();
            db.Insert(list);
            list = new Listing();
            db.Insert(list);
            list = new Listing();
            db.Insert(list);
        }

    }
}