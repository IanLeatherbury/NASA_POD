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

namespace NASA_POD
{
	[Register ("SecondViewController")]
	partial class SecondViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel notification_label { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch notification_switch { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (notification_label != null) {
				notification_label.Dispose ();
				notification_label = null;
			}
			if (notification_switch != null) {
				notification_switch.Dispose ();
				notification_switch = null;
			}
		}
	}
}
