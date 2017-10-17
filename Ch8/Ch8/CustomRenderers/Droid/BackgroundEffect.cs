using System;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CustomRenderers.Droid;

[assembly: ResolutionGroupName("MyEffects")]
[assembly: ExportEffect(typeof(BackgroundEffect), "BackgroundEffect")]
namespace CustomRenderers.Droid
{
	public class BackgroundEffect : PlatformEffect
	{
        private Android.Graphics.Color originalColor;

		protected override void OnAttached()
		{
			try
			{
                originalColor = (Control.Background as ColorDrawable).Color;
				Control.SetBackgroundColor(Android.Graphics.Color.LightGreen);
			}
			catch
			{
				// Cannot set property on attached control
			}
		}

		protected override void OnDetached()
		{
            Control.SetBackgroundColor(originalColor);
		}
	}
}
