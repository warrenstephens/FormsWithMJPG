
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FormsWithMJPG;

using MediaPlayer;
using UIKit;
using AVKit;
using AVFoundation;
using Foundation;
using CoreMedia;
using CoreImage;
using CoreVideo;
using CoreGraphics;

namespace FormsWithMJPG.iOS
{
	public class IosProcessMJPG: NSUrlConnectionDataDelegate
	{
		NSUrl nsUrl;
		string sUsername;
		string sPassword;
		public NSUrlConnection connection;
		byte[] result = null;
		long result_expected_length;
		NSData nsdResult = new NSData();
		UIImageView uiImage;

		public IosProcessMJPG (NSUrl url, string username, string password, UIImageView uiImageDestination)
		{
			nsUrl = NSUrl.FromString ("");
			if (url != null)
				nsUrl = url;
			sUsername = "";
			if (username != null)
				sUsername = username;	
			sPassword = "";
			if( password != null )
				sPassword = password;
			if ( uiImageDestination == null)
				return;

			NSUrlRequest request = new NSUrlRequest (nsUrl,NSUrlRequestCachePolicy.ReloadIgnoringCacheData, 20);
			connection = NSUrlConnection.FromRequest(request, this);
			if (connection == null)
				Console.WriteLine ("connection is null");
			uiImage = uiImageDestination;
		}

		public override void ReceivedResponse(NSUrlConnection connection, NSUrlResponse response)
		{
			//Console.WriteLine ("ReceivedResponse");
			//Console.WriteLine ("response Description '{0}'", response.Description);
			//Console.WriteLine ("response MimeType '{0}'", response.MimeType);
			//Console.WriteLine ("response ExpectedContentLength '{0}'", response.ExpectedContentLength);

			result = new byte[0];
			result_expected_length = response.ExpectedContentLength;
		}

		public override void ReceivedData( NSUrlConnection connection, NSData data)
		{
			// get the length of the previously received data
			int iPrev = 0;
			if( result != null )
				iPrev = result.Length;

			// the next result is the previous result (if any) with the new data appended 
			byte [] Next = new byte [(int)iPrev + (int)data.Length];
			if( result != null )
				result.CopyTo (Next, 0);
			Marshal.Copy (data.Bytes, Next, iPrev, (int) data.Length);
			result = Next;

			if( result.Length >= result_expected_length )
			{
				// try to create an image from the data
				try
				{
				nsdResult = NSData.FromArray(result);
				UIImage im = new UIImage(nsdResult);
				if (im != null) {
					// image created, so set the destination
					uiImage.Image = im;
					}
				}
				catch( Exception ex ) {
					// ignore (unable to create image)
					Console.WriteLine("Exception '{0}'",ex.Message);
				}
			}
		}

		public override void FinishedLoading (NSUrlConnection connection)
		{
			Console.WriteLine ("FinishedLoading");
		}

		public override void FailedWithError (NSUrlConnection connection, NSError error)
		{
			Console.WriteLine ("FailedWithError '{0}'", error.Description);
		}

		public override Boolean CanAuthenticateAgainstProtectionSpace (NSUrlConnection connection, 
			NSUrlProtectionSpace protectionSpace)
		{
			//Console.WriteLine ("CanAuthenticateAgainstProtectionSpace '{0}'", protectionSpace.AuthenticationMethod);

			if (protectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodHTTPBasic) {
				return true;
			} else if (protectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodHTTPDigest)
				return true;
			else if (protectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodNTLM) {
				Console.WriteLine ("AuthenticationMethod NTLM");
				return false;
			} else if (protectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodServerTrust) {
				Console.WriteLine ("AuthenticationMethod ServerTrust");
				return false;
			}
			return false;
		}

		public override void ReceivedAuthenticationChallenge (NSUrlConnection connection, 
			NSUrlAuthenticationChallenge challenge)
		{
			//Console.WriteLine ("ReceivedAuthenticationChallenge '{0}'", challenge.Description);

			NSUrlCredential credential = null;
			if (challenge.ProtectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodHTTPBasic) {
				if( challenge.PreviousFailureCount > 0 )
					Console.WriteLine("Basic authentication failed");
				else 
					credential = new NSUrlCredential (sUsername, sPassword, NSUrlCredentialPersistence.ForSession);

			} else if (challenge.ProtectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodHTTPDigest) {
				if( challenge.PreviousFailureCount > 0 )
					Console.WriteLine("Digest authentication failed");
				else 
					credential = new NSUrlCredential (sUsername, sPassword, NSUrlCredentialPersistence.ForSession);
			}
			else if (challenge.ProtectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodServerTrust) {
				Console.WriteLine ("AuthenticationMethodServerTrust (TBD)");
			} 
			else
			{
				Console.WriteLine ("Some other authentication method");
			}

			connection.UseCredential (credential, challenge);
		}

		public override Boolean ConnectionShouldUseCredentialStorage (NSUrlConnection connection)
		{
			//Console.WriteLine ("ConnectionShouldUseCredentialStorage '{0}'", connection.Description);
			return false;
		}

	}
}
