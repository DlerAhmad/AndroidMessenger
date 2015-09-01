using System.Collections.Generic;
using System.Collections;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using Android.Content.Res;
using Android.Locations;
using Android.Util;

namespace MessengerCSCI641
{
    /// <summary>
    /// main activity that holds map info and users list as a left drawer
    /// </summary>
    

    [Activity(Label = "MessengerCSCI641", Icon = "@drawable/icon" , Theme = "@style/CustomActionBarTheme")]
    
    public class MapActivity : Activity , IOnMapReadyCallback , ILocationListener
    {
        public static User client;
        private GoogleMap GMap;
        private User[] users;

        //left drawer attributes
        DrawerLayout leftDrawerLayaout;
        List<User> leftDrawerItems = new List<User>();
        ArrayAdapter leftDrawerAdapter;
        ListView leftDrawer;
        ActionBarDrawerToggle leftDrawerToggle;

        //location stuff
        Location currentLocation;
        LocationManager locationManager;
        String locationProvider;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            locationManager = GetSystemService(Context.LocationService) as LocationManager;

            //set the user current location
            setlocation();

            //setup the map
            SetupMap();


            //set left drawer staff
            leftDrawerLayaout = FindViewById<DrawerLayout>(Resource.Id.myDrawer);
            leftDrawer = FindViewById<ListView>(Resource.Id.leftListView);

            //get users 
            populateUsersOnDrawer();

            leftDrawerToggle = new ActionBarDrawerToggle(this, leftDrawerLayaout, Resource.Drawable.menu, Resource.String.drawer_open, Resource.String.drawer_close);
            leftDrawerLayaout.SetDrawerListener(leftDrawerToggle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayShowTitleEnabled(true);
           
            //start message service 
            StartService(new Intent(this, typeof(MsgApiService)));

        }

        private async void setlocation()
        {
            //getting location of client
            try
            {
                Criteria criteriaForLocationService = new Criteria();
                criteriaForLocationService.Accuracy = Accuracy.Fine;
                locationProvider = locationManager.GetBestProvider(criteriaForLocationService, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("*********************************************************************************************************");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("***********************************************************************************************");
            }

            if (locationProvider != null)
            {
                locationManager.RequestLocationUpdates(locationProvider, 2000, 1, this);
                currentLocation = locationManager.GetLastKnownLocation(locationProvider);
                client.latitude = currentLocation.Latitude;
                client.longitude = currentLocation.Longitude;
                client.lastUpdated = DateTime.Now;
                client.accuracy = 100;
                string result = await ApiRequests.setLocation(client);
                Console.WriteLine("***********************************");
                Console.WriteLine("result = " + result);
            }
            else
            {
                Console.WriteLine("******************** elsee  ***************");

            }

        }

        protected override void OnPostCreate(Bundle bundle)
        {
            base.OnPostCreate(bundle);
            leftDrawerToggle.SyncState();

        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            leftDrawerToggle.OnConfigurationChanged(newConfig);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if(leftDrawerToggle.OnOptionsItemSelected(item))
            {
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void SetupMap()
        {
            if(GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);

            }
        }
        private void MapOnMarkerClick(object sender, GoogleMap.InfoWindowClickEventArgs markerClickEventArgs)
        {

            Marker marker = markerClickEventArgs.Marker;
            string[] name = marker.Title.Split(new char[0]);
            Intent intent = new Intent(this, typeof(ChatActivity));
            intent.PutExtra("email", marker.Snippet);
            intent.PutExtra("firstName", name[0]);
            intent.PutExtra("lastName", name[1]);

            StartActivity(intent);
            marker.Flat = false;
        }

        public async void OnMapReady(GoogleMap googleMap)
        {
            GMap = googleMap;
            GMap.InfoWindowClick += MapOnMarkerClick;
            
            // users = new ArrayList();
            string result= await ApiRequests.getLocations(client);
            if (result!= "error")
            {
                users = JsonConvert.DeserializeObject<User[]>(result);
                

                foreach (User user in users)
                {
                    if (user.latitude != null)
                    {
                        GMap.AddMarker(new MarkerOptions()
                            .SetTitle(user.first_name + " " + user.last_name)
                            
                            .SetSnippet(user.email)
                            .InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.marker))
                            .SetPosition(new LatLng((double)user.latitude, (double)user.longitude)));
                    }
                }
            }

            GMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng((double)client.latitude, (double)client.longitude), 12));

           
        }

        public async void populateUsersOnDrawer()
        {
            int count = 0;
            string result = await ApiRequests.getUsers(client);
            if (result != "error")
            {
                User[] users = JsonConvert.DeserializeObject<User[]>(result);

                foreach (User user in users)
                {
                    
                    leftDrawerItems.Add(user);

                }
            }

            leftDrawerAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, leftDrawerItems);
            //leftDrawer.SetBackgroundColor(Android.Graphics.Color.AliceBlue);
            
            
            leftDrawer.Adapter = leftDrawerAdapter;

            //add a click listner for items in the viewlist
            leftDrawer.ItemClick+= onListViewItemClick;
   
        }


        public void onListViewItemClick(object sender, EventArgs ea)
        {
            ListView listView = sender as ListView;

            int userPos = listView.CheckedItemPosition;
            var user = listView.GetItemAtPosition(userPos).JavaCast<User>();

            var chatActivity = new Intent(this, typeof(ChatActivity));
            if (user != null)
            {
                chatActivity.PutExtra("email", user.email);
                chatActivity.PutExtra("firstName", user.first_name);
                chatActivity.PutExtra("lastName", user.last_name);
                chatActivity.PutExtra("msg", "hi there this is working");
            }
            else
            {
                chatActivity.PutExtra("email", "not working");
                chatActivity.PutExtra("msg", user.ToString());
            }

            //chatActivity.PutExtra("msg", "hi there");
            StartActivity(chatActivity);
        }
                           
        protected override void OnDestroy()
        {
            StopService(new Intent(this, typeof(MsgApiService)));
            base.OnDestroy();
        }

        public async void OnLocationChanged(Location location)
        {
            currentLocation = locationManager.GetLastKnownLocation(locationProvider);
            client.latitude = currentLocation.Latitude;
            client.longitude = currentLocation.Longitude;
            client.lastUpdated = DateTime.Now;
            client.accuracy = 100;
            string result = await ApiRequests.setLocation(client);
            Console.WriteLine("***********************************");
            Console.WriteLine("result = " + result);
        }

        public void OnProviderDisabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum]Availability status, Bundle extras)
        {
           // throw new NotImplementedException();
        }

        class usersCustomList : ListActivity 
        {
            protected override void OnListItemClick(ListView l, View v, int position, long id)
            {

            }
        }
    }
}

