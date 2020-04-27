using Xamarin.Essentials;

namespace travellingeuro
{
    public static class AppSettings
    {
        //google ads keys
        const string defaultAndroidAdsKey = "ca-app-pub-9800707284712065~1990749964";
        const string defaultIosAdsKey = "ca-app-pub-9800707284712065~5378249062";

        //google ads id's
        const string defaultAndroidAds = "ca-app-pub-9800707284712065/3418493654";
        const string defaultIosAds = "ca-app-pub-9800707284712065/6044656994";
        const string defaultSampleAndroidAds = "ca-app-pub-3940256099942544/6300978111";
        const string defaultSampleIosAds = "ca-app-pub-3940256099942544/2934735716";

        //Other services

        const string defaultCognitiveServiceKey = "27a4453a28ab4df8af50d22fed37bcc0";
        const string defaultGooglePlacesApiKey = "AIzaSyAA5f_usWVL2jbldPd4KDKFFZIrMuKYD6U";
        const string defaultGoogleMapsIosKey = "AIzaSyCgNTU_EGivVGG9uS2FrvkdRinGzAmd99g";


        //EndPoints
        static readonly string defautlSearchEndPoint;
        static readonly string defaultUserEndPoint;
        static readonly string defaultUploadsEndPoint;
        static readonly string defaultMintsEndPoint;
        static readonly string defaultCoginitiveServiceEndPoint;
        static readonly string defaultSMSEndPoint;
        static readonly string defaultGoogleBaseEndPoint;
        static readonly string defaultNotificationEndPoint;

        static AppSettings()
        {
            defautlSearchEndPoint = "https://travellingeurowebapi.azurewebsites.net/api/notes/";
            defaultUserEndPoint = "https://travellingeurowebapi.azurewebsites.net/api/users/";
            defaultUploadsEndPoint = "https://travellingeurowebapi.azurewebsites.net/api/Uploads/";
            defaultMintsEndPoint = "https://travellingeurowebapi.azurewebsites.net/api/mints/";
            defaultCoginitiveServiceEndPoint = "https://southcentralus.api.cognitive.microsoft.com/";
            defaultSMSEndPoint = "https://travellingeurowebapi.azurewebsites.net/api/sms";
            defaultGoogleBaseEndPoint = "https://maps.googleapis.com/maps/";
            defaultNotificationEndPoint = "https://travellingeurowebapi.azurewebsites.net/api/notifications/";


        }

        //Properties
        public static string AndroidAdsKey
        {
            get => Preferences.Get(nameof(AndroidAdsKey), defaultAndroidAdsKey);
            set => Preferences.Set(nameof(AndroidAdsKey), value);
        }

        public static string IosAdsKey
        {
            get => Preferences.Get(nameof(IosAdsKey), defaultIosAdsKey);
            set => Preferences.Set(nameof(IosAdsKey), value);
        }

        public static string AndroidAds
        {
            get => Preferences.Get(nameof(AndroidAds), defaultAndroidAds);
            set => Preferences.Set(nameof(AndroidAds), value);
        }

        public static string IosAds
        {
            get => Preferences.Get(nameof(IosAds), defaultIosAds);
            set => Preferences.Set(nameof(IosAds), value);
        }

        public static string SampleAndroidAds
        {
            get => Preferences.Get(nameof(SampleAndroidAds), defaultSampleAndroidAds);
            set => Preferences.Set(nameof(SampleAndroidAds), value);
        }

        public static string SampleIosAds
        {
            get => Preferences.Get(nameof(SampleIosAds), defaultSampleIosAds);
            set => Preferences.Set(nameof(SampleIosAds), value);
        }




        public static string CognitiveServiceKey
        {
            get => Preferences.Get(nameof(CognitiveServiceKey), defaultCognitiveServiceKey);
            set => Preferences.Set(nameof(CognitiveServiceKey), value);
        }

        public static string GooglePlacesApiKey
        {
            get => Preferences.Get(nameof(GooglePlacesApiKey), defaultGooglePlacesApiKey);
            set => Preferences.Set(nameof(GooglePlacesApiKey), value);
        }

        public static string GoogleMapsIosKey
        {
            get => Preferences.Get(nameof(GoogleMapsIosKey), defaultGoogleMapsIosKey);
            set => Preferences.Set(nameof(GoogleMapsIosKey), value);
        }

        public static string SearchEndPoint
        {
            get => Preferences.Get(nameof(SearchEndPoint), defautlSearchEndPoint);
            set => Preferences.Set(nameof(SearchEndPoint), value);
        }

        public static string UserEndPoint
        {
            get => Preferences.Get(nameof(UserEndPoint), defaultUserEndPoint);
            set => Preferences.Set(nameof(UserEndPoint), value);
        }

        public static string UploadsEndPoint
        {
            get => Preferences.Get(nameof(UploadsEndPoint), defaultUploadsEndPoint);
            set => Preferences.Set(nameof(UploadsEndPoint), value);
        }

        public static string MintsEndPoint
        {
            get => Preferences.Get(nameof(MintsEndPoint), defaultMintsEndPoint);
            set => Preferences.Set(nameof(MintsEndPoint), value);
        }



        public static string CognitiveServiceEndPoint
        {
            get => Preferences.Get(nameof(CognitiveServiceEndPoint), defaultCoginitiveServiceEndPoint);
            set => Preferences.Set(nameof(CognitiveServiceEndPoint), value);
        }

        public static string SMSEndPoint
        {
            get => Preferences.Get(nameof(SMSEndPoint), defaultSMSEndPoint);
            set => Preferences.Set(nameof(SMSEndPoint), value);
        }

        public static string GoogleBaseEndPoint
        {
            get => Preferences.Get(nameof(GoogleBaseEndPoint), defaultGoogleBaseEndPoint);
            set => Preferences.Set(nameof(GoogleBaseEndPoint), value);
        }

        public static string NotificationEndPoint
        {
            get => Preferences.Get(nameof(NotificationEndPoint), defaultNotificationEndPoint);
            set => Preferences.Set(nameof(NotificationEndPoint), value);
        }



    }
}
