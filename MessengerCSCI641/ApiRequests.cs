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
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MessengerCSCI641
{
    /// <summary>
    /// A class with static methods to send requests to the api
    /// </summary>
    class ApiRequests
    {
        public static async Task<string> createAccount(User newUser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.cs.rit.edu/~jsb/2145/ProgSkills/Labs/Messenger/api.php");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(
                   "?command=createAccount&email=" + newUser.email +
                   "&password=" + newUser.password +
                   "&first_name=" + newUser.first_name +
                   "&last_name=" + newUser.last_name);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return "error";
                }
            }
        }
        public static async Task<string> deleteAccount(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.cs.rit.edu/~jsb/2145/ProgSkills/Labs/Messenger/api.php");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(
                   "?command=deleteAccount&email=" + user.email +
                   "&password=" + user.password);
                if (response.IsSuccessStatusCode)
                {
                    string result = "account deleted";
                    return result;
                }
                else
                {
                    return "error";
                }
            }
        }
        public static async Task<string> getLocations(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.cs.rit.edu/~jsb/2145/ProgSkills/Labs/Messenger/api.php");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(
                   "?command=getLocations&email=" + user.email +
                   "&password=" + user.password);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return "error";
                }
            }
        }
        public static async Task<string> getMessages(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.cs.rit.edu/~jsb/2145/ProgSkills/Labs/Messenger/api.php");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(
                   "?command=getMessages&email=" + user.email +
                   "&password=" + user.password);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return "error";
                }
            }
        }
        public static async Task<string> getUsers(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.cs.rit.edu/~jsb/2145/ProgSkills/Labs/Messenger/api.php");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(
                   "?command=getUsers&email=" + user.email +
                   "&password=" + user.password);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return "error";
                }
            }
        }
        public static async Task<string> sendMessage(User user, string to, string message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.cs.rit.edu/~jsb/2145/ProgSkills/Labs/Messenger/api.php");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(
                   "?command=sendMessage&email=" + user.email +
                   "&password=" + user.password +
                   "&to="+to +
                   "&message="+message);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return "error";
                }
            }
        }
        public static async Task<string> setLocation(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.cs.rit.edu/~jsb/2145/ProgSkills/Labs/Messenger/api.php");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(
                   "?command=setLocation&email=" + user.email +
                   "&password=" + user.password +
                   "&lat="+user.latitude +
                   "&long="+user.longitude+
                   "&acc=" +user.accuracy);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return "error";
                }
            }
        }
        public static async Task<string> setPush(User user, string pushUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.cs.rit.edu/~jsb/2145/ProgSkills/Labs/Messenger/api.php");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(
                   "?command=setPush&email=" + user.email +
                   "&password=" + user.password+
                   "&pushUrl="+pushUrl);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return "error";
                }
            }
        }
    }
}