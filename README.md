# FormsWithMJPG

Xamarin Forms project:

1. An MJPG viewer (tested with Axis and KT&C cameras with username and password)
2. The iOS side is implemented, but not yet the Android side 
   (without an Android license then just ignore the errors and warnings, the iOS side will still work)
3. Handles Basic and Digest authentication (if necessary) with provided username and password
4. Built with Xamarin Forms 2.1.0.6524

Files:

1. FormsWithMJPGPage.cs is the main Xamarin Forms page
2. CustomRenderers.cs defines the CustomMJPGImage class based on the Image class
3. IosRenderers.cs contains the iOS side renderer of CustomMJPGImage
4. IosProcessMJPG.cs implements an NSUrlConnectionDataDelegate based class which creates the frame-by-frame images
5. AndroidRenderers.cs defines the (to be completed) Android side renderer of CustomMJPGImage
