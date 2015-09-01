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
using Java.Util;

namespace MessengerCSCI641
{
    /// <summary>
    /// a class representing a user
    /// </summary>
    public class User :Java.Lang.Object

    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public int? accuracy { get; set; }
        public DateTime? lastUpdated { get; set; }

       
        //public Dictionary<int, Message> messages { get; set; }

        public override string ToString()
        {
            return (this.first_name + " "+ this.last_name);
        }

       
      
    }
}