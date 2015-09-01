package messengercsci641;


public class MsgApiService
	extends android.app.Service
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onStart:(Landroid/content/Intent;I)V:GetOnStart_Landroid_content_Intent_IHandler\n" +
			"n_onStartCommand:(Landroid/content/Intent;II)I:GetOnStartCommand_Landroid_content_Intent_IIHandler\n" +
			"n_onBind:(Landroid/content/Intent;)Landroid/os/IBinder;:GetOnBind_Landroid_content_Intent_Handler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"";
		mono.android.Runtime.register ("MessengerCSCI641.MsgApiService, MessengerCSCI641, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MsgApiService.class, __md_methods);
	}


	public MsgApiService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MsgApiService.class)
			mono.android.TypeManager.Activate ("MessengerCSCI641.MsgApiService, MessengerCSCI641, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onStart (android.content.Intent p0, int p1)
	{
		n_onStart (p0, p1);
	}

	private native void n_onStart (android.content.Intent p0, int p1);


	public int onStartCommand (android.content.Intent p0, int p1, int p2)
	{
		return n_onStartCommand (p0, p1, p2);
	}

	private native int n_onStartCommand (android.content.Intent p0, int p1, int p2);


	public android.os.IBinder onBind (android.content.Intent p0)
	{
		return n_onBind (p0);
	}

	private native android.os.IBinder n_onBind (android.content.Intent p0);


	public void onDestroy ()
	{
		n_onDestroy ();
	}

	private native void n_onDestroy ();

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
