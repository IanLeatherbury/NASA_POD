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
	[Register ("GlanceController")]
	partial class GlanceController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceImage glance_image { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (glance_image != null) {
				glance_image.Dispose ();
				glance_image = null;
			}
		}
	}
}
