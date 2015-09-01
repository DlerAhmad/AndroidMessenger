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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace MessengerCSCI641
{
    /// <summary>
    /// initial login activity
    /// </summary>
    [Activity(Label = "Login", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : Activity
    {
        private User client;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.login);
            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);

            //retrieve info from last session
            retrieveInfo();


            try
            {
                loginButton.Click += delegate { login(); };
            }catch (Exception e)
            {
                Console.WriteLine("**********************");
                Console.WriteLine(e.Data);
                Console.WriteLine("**********************");
            }
            

            // Create your application here
        }

        //validates the user info
        private async void login()
        {
            var fname = FindViewById<EditText>(Resource.Id.editText1);
            var lname = FindViewById<EditText>(Resource.Id.editText2);
            var email = FindViewById<EditText>(Resource.Id.editText3);
            var pass = FindViewById<EditText>(Resource.Id.editText4);
            var errMsg = FindViewById<TextView>(Resource.Id.textView1);
            if (fname.Text!="" && lname.Text!="" && email.Text!="" && pass.Text!="")
            {
                errMsg.SetTextColor(Android.Graphics.Color.Green);
                client = new User();
                client.first_name = fname.Text;
                client.last_name = lname.Text;
                client.email = email.Text;
                client.password = pass.Text;
                client.latitude = 43.1656;
                client.longitude = -77.6114;
                MapActivity.client = client;

                //setting the global static client object
                errMsg.Text = await ApiRequests.createAccount(client) + ". logging in...";

                //startin map activity
                StartActivity(typeof(MapActivity));
            }
            else
            {
                errMsg.SetTextColor(Android.Graphics.Color.Red);
                errMsg.Text = "Please fill all the fields.";
            }
        }


        protected void saveInfo()
        {
            //store
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("fname", client.first_name);
            prefEditor.PutString("lname", client.last_name);
            prefEditor.PutString("email", client.email);
            prefEditor.PutString("pass", client.password);
            prefEditor.Commit();
        }

        protected void retrieveInfo()
        {
            //retreive 
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);

            var fname = FindViewById<EditText>(Resource.Id.editText1);
            var lname = FindViewById<EditText>(Resource.Id.editText2);
            var email = FindViewById<EditText>(Resource.Id.editText3);
            var pass = FindViewById<EditText>(Resource.Id.editText4);

            fname.Text= prefs.GetString("fname", null);
            lname.Text = prefs.GetString("lname", null);
            email.Text = prefs.GetString("email", null);
            pass.Text = prefs.GetString("pass", null);


            //Show a toast
            RunOnUiThread(() => Toast.MakeText(this, fname.Text, ToastLength.Long).Show());

        }

        protected override void OnStop()
        {
            saveInfo();
            base.OnStop();
        }
    }
}