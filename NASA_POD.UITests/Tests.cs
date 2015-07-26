using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;
using System.Threading.Tasks;

namespace NASA_POD.UITests
{
	[TestFixture]
	public class Tests
	{
		iOSApp app;

		[SetUp]
		public void BeforeEachTest ()
		{
			app = ConfigureApp.iOS.StartApp ();
		}

		[Test]
		public void RotateApp ()
		{
			Task.Delay (7000);
			app.Screenshot ("And the app is opened");

			app.SetOrientationLandscape ();
			app.Screenshot ("Then I rotate the phone");
		}

	}
}


