using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace MessengerCSCI641
{
    /// <summary>
    /// chat activity to manage a chat session
    /// </summary>
    [Activity(Label = "ChatActivity")]
    public class ChatActivity : Activity
    {

        public static ChatActivity instance;
        static string toEmail;
        private string toFirstName;
        private string toLastName;
        private string msg;
        private List<string> messages = new List<string>();
        ListView msgList;
        Button sendButton;
        static ArrayAdapter msgAdapter;
        EditText msgToSend;


        /// <summary>
        /// receives the bcast from service thread
        /// </summary>
        [BroadcastReceiver(Enabled = true)]
        public class MsgBroadcasrReceiver : BroadcastReceiver
        {
            public static readonly string MSG_BCAST = "MSG_BCAST";
            public override void OnReceive(Context context, Intent intent)
            {
                if (intent.Action == MSG_BCAST)
                {
                    string fromEmail = intent.GetStringExtra("fromEmail");
                    string fromName = intent.GetStringExtra("fromName");
                    if (fromEmail.Equals(toEmail))
                    {
                        string msgContent = intent.GetStringExtra("message");
                        msgAdapter.Add(fromName+ ":  "+msgContent);
                        
                    }

                }
            }
        }

        private MsgBroadcasrReceiver _receiver;


        /// <summary>
        /// register to service
        /// </summary>
        private void RegisterBroadcastReceiver()
        {
            IntentFilter filter = new IntentFilter(MsgBroadcasrReceiver.MSG_BCAST);
            filter.AddCategory(Intent.CategoryDefault);
            _receiver = new MsgBroadcasrReceiver();
            RegisterReceiver(_receiver, filter);
            
        }

        private void UnRegisterBroadcastReceiver()
        {
            UnregisterReceiver(_receiver);
        }



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.chatActivity);

            //get intent data
            toEmail = Intent.GetStringExtra("email");
            toFirstName = Intent.GetStringExtra("firstName");
            toLastName = Intent.GetStringExtra("lastName");

            this.Title = toFirstName+" " + toLastName;

            msgList = FindViewById<ListView>(Resource.Id.msg_list);
            sendButton = FindViewById<Button>(Resource.Id.send_btn);
            msgToSend = FindViewById<EditText>(Resource.Id.msg_edit);
            msgToSend.SetTextColor(Android.Graphics.Color.Black);
            msgToSend.SetTextSize(Android.Util.ComplexUnitType.Px,30);
            //messages.Add(msg + "email= " + toEmail);

            sendButton.Click += sendClickEvent;

            msgAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, messages);
            msgList.Adapter = msgAdapter;

            ChatActivity.instance = this;

            RegisterBroadcastReceiver();
            updateMsgBox();
        }
        /// <summary>
        /// updates the chat contents
        /// </summary>
        private void  updateMsgBox()
        {
            ViewModel myViewModel = ViewModel.myViewModelInstance;
            //Queue<string> list = new Queue<string>();
            
            
            for(int i = 0; i< myViewModel.msgsContainer.Count; ++i)
            {
                MyMessage entry = myViewModel.msgsContainer.ElementAt(i);
                if (entry.msg_type.Equals("from") && entry.email.Equals(toEmail))
                {
                   // list.Enqueue(entry.message);
                    msgAdapter.Add(entry.first_name+":  " +entry.message);
                }
                else if (entry.msg_type.Equals("to") && entry.email.Equals(toEmail))
                {
                    //list.Enqueue(entry.message);
                    msgAdapter.Add("YOU:  "+ entry.message);
                }
            }
       

        }
        protected override void OnResume()
        {
            RegisterBroadcastReceiver();
            base.OnResume();
        }

        protected override void OnPause()
        {
            base.OnDestroy();
        }
        protected override void OnDestroy()
        {
           // UnRegisterBroadcastReceiver();
            base.OnDestroy();
        }

        /// <summary>
        /// click event for send button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        public async void sendClickEvent(Object sender, EventArgs ea)
        {
            string msg = msgToSend.Text;
            ViewModel myViewModel = ViewModel.myViewModelInstance;
            msgToSend.Text = "";
            msgAdapter.Add("YOU:  "+msg);
            string result = await ApiRequests.sendMessage(MapActivity.client, toEmail, msg);
            MyMessage[] sentMsg = JsonConvert.DeserializeObject<MyMessage[]>(result);
            myViewModel.msgContainer.Add(sentMsg[0].message_id,sentMsg[0]);
            myViewModel.msgsContainer.Add(sentMsg[0]);
        }
    }
}