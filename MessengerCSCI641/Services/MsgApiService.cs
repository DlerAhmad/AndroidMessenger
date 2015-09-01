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
using System.Threading;
using Newtonsoft.Json;
using System.Net.Http;


namespace MessengerCSCI641
{
    /// <summary>
    /// A back ground service to recive msgs from API
    /// </summary>
    [Service]
    class MsgApiService : Service
    {

       
        int notifCounter = 0;
        public Android.App.TaskStackBuilder stackBuilder;

        public override void OnStart(Intent intent, int startId)
        {
           
            base.OnStart(intent, startId);
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum]StartCommandFlags flags, int startId)
        {

            getMsgs();
            return base.OnStartCommand(intent, flags, startId);
        }


        //reads msgs from api every 3 sec
        private void getMsgs()
        {      
            var thread = new Thread(async () =>
             {
                 MyMessage[] msgs;

                 Notification.Builder builder = new Notification.Builder(this)
                 .SetAutoCancel(true)
                 .SetSmallIcon(Resource.Drawable.notif)
                 .SetDefaults(NotificationDefaults.Sound);


                 var nMgr = (NotificationManager)GetSystemService(NotificationService);

                 Notification notification = new Notification(Resource.Drawable.notif, "message notification");

                 Intent intent = new Intent(this, typeof(ChatActivity));
                 PendingIntent pendingIntent;
                 ViewModel myViewModel = ViewModel.myViewModelInstance;

                 while (true)
                 {
                    
                     string result = await ApiRequests.getMessages(MapActivity.client);
                     msgs = JsonConvert.DeserializeObject<MyMessage[]>(result);

                     if (notifCounter == 0)
                     {
                         for (int i = msgs.Length-1; i >=0; --i)
                         {
                             MyMessage msg = msgs[i];

                             if (!myViewModel.msgContainer.ContainsKey(msg.message_id))
                             {
                                 myViewModel.msgContainer.Add(msg.message_id, msg);
                                 myViewModel.msgsContainer.Add(msg);
                             }
                             
                         }
                         notifCounter++;
                     }
                     else
                     {
                         for (int i = msgs.Length-1; i >= 0; --i)
                         {
                             MyMessage msg = msgs[i];
                             if (!myViewModel.msgContainer.ContainsKey(msg.message_id))
                             {
                                  
                                 myViewModel.msgContainer.Add(msg.message_id, msg);
                                 myViewModel.msgsContainer.Add(msg);

                                 if (msg.msg_type.Equals("from"))
                                 {
                                     intent.PutExtra("email", msg.email);
                                     intent.PutExtra("firstName", msg.first_name);
                                     intent.PutExtra("lastName", msg.last_name);

                                     stackBuilder = Android.App.TaskStackBuilder.Create(this);
                                     stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MapActivity)));
                                     stackBuilder.AddNextIntent(intent);

                                     pendingIntent = PendingIntent.GetActivity(this, notifCounter++, intent, PendingIntentFlags.OneShot);
                                     builder.SetContentIntent(pendingIntent);
                                     builder.SetContentTitle(msg.first_name + " " + msg.last_name);
                                     builder.SetContentText(msg.message);
                                     
                                     

                                     nMgr.Notify(notifCounter++, builder.Build());

                                     BroadcastStarted(msg);
                                 }
                             }
                         }

                     }
                     Thread.Sleep(3000);
                 }

             });

            thread.Start();
        }

        //bcast the msg to all actvities
        private void BroadcastStarted( MyMessage msg)
        {
            Intent BroadcastIntent = new Intent(this, typeof(ChatActivity.MsgBroadcasrReceiver));
            BroadcastIntent.PutExtra("fromEmail", msg.email);
            BroadcastIntent.PutExtra("fromName", msg.first_name);
            BroadcastIntent.PutExtra("message", msg.message);
            BroadcastIntent.SetAction(ChatActivity.MsgBroadcasrReceiver.MSG_BCAST);
            BroadcastIntent.AddCategory(Intent.CategoryDefault);
            SendBroadcast(BroadcastIntent);
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}