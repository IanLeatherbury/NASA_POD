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

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView satImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISlider slider { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sliderLabel { get; set; }

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
			if (satImage != null) {
				satImage.Dispose ();
				satImage = null;
			}
			if (slider != null) {
				slider.Dispose ();
				slider = null;
			}
			if (sliderLabel != null) {
				sliderLabel.Dispose ();
				sliderLabel = null;
			}
		}
	}
}
