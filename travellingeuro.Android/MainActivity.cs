using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;

namespace travellingeuro.Droid
{
    [Activity(Label = "travellingeuro", Icon = "@mipmap/logo", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {


            Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, AppSettings.AndroidAdsKey);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjM4MzQwQDMxMzgyZTMxMmUzMFJtcjh3WkhxNGc5MEpNT29sb3BUeGdkQS91MnRqeGl1S3VDanpKUEYwMVk9");
 
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
    
            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            LoadApplication(new App(new AndroidInitializer()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

