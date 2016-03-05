
using System;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FormsWithMJPG;
using FormsWithMJPG.iOS;

using UIKit;
using Foundation;

[assembly: ExportRenderer( typeof(CustomMJPGImage), typeof(CustomMJPGImageRenderer) )]

namespace FormsWithMJPG.iOS
{
	public class CustomMJPGImageRenderer : ImageRenderer
	{
		NSUrl url = null;
		string sName = ""; 
		string sPass = ""; 

		IosProcessMJPG pmjpg;

		protected override void OnElementChanged( ElementChangedEventArgs<Image> e )
		{
			base.OnElementChanged (e);

			if (Control == null) {
				// Instantiate the native control and assign it to the Control property
			} 

			if (e.OldElement != null) {
				// Unsubscribe from event handlers and cleanup any resources
				if (pmjpg != null)
					Console.WriteLine ("need to cleanup");
			}

			if (e.NewElement != null) {
				// Configure the control and subscribe to event handlers
				if ((e.NewElement as CustomMJPGImage).SourceUri != null)
					url = (e.NewElement as CustomMJPGImage).SourceUri;
				else
					url = default(NSUrl);
				sName = (e.NewElement as CustomMJPGImage).Username;
				sPass = (e.NewElement as CustomMJPGImage).Password;

				pmjpg = new IosProcessMJPG (url, sName, sPass, Control);
				Control.BackgroundColor = UIColor.Gray;
			}
		}
	}
}
