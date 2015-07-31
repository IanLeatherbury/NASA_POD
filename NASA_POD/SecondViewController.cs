using System;

using UIKit;
using Foundation;

namespace NASA_POD
{
	public partial class SecondViewController : UIViewController
	{
		public SecondViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();	
				
			if (notification_switch.On == true) {
				TurnOnNotifications ();
			}

			if (notification_switch.On == false) {
				TurnOffNotifications ();
			}

			notification_switch.ValueChanged += delegate {
				if (notification_switch.On == true) {
					TurnOnNotifications ();
				}

				if (notification_switch.On == false) {
					TurnOffNotifications ();
				}
			};

			//slider
			slider.SetThumbImage (UIImage.FromFile ("rocket_40.png"), UIControlState.Normal);
			sliderLabel.Text = slider.Value.ToString () + " AM";
			slider.ValueChanged += HandleValueChanged;
				
		}

		void HandleValueChanged (object sender, EventArgs e)
		{   // display the value in a label
			var castSliderToInt = (int)slider.Value;

			sliderLabel.Text = castSliderToInt.ToString () + " AM";
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		void TurnOnNotifications ()
		{
			// create notification for every day at specified time
			var notification = new UILocalNotification ();

			notification.RepeatInterval = NSCalendarUnit.Day;

			//set time
			var time = new NSDateComponents ();
			time.Hour = (nint)slider.Value;
			time.Minute = 00;
			time.Second = 0;

			//choose calendar
			var calendar = new NSCalendar (NSCalendarType.Gregorian);

			//create fire date
			var date = calendar.DateFromComponents (time);

			// set the fire date (the date time in which it will fire)
			notification.FireDate = date;
			notification.TimeZone = NSTimeZone.DefaultTimeZone;

			// configure the alert stuff
			notification.AlertTitle = "The APOD is ready!";
//			notification.AlertAction = "Open the app";
			notification.AlertBody = "Open the app and check it out!";

			notification.UserInfo = NSDictionary.FromObjectAndKey (new NSString ("UserInfo for notification"), new NSString ("Notification"));

			// modify the badge - has no effect on the Watch
			notification.ApplicationIconBadgeNumber = 1;

			// set the sound to be the default sound
			notification.SoundName = UILocalNotification.DefaultSoundName;

			// schedule it
			UIApplication.SharedApplication.ScheduleLocalNotification (notification);
		}

		void TurnOffNotifications ()
		{
			UIApplication.SharedApplication.CancelAllLocalNotifications ();
		}


	}
}
