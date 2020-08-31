using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzEwOTEwQDMxMzgyZTMyMmUzMEt3ZGc2SjM1Z0dGcEVYdzc1KzY4K3l1RURTQjR0RXN4SkpyNzMvR2M1MWs9");
 
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
    
            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);            
            UserDialogs.Init(this);
            AppCenter.Start("93ae46ad-ed4c-40ca-82df-e12c3fdab482;ios=fc96f24b-04b6-490d-a920-7ed0956a5bee", typeof(Analytics), typeof(Crashes));
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

