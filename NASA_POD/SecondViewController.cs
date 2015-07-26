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
			// Perform any additional setup after loading the view, typically from a nib.

			notification_switch.ValueChanged += delegate {
				
				if (notification_switch.On == true) {
					// create notification for every day at 9 AM
					var notification = new UILocalNotification ();

					notification.RepeatInterval = NSCalendarUnit.Day;

					//set time
					var time = new NSDateComponents ();
					time.Hour = 09;
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
					notification.AlertAction = "Open the app";
					notification.AlertBody = "And check it out!";

					notification.UserInfo = NSDictionary.FromObjectAndKey (new NSString ("UserInfo for notification"), new NSString ("Notification"));

					// modify the badge - has no effect on the Watch
					notification.ApplicationIconBadgeNumber = 1;

					// set the sound to be the default sound
					notification.SoundName = UILocalNotification.DefaultSoundName;

					// schedule it
					UIApplication.SharedApplication.ScheduleLocalNotification (notification);
				}

				if (notification_switch.On == false) {
					UIApplication.SharedApplication.CancelAllLocalNotifications ();
				}
			};
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
