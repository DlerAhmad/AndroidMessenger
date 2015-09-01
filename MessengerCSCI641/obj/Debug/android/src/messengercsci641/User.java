package messengercsci641;


public class User
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_toString:()Ljava/lang/String;:GetToStringHandler\n" +
			"";
		mono.android.Runtime.register ("MessengerCSCI641.User, MessengerCSCI641, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", User.class, __md_methods);
	}


	public User () throws java.lang.Throwable
	{
		super ();
		if (getClass () == User.class)
			mono.android.TypeManager.Activate ("MessengerCSCI641.User, MessengerCSCI641, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.String toString ()
	{
		return n_toString ();
	}

	private native java.lang.String n_toString ();

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
