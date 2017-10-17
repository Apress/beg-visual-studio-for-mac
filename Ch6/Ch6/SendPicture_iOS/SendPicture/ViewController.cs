using System;
using Foundation;
using MessageUI;
using UIKit;

namespace SendPicture
{
    public partial class ViewController : UIViewController
    {

        UIImagePickerController imagePicker;
        MFMailComposeViewController mailController;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Choose Photo";
			View.BackgroundColor = UIColor.White;

			SelectButton.TouchUpInside += SelectButton_TouchUpInside;
            SendButton.TouchUpInside+= SendButton_TouchUpInside; 
		}

        private void SendButton_TouchUpInside(object sender, EventArgs e)
        {
			if (MFMailComposeViewController.CanSendMail)
			{
                // an array of strings that represents recipients
				var to = new string[] { "someone@mail.com" };

                // MFMailComposeViewController calls the UI to send an email
				if (MFMailComposeViewController.CanSendMail)
				{
					mailController = new MFMailComposeViewController();
					mailController.SetToRecipients(to);
					mailController.SetSubject("A picture");
					mailController.SetMessageBody("Hi, I'm sending a picture from iOS", false);

                    mailController.AddAttachmentData(ImageView.Image.AsJPEG(), "image/jpeg", "image.jpg");

					mailController.Finished += (object s, MFComposeResultEventArgs args) =>
					{
						BeginInvokeOnMainThread(() =>
						{
							args.Controller.DismissViewController(true, null);
						});
					};
				}

				this.PresentViewController(mailController, true, null);
			}
        }

        private void SelectButton_TouchUpInside(object sender, EventArgs e)
        {
			// create a new picker controller
			imagePicker = new UIImagePickerController();

			// set our source to the photo library
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

			// set what media types
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;

			// show the picker
			PresentModalViewController(imagePicker, true);
			//UIPopoverController picc = new UIPopoverController(imagePicker);
		}

        void Handle_Canceled(object sender, EventArgs e)
		{
			imagePicker.DismissModalViewController(true);
		}

		// This is a sample method that handles the FinishedPickingMediaEvent
		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			bool isImage = false;
			switch (e.Info[UIImagePickerController.MediaType].ToString())
			{
				case "public.image":
					isImage = true;
					break;

				case "public.video":
					break;
			}

			// if it was an image, get the other image info
			if (isImage)
			{

				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null)
				{
					// do something with the image
					ImageView.Image = originalImage;
				}

				// get the edited image
				UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
				if (editedImage != null)
				{
					// do something with the image
					ImageView.Image = editedImage;
                }
			}
			// if it's a video
			else
			{
                // simply return, we don't support videos
                return;
			}

			// dismiss the picker
			imagePicker.DismissModalViewController(true);
		}
    }
}
