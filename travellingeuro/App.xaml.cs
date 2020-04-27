using Prism;
using Prism.Ioc;
using travellingeuro.Services.AddNote;
using travellingeuro.Services.Dialogs;
using travellingeuro.Services.Notification;
using travellingeuro.Services.Request;
using travellingeuro.Services.Scan;
using travellingeuro.Services.SearchNote;
using travellingeuro.Services.SearchPlace;
using travellingeuro.Services.SMS;
using travellingeuro.Services.User;
using travellingeuro.ViewModels;
using travellingeuro.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace travellingeuro
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PresentationPage, PresentationPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchNotePage, SearchNotePageViewModel>();
            containerRegistry.RegisterForNavigation<PhoneNumberPage, PhoneNumberPageViewModel>();
            containerRegistry.RegisterForNavigation<TokenPage, TokenPageViewModel>();
            containerRegistry.RegisterForNavigation<SpecimenPage, SpecimenPageViewModel>();
            containerRegistry.RegisterForNavigation<AddNote, AddNoteViewModel>();
            containerRegistry.RegisterForNavigation<ViewUpload, ViewUploadViewModel>();
            
            //Services
            containerRegistry.Register<IDialogService, DialogService>();
            containerRegistry.Register<IRequestService, RequestService>();
            containerRegistry.Register<IScanService, ScanService>();
            containerRegistry.Register<ISearchNote, SearchNote>();
            containerRegistry.Register<ISmsService, SmsService>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IAddNoteService, AddNoteService>();
            containerRegistry.Register<ISearchPlace, SearchPlace>();
            containerRegistry.Register<INotificationService, NotificationService>();




            
        }
    }
}
