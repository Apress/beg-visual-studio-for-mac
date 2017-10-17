using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CustomRenderers;
using CustomRenderers.Droid;

[assembly: ExportRenderer(typeof(SelectableEntry), typeof(SelectableEntryRenderer))]
namespace CustomRenderers.Droid
{
    public class SelectableEntryRenderer: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			if (e.OldElement == null)
			{
				var nativeEditText = (global::Android.Widget.EditText)Control;
				nativeEditText.SetSelectAllOnFocus(true);
			}
		}
    }
}
