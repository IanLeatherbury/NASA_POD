using System;

using WatchKit;
using Foundation;
using System.Threading.Tasks;
using System.Net.Http;
using ModernHttpClient;
using System.IO;
using NASA_POD;
using UIKit;
using System.Drawing;

namespace NASA_PODWatchKitExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		public const string WisdomUrl = "https://api.nasa.gov/planetary/apod?concept_tags=True&api_key=DEMO_KEY";

		public InterfaceController (IntPtr handle) : base (handle)
		{
		}

		public override async void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			await InitiateHttp ();
		}

		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}

		public async Task InitiateHttp ()
		{
			var client = new System.Net.Http.HttpClient (new NativeMessageHandler ());
			RenderStream (await client.GetStreamAsync (WisdomUrl));
		}

		public void RenderStream (Stream stream)
		{
			var reader = new System.IO.StreamReader (stream);

			var json = reader.ReadToEnd ();

			var apodJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ApodClass> (json);

			var url = apodJson.url;

			var device = WKInterfaceDevice.CurrentDevice;

//			img_title.SetText (apodJson.title);
			explanation.SetText (apodJson.explanation);

			using (var image = FromUrl (url)) {					
				if (!device.AddCachedImage (MaxResizeImage(image, 25,25), "APOD")) {
					Console.WriteLine ("Image cache full.");
				} else {
					cached_apod.SetImage (image);
				}
			}
		}

		static UIImage FromUrl (string uri)
		{
			using (var url = new NSUrl (uri))
			using (var data = NSData.FromUrl (url))
				return UIImage.LoadFromData (data);
		}

		public UIImage MaxResizeImage (UIImage sourceImage, float maxWidth, float maxHeight)
		{
			var sourceSize = sourceImage.Size;
			var maxResizeFactor = Math.Max (maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
			if (maxResizeFactor > 1)
				return sourceImage;
			var width = maxResizeFactor * sourceSize.Width;
			var height = maxResizeFactor * sourceSize.Height;
			UIGraphics.BeginImageContext (new SizeF ((float)width, (float)height));
			sourceImage.Draw (new RectangleF (0, 0, (float)width, (float)height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return resultImage;
		}
	}
}

