
using System;
using System.IO;
using System.Diagnostics;

using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Net; 
using System.Text;

namespace FormsWithMJPG
{
	public class FormsWithMJPGPage : ContentPage
	{
		Entry entryUrl;
		Entry entryUsername;
		Entry entryPassword;
		Button buttonShow;

		Uri uri;
		string username;
		string password;

		CustomMJPGImage cimage;
		CustomMJPGImage cimageShow;
		StackLayout stackLayout;
		ScrollView scrollView;

		public FormsWithMJPGPage()
		{
			entryUrl = new Entry {
				Placeholder = "Url",
				VerticalOptions = LayoutOptions.Center,
				Keyboard = Keyboard.Url,
			};

			entryUsername = new Entry {
				Placeholder = "Username",
				VerticalOptions = LayoutOptions.Center,
				Keyboard = Keyboard.Create(0) // KeyboardFlags.Default does not exist
			};

			entryPassword = new Entry {
				Placeholder = "Password",
				VerticalOptions = LayoutOptions.Center,
				Keyboard = Keyboard.Text,
				IsPassword = true
			};


			buttonShow = new Button
			{
				Text = "Show",
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Fill
			};

			buttonShow.Clicked += (sender, args) =>
			{
				if( entryUrl.Text == null )
					return;
				uri = new Uri(entryUrl.Text);
				if( entryUsername == null )
					return;
				username = entryUsername.Text;
				if( entryPassword == null )
					return;
				password = entryPassword.Text;

				//Debug.WriteLine("uri '{0}'",uri);
				//Debug.WriteLine("username '{0}'", username);
				//Debug.WriteLine("password '{0}'", password);

				cimageShow = new CustomMJPGImage {
					Source = ImageSource.FromResource("Black.jpg"), 
					SourceUri = uri,
					Username = username,
					Password = password,
					WidthRequest = 400,
					HeightRequest = 200,
					HorizontalOptions = LayoutOptions.End,
					VerticalOptions = LayoutOptions.Fill
				};
				stackLayout.Children[4] = cimageShow;
			}; 

			// an initial site can be supplied here
			//Uri uriInit = new Uri ("http://webcam.st-malo.com/axis-cgi/mjpg/video.cgi?resolution=640x480");
			//string uname = "username1";
			//string pword = "password1";

			// disable the following 3 lines if the above 3 lines are used
			Uri uriInit = default(Uri);
			string uname = "";
			string pword = "";

			cimage = new CustomMJPGImage {
				Source = ImageSource.FromResource("Black.jpg"), 
				SourceUri = uriInit,
				Username = uname,
				Password = pword,
				WidthRequest = 400,
				HeightRequest = 200,
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Fill
			};

			stackLayout = new StackLayout
			{
				Children = 
				{
					entryUrl,
					entryUsername,
					entryPassword,
					buttonShow,
					cimage,
				},
				HeightRequest = 1500
			}; 

			scrollView = new ScrollView
			{
				//BackgroundColor = Color.White,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Content = stackLayout 
			}; 

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

			this.Content = scrollView;  

		}
	}
} 
