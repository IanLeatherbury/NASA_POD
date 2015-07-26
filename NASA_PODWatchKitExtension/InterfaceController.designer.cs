// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace NASA_PODWatchKitExtension
{
	[Register ("InterfaceController")]
	partial class InterfaceController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceImage cached_apod { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceLabel explanation { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (cached_apod != null) {
				cached_apod.Dispose ();
				cached_apod = null;
			}
			if (explanation != null) {
				explanation.Dispose ();
				explanation = null;
			}
		}
	}
}
