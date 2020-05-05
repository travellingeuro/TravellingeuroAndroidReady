using Syncfusion.SfMaps.XForms.iOS;
using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;
using Google.MobileAds;

namespace travellingeuro.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjM4MzQwQDMxMzgyZTMxMmUzMFJtcjh3WkhxNGc5MEpNT29sb3BUeGdkQS91MnRqeGl1S3VDanpKUEYwMVk9");
global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
SfMapsRenderer.Init();
            MobileAds.SharedInstance.Start(CompletionHandler);
            SfGradientViewRenderer.Init();
            SfBorderRenderer.Init();
            SfButtonRenderer.Init();
            SfComboBoxRenderer.Init();
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        private void CompletionHandler(InitializationStatus status) { }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
