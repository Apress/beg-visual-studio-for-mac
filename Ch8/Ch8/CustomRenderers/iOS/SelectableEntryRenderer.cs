using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CustomRenderers;
using CustomRenderers.iOS;

[assembly: ExportRenderer(typeof(SelectableEntry), typeof(SelectableEntryRenderer))]
namespace CustomRenderers.iOS
{
    public class SelectableEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			var nativeTextField = Control;
			nativeTextField.EditingDidBegin += (object sender, EventArgs eIos) =>
			{
				nativeTextField.PerformSelector(new ObjCRuntime.Selector("selectAll"),
				null, 0.0f);
			};
		}
	}
}
