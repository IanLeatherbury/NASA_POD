using Foundation;
using UIKit;
using System.Threading.Tasks;
using System.Net.Http;
using ModernHttpClient;
using System.IO;
using System;
using CoreGraphics;
using System.Drawing;
using Xamarin;
using System.Collections.Generic;

namespace NASA_POD
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window {
			get;
			set;
		}

		public override void ReceivedLocalNotification (UIApplication application, UILocalNotification notification)
		{
			// show an alert
			new UIAlertView (notification.AlertAction, "!" + notification.AlertBody, null, "OK", null).Show ();

			// reset our badge
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

		public override void HandleAction (UIApplication application, string actionIdentifier, UILocalNotification localNotification, Action completionHandler)
		{
			// show an alert
			new UIAlertView (localNotification.AlertAction, "?" + localNotification.AlertBody, null, "OK", null).Show ();

			// reset our badge
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif

//			Insights.Initialize("1927149b6a7581f424fb6fc8ded1cfc54c9e6c10"); //old insights
			Insights.Initialize ("13edaee3613130296848d5406ed1b8028b1136b7"); // insight DF session

			#region InsightDFSession
			//Identify
			Insights.Identify ("a.unique.id@for.this.user", Insights.Traits.Name, "Gordon Strachen");
			var traits = new Dictionary<string, string> {
				{ Insights.Traits.Email, "gordon.strachen@celtic.com" },
				{ Insights.Traits.Name, "Gordon Strachen" }
			};
			Insights.Identify ("YourUsersUniqueId", traits);

			//track
			Insights.Track ("MusicTrackPlayed", new Dictionary<string, string> {
				{ "SongName", "Shake It Off" },
				{ "Length", "219" }
			});

			//error
			try {
				throw new NotSupportedException (string.Format ("Oh no. Really bad stuff happened.", 1));
			} catch (Exception exp) {
				Xamarin.Insights.Report (exp, new Dictionary <string, string> { 
					{ "error-local-time", DateTime.Now.ToString () }
				}, Xamarin.Insights.Severity.Error);
			}

			//warning
			try {
				// create a collection container to hold exceptions
				List<Exception> exceptions = new List<Exception> ();

				// do some stuff here ........

				// we have an exception with an innerexception, so add it to the list
				exceptions.Add (new TimeoutException ("This is part 1 of aggregate exception", new ArgumentException ("ID missing")));

				// do more stuff .....

				// Another exception, add to list
				exceptions.Add (new NotImplementedException ("This is part 2 of aggregate exception"));

				// all done, now create the AggregateException and throw it
				AggregateException aggEx = new AggregateException (exceptions);
				//throw aggEx;
				throw aggEx;
			} catch (Exception exp) {
				Xamarin.Insights.Report (exp, new Dictionary <string, string> { 
					{ "warning-local-time", DateTime.Now.ToString () }
				}, Xamarin.Insights.Severity.Warning);
			}

//			throw new Exception("Unhandled Exception that crashed your app.");

			#endregion InsightsDFSession


			//ask for notification priveleges
			var settings = UIUserNotificationSettings.GetSettingsForTypes (
				               UIUserNotificationType.Alert | UIUserNotificationType.Badge |
				               UIUserNotificationType.Sound, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings (settings);

			// check for a notification
			if (launchOptions != null) {
				// check for a local notification
				if (launchOptions.ContainsKey (UIApplication.LaunchOptionsLocalNotificationKey)) {
					var localNotification = launchOptions [UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
					if (localNotification != null) {
						new UIAlertView (localNotification.AlertAction, localNotification.AlertBody, null, "OK", null).Show ();
						// reset our badge
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					}
				}
			}

			return true;
		}
	}
}


