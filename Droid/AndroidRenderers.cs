
using System;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FormsWithMJPG;
using FormsWithMJPG.Droid;

[assembly: ExportRenderer( typeof(CustomMJPGImage), typeof(CustomMJPGImageRenderer) )]

namespace FormsWithMJPG.Droid
{
	// not implemented
	public class CustomMJPGImageRenderer : ImageRenderer
	{
		protected override void OnElementChanged( ElementChangedEventArgs<Image> e )
		{
			base.OnElementChanged (e);
		}
	}
}
