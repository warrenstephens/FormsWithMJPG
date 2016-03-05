
using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FormsWithMJPG
{
	public class CustomMJPGImage : Image
	{
		public CustomMJPGImage() { }

		// allow for a Uri
		/*
		// alternate syntax
		public static readonly BindableProperty SourceUriProperty =
			BindableProperty.Create<CustomMJPGImage,Uri>
		(im => im.SourceUri, default(Uri));
		*/
		public static readonly BindableProperty SourceUriProperty = 
			BindableProperty.Create("SourceUri",
				typeof(Uri),
				typeof(CustomMJPGImage),
				default(Uri) ); // default Uri

		public Uri SourceUri
		{
			set { SetValue (SourceUriProperty, value); }
			get { return (Uri)GetValue (SourceUriProperty); }
		}

		// allow for a Username
		public static readonly BindableProperty UsernameProperty =
			BindableProperty.Create("Username",
				typeof(string),
				typeof(CustomMJPGImage),
				"" ); // default Username
		public string Username
		{
			set { SetValue (UsernameProperty, value); }
			get { return (string)GetValue (UsernameProperty); }
		}

		// allow for a Password
		public static readonly BindableProperty PasswordProperty =
			BindableProperty.Create("Password",
				typeof(string),
				typeof(CustomMJPGImage),
				"" ); // default Password
		public string Password
		{
			set { SetValue (PasswordProperty, value); }
			get { return (string)GetValue (PasswordProperty); }
		}

	}

}
