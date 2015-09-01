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
    /// viewmodel to hold all msgs
    /// </summary>
    class ViewModel
    {
        public  Dictionary<int, MyMessage> msgContainer { get; set; }
        public List<MyMessage> msgsContainer { get; set; }

        private static ViewModel myViewModel = null;
        public static ViewModel myViewModelInstance
        {
            get
            {
                // Delay creation of the view model until necessary
                return myViewModel ?? (myViewModel = new ViewModel());
            }
        }

        private ViewModel()
        {
           // myViewModel = new ViewModel();
            msgContainer = new Dictionary<int, MyMessage>();
            msgsContainer = new List<MyMessage>();
        }

    }
}