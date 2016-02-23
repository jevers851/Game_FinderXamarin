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
    [Table("Listing")]
    public class Listing
    {
        private long id;
        private long acct_id;
        private string title;
        private string description;
        private double price;
        private string gameTitle;
        private string gameSystem;
        private string gamePublisher;
        private string condition;
        private string status;

        public Listing()
        {
            Id = 0;
            Acct_id = 1;
            Title = "Test Listing Title";
            Description = "A good test game.";
            Price = 0.00;
            GameTitle = "Test Title";
            GameSystem = "Test System";
            GamePublisher = "Test Publisher";
            Condition = "Test Condition";
            Status = "Test Status";
        }
        public Listing(long newID, long newAcctID, string newTitle, string newDescription, 
            double newPrice, string newGameTitle, string newGameSystem, string newGamePublisher, string newCondition,
            string newStatus)
        {
            Id = newID;
            Acct_id = newAcctID;
            Title = newTitle;
            Description = newDescription;
            Price = newPrice;
            GameTitle = newGameTitle;
            GameSystem = newGameSystem;
            GamePublisher = newGamePublisher;
            Condition = newCondition;
            Status = newStatus;
        }

        [PrimaryKey, AutoIncrement, Column("ls_ID")]
        public long Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [Column("uacc_ID")]
        public long Acct_id
        {
            get
            {
                return acct_id;
            }

            set
            {
                acct_id = value;
            }
        }

        [MaxLength(255), Column("lsTitle")]
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        [MaxLength(255), Column("lsDescription")]
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        [Column("lsPrice")]
        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        [MaxLength(100), Column("lsGameTitle")]
        public string GameTitle
        {
            get
            {
                return gameTitle;
            }

            set
            {
                gameTitle = value;
            }
        }

        [MaxLength(25), Column("lsGameSystem")]
        public string GameSystem
        {
            get
            {
                return gameSystem;
            }

            set
            {
                gameSystem = value;
            }
        }

        [MaxLength(35), Column("lsGamePublisher")]
        public string GamePublisher
        {
            get
            {
                return gamePublisher;
            }

            set
            {
                gamePublisher = value;
            }
        }

        [MaxLength(20), Column("lsCondition")]
        public string Condition
        {
            get
            {
                return condition;
            }

            set
            {
                condition = value;
            }
        }

        [MaxLength(20), Column("lsStatus")]
        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        /// <summary>
        /// Turns the Listing object into a string form for passing between activities.
        /// </summary>
        /// <returns>The Listing object as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", id, acct_id, Title, Description, Price, GameTitle, GameSystem, GamePublisher, Condition);
        }
    }
}