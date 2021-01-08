package crc64a85e42b114a93146;


public class CustomImageRenderer
	extends crc643f46942d9dd1fff9.ImageRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ChatApp_Ondoy.Droid.CustomImageRenderer, ChatApp-Ondoy.Android", CustomImageRenderer.class, __md_methods);
	}


	public CustomImageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomImageRenderer.class)
			mono.android.TypeManager.Activate ("ChatApp_Ondoy.Droid.CustomImageRenderer, ChatApp-Ondoy.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CustomImageRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomImageRenderer.class)
			mono.android.TypeManager.Activate ("ChatApp_Ondoy.Droid.CustomImageRenderer, ChatApp-Ondoy.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CustomImageRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomImageRenderer.class)
			mono.android.TypeManager.Activate ("ChatApp_Ondoy.Droid.CustomImageRenderer, ChatApp-Ondoy.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
