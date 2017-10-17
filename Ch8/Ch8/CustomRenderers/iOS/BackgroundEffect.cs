using System;
using CustomRenderers.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("MyEffects")]
[assembly: ExportEffect(typeof(BackgroundEffect), "BackgroundEffect")]
namespace CustomRenderers.iOS
{
    public class BackgroundEffect: PlatformEffect
    {
        private UIColor originalColor;
		protected override void OnAttached()
		{
			try
			{
                originalColor = Control.BackgroundColor;
				Control.BackgroundColor = UIColor.FromRGB(204, 153, 255);
			}
			catch
			{
				// Cannot set property on attached control
			}
		}

		protected override void OnDetached()
		{
            try
            {
                Control.BackgroundColor = originalColor;
            }
            catch
            {
                
            }
		}
    }
}
