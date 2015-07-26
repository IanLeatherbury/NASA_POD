using System;

using UIKit;
using System.Threading.Tasks;
using ModernHttpClient;
using System.IO;
using Foundation;
using System.Drawing;
using CoreGraphics;
using AVFoundation;
using MediaPlayer;
using MBProgressHUD;

namespace NASA_POD
{
	public partial class FirstViewController : UIViewController
	{
		UIImageView imageView;
		UIImage apod;
		string imgUrl, imgTitle, imgExplanation, mediaType;

		public UIImage Apod {
			get {
				return apod;
			}
			set {
				apod = value;
			}
		}

		public string ImgTitle {
			get {
				return imgTitle;
			}
			set {
				imgTitle = value;
			}
		}

		public string ImgExplanation {
			get {
				return imgExplanation;
			}
			set {
				imgExplanation = value;
			}
		}

		public string ImgUrl {
			get {
				return imgUrl;
			}
			set {
				imgUrl = value;
			}
		}

		public string MediaType {
			get {
				return mediaType;
			}
			set {
				mediaType = value;
			}
		}

		public const string WisdomUrl = "https://api.data.gov/nasa/planetary/apod?api_key=CBOOYPk0oJ0nNTTV5LVUFhLF3wC6xqQ992IbuDnR&format=JSON";
		//			"https://api.nasa.gov/planetary/apod?concept_tags=True&api_key=DEMO_KEY";

		public FirstViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//create waiting hud
			var hud = new MTMBProgressHUD (View) {
				LabelText = "Loading high res image...",
				RemoveFromSuperViewOnHide = true
			};
			View.AddSubview (hud);
			hud.Show (animated: true);

			//load app content
			InitiateHttp ();

			//hide hud
			hud.Hide (animated: true, delay: 5);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
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

			if (json != null) {

				var apodJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ApodClass> (json);

				Console.WriteLine (apodJson);

				//set properties from JSON
				ImgUrl = apodJson.url;
				Apod = FromUrl (ImgUrl);
				ImgTitle = apodJson.title;
				ImgExplanation = apodJson.explanation;
				MediaType = apodJson.media_type;

				//create UI on main thread
				InvokeOnMainThread (delegate {
					CreatePortraitUI ();

				});	
			} else
				return;
		}

		static UIImage FromUrl (string uri)
		{
			using (var url = new NSUrl (uri))
			using (var data = NSData.FromUrl (url))
				return UIImage.LoadFromData (data);
		}

		void CreatePortraitUI ()
		{
			//create main scroll view
			var scrollView = new UIScrollView (new RectangleF (0, 0, (float)View.Frame.Width, (float)(View.Frame.Height * (.66))));
			View.AddSubview (scrollView);
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

			//set image and check for video
			if (MediaType != "video") {
				imageView = new UIImageView (Apod);
				scrollView.ContentSize = imageView.Image.Size;
				scrollView.AddSubview (imageView);

				//set zoom properties
				scrollView.MaximumZoomScale = 3f;
				scrollView.MinimumZoomScale = .1f;			
				scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => {
					return imageView;
				};
			} else {
				var imageView = new UIImageView (new UIImage ("halloween.jpg"));
				scrollView.ContentSize = imageView.Image.Size;
				scrollView.AddSubview (imageView);

				//set zoom properties
				scrollView.MaximumZoomScale = 3f;
				scrollView.MinimumZoomScale = .1f;			
				scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => {
					return imageView;
				};
			}

			//scrollview for lower half
			var scrollView2 = new UIScrollView (new RectangleF (0, (float)(View.Frame.Height * (.66) + 7), (float)View.Frame.Width, (float)(View.Frame.Height * .66 - 27)));
			View.AddSubview (scrollView2);

			//set title of image
			var frame = new CGRect (0, 0, View.Frame.Width, 20);
			var imgTitle = new UILabel (frame);
			imgTitle.Text = ImgTitle;
			imgTitle.TextAlignment = UITextAlignment.Center;
			scrollView2.AddSubview (imgTitle);


			//check to see if the POD is a video
			if (MediaType == "video") {
				var imgExplanation = new UITextView ();
				imgExplanation.Frame = new CGRect (0, 20, View.Frame.Width, View.Frame.Height);
				imgExplanation.Text = "Today's astronomy pic of the day is a video! Head over to http://apod.nasa.gov/ to check it out! Video support is coming soon. In the meantime, enjoy some pumpkins!";
				imgExplanation.Editable = false;

				scrollView2.ContentSize = View.Frame.Size;
				scrollView2.AddSubview (imgExplanation);
			} else {
				var imgExplanation = new UITextView ();
				imgExplanation.Frame = new CGRect (0, 20, View.Frame.Width, View.Frame.Height);
				imgExplanation.Text = ImgExplanation;
				imgExplanation.Editable = false;

				scrollView2.ContentSize = View.Frame.Size;
				scrollView2.AddSubview (imgExplanation);
			}
		}

		/// <summary>
		/// When the device rotates, the OS calls this method to determine if it should try and rotate the
		/// application and then call WillAnimateRotation
		/// </summary>
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// we're passed to orientation that it will rotate to. in our case, we could
			// just return true, but this switch illustrates how you can test for the
			// different cases
			switch (toInterfaceOrientation)
			{
			case UIInterfaceOrientation.LandscapeLeft:
			case UIInterfaceOrientation.LandscapeRight:
			case UIInterfaceOrientation.Portrait:
			case UIInterfaceOrientation.PortraitUpsideDown:
			default:
				return true;
			}
		}

		public override void WillAnimateRotation (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillAnimateRotation (toInterfaceOrientation, duration);

			switch (toInterfaceOrientation) {
			// if we're switching to landscape
			case UIInterfaceOrientation.LandscapeLeft:
			case UIInterfaceOrientation.LandscapeRight:

				UIApplication.SharedApplication.SetStatusBarHidden (true, true);

				this.TabBarController.TabBar.Hidden = true;

				break;

				// we're switch back to portrait
			case UIInterfaceOrientation.Portrait:
			case UIInterfaceOrientation.PortraitUpsideDown:
				
				this.TabBarController.TabBar.Hidden = false;

				break;
			}
		}
	}
}

