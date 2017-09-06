package md5204baa0763126b1faba40268d6fa76b3;


public class MainActivity_Receiver
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("SkimmerScanner.MainActivity+Receiver, App4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_Receiver.class, __md_methods);
	}


	public MainActivity_Receiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_Receiver.class)
			mono.android.TypeManager.Activate ("SkimmerScanner.MainActivity+Receiver, App4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MainActivity_Receiver (android.app.Activity p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_Receiver.class)
			mono.android.TypeManager.Activate ("SkimmerScanner.MainActivity+Receiver, App4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.App.Activity, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

	private java.util.ArrayList refList;
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
