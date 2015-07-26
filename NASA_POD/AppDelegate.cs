using Foundation;
using UIKit;
using System.Threading.Tasks;
using System.Net.Http;
using ModernHttpClient;
using System.IO;
using System;
using CoreGraphics;
using System.Drawing;

namespace NASA_POD
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window {
			get;
			set;
		}

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			// show an alert
			new UIAlertView(notification.AlertAction, "!" + notification.AlertBody, null, "OK", null).Show();

			// reset our badge
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

//		public override void HandleAction (UIApplication application, string actionIdentifier, UILocalNotification localNotification, Action completionHandler)
//		{
//			// show an alert
//			new UIAlertView(localNotification.AlertAction, "?" + localNotification.AlertBody, null, "OK", null).Show();
//
//			// reset our badge
//			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
//		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif

			//ask for notification priveleges
			var settings = UIUserNotificationSettings.GetSettingsForTypes (
				               UIUserNotificationType.Alert | UIUserNotificationType.Badge |
				               UIUserNotificationType.Sound, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings (settings);

			// check for a notification
			if (launchOptions != null)
			{
				// check for a local notification
				if (launchOptions.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
				{
					var localNotification = launchOptions[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
					if (localNotification != null)
					{
						new UIAlertView(localNotification.AlertAction, localNotification.AlertBody, null, "OK", null).Show();
						// reset our badge
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					}
				}
			}

//			// create notification for every day at 9 AM
//			var notification = new UILocalNotification();
//			notification.RepeatInterval = NSCalendarUnit.Day;
//
//			//set time
//			var time = new NSDateComponents();
//			time.Hour = 09;
//			time.Minute = 00;
//			time.Second = 0;
//
//			//choose calendar
//			var calendar = new NSCalendar (NSCalendarType.Gregorian);
//
//			//create fire date
//			var date = calendar.DateFromComponents (time);
//
//			// set the fire date (the date time in which it will fire)
//			notification.FireDate = date;
//			notification.TimeZone = NSTimeZone.DefaultTimeZone;
//
//			// configure the alert stuff
//			notification.AlertTitle = "The APOD is ready!";
//			notification.AlertAction = "Alert Action";
//			notification.AlertBody = "Alert Body";
//
//			notification.UserInfo = NSDictionary.FromObjectAndKey (new NSString("UserInfo for notification"), new NSString("Notification"));
//
//			// modify the badge - has no effect on the Watch
//			notification.ApplicationIconBadgeNumber = 1;
//
//			// set the sound to be the default sound
//			notification.SoundName = UILocalNotification.DefaultSoundName;
//
//			// schedule it
//			UIApplication.SharedApplication.ScheduleLocalNotification(notification);

			return true;
		}
	}
}


