using System;
using System.Collections.Generic;
using System.Linq;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace SendPicture
{
    [Activity(Label = "Send Email WithAttachment", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private ImageView _imageView;
		Uri fileName;

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			if (requestCode == 0 && resultCode == Result.Ok)
			{
				// Make it available in the gallery

				Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
				Uri contentUri = Uri.FromFile(App._file);
				mediaScanIntent.SetData(contentUri);
				SendBroadcast(mediaScanIntent);

				// Display in ImageView. We will resize the bitmap to fit the display
				// Loading the full sized image will consume to much memory 
				// and cause the application to crash.

				int height = Resources.DisplayMetrics.HeightPixels;
				int width = _imageView.Height;
				App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
				if (App.bitmap != null)
				{
					_imageView.SetImageBitmap(App.bitmap);
					App.bitmap = null;
					// Dispose of the Java side bitmap.
					GC.Collect();
				}
			}
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			// Set the main layout
			SetContentView(Resource.Layout.Main);

			// Check if the app has permission to write on disk
			if (CheckSelfPermission(Manifest.Permission.WriteExternalStorage) == Permission.Granted)
			{
				// Check in an app for taking pictures is available
				if (IsThereAnAppToTakePictures())
				{
					// Create a folder to store pictures
					CreateDirectoryForPictures();

					// Get a reference to widgets
					Button captureButton = FindViewById<Button>(Resource.Id.openCameraButton);
					Button sendButton = FindViewById<Button>(Resource.Id.sendEmailButton);
					_imageView = FindViewById<ImageView>(Resource.Id.photoView);

					// Set event handler
					captureButton.Click += CaptureButton_Click;
					sendButton.Click += SendButton_Click;
				}
			}
		}

		private void SendButton_Click(object sender, EventArgs e)
		{
			if (this.fileName != null)
			{
				var email = new Intent(Intent.ActionSend);

				// Check if at least one activity exists for the specified intent
				if (PackageManager.QueryIntentActivities(email, PackageInfoFlags.MatchDefaultOnly).Any())
				{
					email.PutExtra(Intent.ExtraEmail, new[] { "someone@email.com" });
					email.PutExtra(Intent.ExtraSubject, "Sample email with attachment");
					email.PutExtra(Intent.ExtraStream, fileName);

					email.SetType("message/rfc822");

					StartActivity(Intent.CreateChooser(email, "Email"));
				}
			}
		}

		private void CreateDirectoryForPictures()
		{
			App._dir = new File(
				Environment.GetExternalStoragePublicDirectory(
					Environment.DirectoryPictures), "CameraAppDemo");
			if (!App._dir.Exists())
			{
				App._dir.Mkdirs();
			}
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities =
				PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void CaptureButton_Click(object sender, EventArgs eventArgs)
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);

			App._file = new File(App._dir, "SampleImg.jpg");
			this.fileName = Uri.FromFile(App._file);

			intent.PutExtra(MediaStore.ExtraOutput, this.fileName);

			StartActivityForResult(intent, 0);
		}
	}

	public static class App
	{
		public static File _file;
		public static File _dir;
		public static Bitmap bitmap;
	}
}

