package messengercsci641;


public class MapActivity_usersCustomList
	extends android.app.ListActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onListItemClick:(Landroid/widget/ListView;Landroid/view/View;IJ)V:GetOnListItemClick_Landroid_widget_ListView_Landroid_view_View_IJHandler\n" +
			"";
		mono.android.Runtime.register ("MessengerCSCI641.MapActivity/usersCustomList, MessengerCSCI641, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MapActivity_usersCustomList.class, __md_methods);
	}


	public MapActivity_usersCustomList () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MapActivity_usersCustomList.class)
			mono.android.TypeManager.Activate ("MessengerCSCI641.MapActivity/usersCustomList, MessengerCSCI641, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onListItemClick (android.widget.ListView p0, android.view.View p1, int p2, long p3)
	{
		n_onListItemClick (p0, p1, p2, p3);
	}

	private native void n_onListItemClick (android.widget.ListView p0, android.view.View p1, int p2, long p3);

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
