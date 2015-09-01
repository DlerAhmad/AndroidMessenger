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

namespace MessengerCSCI641
{
    /// <summary>
    /// a class representig a msg
    /// </summary>
    class MyMessage
    {
        public int message_id { get; set; }
        public string msg_type { get; set; }
        public string message { get; set; }
        public DateTime ts { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

    }
}