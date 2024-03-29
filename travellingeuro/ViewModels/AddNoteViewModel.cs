﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using travellingeuro.Exceptions;
using travellingeuro.Helper;
using travellingeuro.Models;
using travellingeuro.Services.AddNote;
using travellingeuro.Services.Dialogs;
using travellingeuro.Services.Notification;
using travellingeuro.Services.Scan;
using travellingeuro.Services.SearchNote;
using travellingeuro.Services.SearchPlace;
using travellingeuro.Services.User;
using Xamarin.Forms.Internals;

namespace travellingeuro.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddNoteViewModel : BindableBase, INavigationAware
    {
        //Services
        public INavigationService NavigationService { get; private set; }
        public IDialogService dialogService { get; private set; }
        public IUserService userService { get; private set; }
        public ISearchPlace searchPlace { get; private set; }
        public IScanService scanService { get; private set; }
        public IAddNoteService addnoteService { get; private set; }
        public ISearchNote searchNoteService { get; private set; }
        public INotificationService NotificationService { get; private set; }



        //Commands
        public ICommand FocusOriginCommand { get; set; }
        public DelegateCommand ShowSpecimenCommand { get; set; }
        public DelegateCommand ScanCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand ViewMapCommand { get; set; }
        public DelegateCommand AddAnotherCommand { get; set; }
        public DelegateCommand NavigateToUploadsCommand { get; set; }


        //Properties
        private List<Uploads> uploads;
        public List<Uploads> Uploads
        {
            get { return uploads; }
            set { SetProperty(ref uploads, value); }
        }

        private bool isbusy;
        public bool IsBusy
        {
            get { return isbusy; }
            set { SetProperty(ref isbusy, value); }
        }

        private string serialnumber;
        public string SerialNumber
        {
            get { return serialnumber; }
            set { SetProperty(ref serialnumber, value); }
        }

        private object notevalue;
        public object NoteValue
        {
            get { return notevalue; }
            set { SetProperty(ref notevalue, value); }
        }


        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private bool showemailentry;
        public bool ShowEmailEntry
        {
            get { return showemailentry; }
            set { SetProperty(ref showemailentry, value); }
        }

        private string comments;
        public string Comments
        {
            get { return comments; }
            set { SetProperty(ref comments, value); }
        }

        private ObservableCollection<GooglePlaceAutoCompletePrediction> places;
        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places
        {
            get { return places; }
            set { SetProperty(ref places, value); }
        }

        private GooglePlace selectedplace;
        public GooglePlace SelectedPlace
        {
            get { return selectedplace; }
            set { SetProperty(ref selectedplace, value); }
        }

        private bool isviewonmapenable;
        public bool IsViewonMapEnable
        {
            get { return isviewonmapenable; }
            set { SetProperty(ref isviewonmapenable, value); }
        }

        bool isPickupFocused = true;
        bool isListViewVisible = false;
        public bool IsListVisible
        {
            get => isListViewVisible;
            set
            {
                isListViewVisible = value;
            }
        }



        string search;
        public string Search
        {
            get => search;
            set
            {
                search = value;
                if (!string.IsNullOrEmpty(search))
                {
                    isPickupFocused = true;
                    isListViewVisible = true;
                    _ = GetPlacesByName();
                    RaisePropertyChanged();
                }
            }
        }

        GooglePlaceAutoCompletePrediction placeSelected;
        public GooglePlaceAutoCompletePrediction PlaceSelected
        {
            get => placeSelected;
            set
            {
                placeSelected = value;
                isPickupFocused = true;
                isListViewVisible = false;
                if (placeSelected != null)
                    _ = GetPlaceDetail();
                RaisePropertyChanged();
            }
        }


        public AddNoteViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IUserService userService,
            ISearchPlace searchPlace,
            IScanService scanService,
            IAddNoteService addNoteService,
            ISearchNote searchNoteSercice,
            INotificationService notificationService)
        {
            this.NavigationService = navigationService;
            this.dialogService = dialogService;
            this.userService = userService;
            this.searchPlace = searchPlace;
            this.scanService = scanService;
            this.addnoteService = addNoteService;
            this.searchNoteService = searchNoteSercice;
            this.NotificationService = notificationService;
            this.ShowEmailEntry = emailexist();
            this.ShowSpecimenCommand = new DelegateCommand(ShowSpecimenAsyncMethod);
            this.ScanCommand = new DelegateCommand(async () => await ScanAsyncMethod());
            this.AddCommand = new DelegateCommand(AddMethod);
            this.ViewMapCommand = new DelegateCommand(ViewonMapMethod, CanViewOnMap).ObservesProperty(() => IsViewonMapEnable);
            this.AddAnotherCommand = new DelegateCommand(AddAnotherMethod, CanViewOnMap).ObservesProperty(() => IsViewonMapEnable);
            this.NavigateToUploadsCommand = new DelegateCommand(NavigateToUploadsMethod, CanViewOnMap).ObservesProperty(() => IsViewonMapEnable);


        }

        private bool emailexist()
        {
            if (App.Current.Properties.ContainsKey("user"))
            {
                Email = (string)App.Current.Properties["user"];
                return false;
            }
            return true;
        }

        private void AddAnotherMethod()
        {
            IsViewonMapEnable = false;
            CleanUp();
        }

        private async void ViewonMapMethod()
        {
            try
            {
                IsBusy = true;

                if (!String.IsNullOrEmpty(SerialNumber))
                {
                    var listofnotes = await searchNoteService.GetSearchAsync(SerialNumber);
                    var parameters = new NavigationParameters();
                    parameters.Add("Uploads", listofnotes);
                    await NavigationService.NavigateAsync("MapNotePage", parameters, useModalNavigation: true);
                }
                else
                {
                    await dialogService.ShowAlertAsync("Fill the form first", Resources.ErrorTitle, Resources.DialogOk);
                }
            }

            catch (HttpRequestException httpEx)
            {

                Debug.WriteLine($"[Booking Where Step] Error retrieving data: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await dialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }

            }
            catch (ConnectivityException cex)
            {

                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await dialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");

            }
            catch (Exception ex)
            {

                await dialogService.ShowAlertAsync(ex.Message, Resources.ErrorTitle, Resources.DialogOk);

            }
            finally
            {
                IsBusy = false;
            }

            CleanUp();

        }

        private void CleanUp()
        {
            SerialNumber = null;
            SelectedPlace = null;
            Search = null;
            NoteValue = null;
            Search = null;
            Comments = null;
            IsViewonMapEnable = false;
        }

        bool CanViewOnMap()
        {
            return IsViewonMapEnable;
        }

        private async void ShowSpecimenAsyncMethod()
        {
            await NavigationService.NavigateAsync("SpecimenPage", useModalNavigation: true);
        }

        private async void NavigateToUploadsMethod()
        {
            try
            {
                IsBusy = true;
                Uploads = await searchNoteService.GetSearchAsync(SerialNumber);
                var parameters = new NavigationParameters { { "Uploads", Uploads } };
                await NavigationService.NavigateAsync("ViewUpload", parameters);

            }
            catch (HttpRequestException httpEx)
            {

                Debug.WriteLine($"[Booking Where Step] Error retrieving data: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await dialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }

            }
            catch (ConnectivityException cex)
            {

                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await dialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(Resources.NotFound))
                {
                    await dialogService.ShowAlertAsync("This note has not yet been entered", Resources.ErrorTitle, Resources.DialogOk);
                    var parameters = new NavigationParameters { { "SerialNumber", SerialNumber } };
                    await NavigationService.NavigateAsync("AddNote", parameters);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ScanAsyncMethod()
        {
            try
            {
                IsBusy = true;
                SerialNumber = await scanService.GetSearchAsync();
            }
            catch (Exception ex)

            {
                await dialogService.ShowAlertAsync(ex.Message, Resources.ErrorTitle, Resources.DialogOk);
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Check Values
        private async void AddMethod()
        {
            if (SerialNumber == null | SelectedPlace == null | Comments == null | Email == null)

            {
                await dialogService.ShowAlertAsync("Review your entry", Resources.ErrorTitle, Resources.DialogOk);
            }
            else
            {
                //check validaity of value
                Checkvalue checkvalue = new Checkvalue();
                bool result = checkvalue.Checknumber(SerialNumber);
                //check validity of email
                Emailvalidator emailvalidator = new Emailvalidator();
                bool validemail = emailvalidator.IsValid(Email);
                if (validemail == false)
                {
                    await dialogService.ShowAlertAsync($"{Email} " + emailvalidator.NotValidMessage, Resources.ErrorTitle, Resources.DialogOk);
                }

                if (!string.IsNullOrEmpty(SerialNumber) && result == true && validemail==true)
                {
                    await LogUserAync();
                    await AddNoteAsync();
                }
                else
                {
                    await dialogService.ShowAlertAsync("Review your entry", checkvalue.Message, "OK");
                }
            }
        }

        async Task LogUserAync()
        {
            try
            {
                IsBusy = true;
                App.Current.Properties.Remove("token");
                var users = await userService.GetUserEmail(Email);
                if (users.Count == 0)
                {
                    await HandleUser();
                }

                App.Current.Properties["user"] = Email;
                await App.Current.SavePropertiesAsync();

            }
            catch (HttpRequestException httpEx)
            {

                Debug.WriteLine($"[Booking Where Step] Error retrieving data: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await dialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }

            }
            catch (ConnectivityException cex)
            {

                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await dialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");

            }

            catch (Exception ex)
            {
                await dialogService.ShowAlertAsync(ex.Message, Resources.ErrorTitle, Resources.DialogOk);
            }

            finally
            {
                IsBusy = false;
            }
        }

        async Task HandleUser()
        {
            try
            {
                Users newuser = new Users() { Email = Email, EmailConfirmed = 1, Keeplogged = 1, Keepmeinformed = 1, Role = "user", Alias = "Anonymous" };
                var response = await userService.PostUser(newuser);
                App.Current.Properties["user"] = Email;
                await App.Current.SavePropertiesAsync();
            }
            catch (HttpRequestException httpEx)
            {
                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await dialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }

            }
            catch (ConnectivityException cex)
            {
                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await dialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");

            }
            catch (Exception ex)
            {

                await dialogService.ShowAlertAsync(ex.Message, Resources.ErrorTitle, Resources.DialogOk);
            }

            finally
            {
                IsBusy = false;
            }
        }

        async Task AddNoteAsync()
        {
            try
            {
                IsBusy = true;
                var note = await addnoteService.GetSearchAsync(SerialNumber);
                if (note.Any()) //note exists in datbase
                {
                    await Newupload(note.FirstOrDefault());
                }
                else //Note doesn't exist.
                {
                    var letter = SerialNumber[0];
                    var Mints = await addnoteService.GetMintCode(letter.ToString());
                    if (Mints.Any())
                    {
                        var Id = Mints.FirstOrDefault().Id;
                        var newnote = new Notes { SerialNumber = SerialNumber.ToUpper(), Value = (string)NoteValue, MintsId = Id };
                        await addnoteService.PostNote(newnote);
                        await dialogService.ShowAlertAsync(
                            $"{newnote.Value} € bill {newnote.SerialNumber} Has been added",
                            "Congratulations",
                            "OK");
                        await Addmintupload(Mints.FirstOrDefault());

                    }
                }

            }
            catch (HttpRequestException httpEx)
            {

                Debug.WriteLine($"[Booking Where Step] Error retrieving data: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await dialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }

            }
            catch (ConnectivityException cex)
            {

                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await dialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");

            }
            catch (Exception ex)
            {

                await dialogService.ShowAlertAsync(ex.Message, Resources.ErrorTitle, Resources.DialogOk);

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Addmintupload(Mints mint)
        {
            try
            {
                IsBusy = true;
                var notes = await addnoteService.GetSearchAsync(SerialNumber);
                var id = notes.FirstOrDefault().Id;
                Uploads mintsupload = new Uploads
                {
                    UsersId = 11,
                    UploadDate = new DateTime(2002, 1, 1),
                    NotesId = id,
                    Longitude = mint.Longitude,
                    Latitude = mint.Latitude,
                    Address = mint.Address,
                    Location = mint.Location,
                    Comments = mint.Comments
                };
                await addnoteService.PostUpload(mintsupload);

                await dialogService.ShowAlertAsync(
                $"New note added in {mintsupload.Address}\n" +
                $" {mintsupload.Comments}",
                "Congratulations",
                "OK");

                await Addclientupload(notes.FirstOrDefault());
            }
            catch (HttpRequestException httpEx)
            {

                Debug.WriteLine($"[Booking Where Step] Error retrieving data: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await dialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }

            }
            catch (ConnectivityException cex)
            {

                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await dialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");

            }
            catch (Exception ex)
            {

                await dialogService.ShowAlertAsync(ex.Message, Resources.ErrorTitle, Resources.DialogOk);

            }
            finally
            {
                IsBusy = false;
            }

        }

        private async Task GetPlacesByName()
        {
            try
            {
                IsBusy = true;
                var places = await searchPlace.GetPlaces(Search);
                var placeResult = places.AutoCompletePlaces;
                if (placeResult != null && placeResult.Count > 0)
                {
                    Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
                }

            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"[Booking Where Step] Error retrieving data: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await dialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }

            }
            catch (ConnectivityException cex)
            {
                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await dialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Booking Where Step] Error: {ex}");

                await dialogService.ShowAlertAsync(
                    Resources.ExceptionMessage,
                    Resources.ExceptionTitle,
                    Resources.DialogOk);
            }
            finally
            {
                IsBusy = false;
            }

        }

        private async Task GetPlaceDetail()
        {
            SelectedPlace = await searchPlace.GetPlaceDetails(PlaceSelected.PlaceId);
            if (SelectedPlace != null)
            {
                if (isPickupFocused)
                {
                    Search = SelectedPlace.Name;
                    CleanFields();
                }
                if (isListViewVisible)
                {
                    isListViewVisible = !isListViewVisible;
                }
            }
        }

        void CleanFields()
        {
            PlaceSelected = null;
            isListViewVisible = false;
        }

        async Task Newupload(Notes note)
        {
            if (App.Current.Properties.ContainsKey("user"))
            {
                var recordednoteid = note.Id.ToString();
                var recordednotevalue = note.Value;
                List<Uploads> listuploads = await addnoteService.GetListUploads((string)App.Current.Properties["user"]);
                bool match = listuploads.Exists(stc => stc.NotesId.ToString() == recordednoteid);

                if (recordednotevalue == (string)NoteValue && !match)
                {
                    await Addclientupload(note);
                }
                else
                {
                    await dialogService.ShowAlertAsync(
                        "Review your entry. Either the amount entered is different\n" +
                        " to what we have for that note or you are trying\n" +
                        " to upload the same note twice.",
                        Resources.ErrorTitle,
                        Resources.DialogOk);
                }
            }
            else
            {
                await dialogService.ShowAlertAsync("Please confirm your device first", Resources.ErrorTitle, Resources.DialogOk);
                await NavigationService.NavigateAsync("PhoneNumberPage");
            }

        }

        async Task Addclientupload(Notes note)
        {
            try
            {
                IsBusy = true;
                string client = (string)App.Current.Properties["user"];
                var user = await userService.GetUserEmail(client);
                var userid = user.FirstOrDefault().Id;
                Uploads clientupload = new Uploads
                {
                    UsersId = userid,
                    UploadDate = DateTime.Now,
                    NotesId = note.Id,
                    Longitude = (float)SelectedPlace.Longitude,
                    Latitude = (float)SelectedPlace.Latitude,
                    Address = SelectedPlace.Address,
                    Location = SelectedPlace.Location,
                    Comments = Comments
                };

                await addnoteService.PostUpload(clientupload);

                await dialogService.ShowAlertAsync(
                    $"{clientupload.Address} € bill {clientupload.Comments} Has been added",
                    "Congratulations",
                    "OK");

                await NotificationService.SendNotification(SerialNumber);

            }
            catch (HttpRequestException httpEx)
            {

                Debug.WriteLine($"[Booking Where Step] Error retrieving data: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await dialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }

            }
            catch (ConnectivityException cex)
            {

                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await dialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");

            }
            catch (Exception ex)
            {

                await dialogService.ShowAlertAsync(ex.Message, Resources.ErrorTitle, Resources.DialogOk);

            }
            finally
            {
                IsBusy = false;
                IsViewonMapEnable = true;
            }

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

            //check parameters for "SerialNumber"
            SerialNumber = (string)parameters["SerialNumber"] ?? null;

            if (!string.IsNullOrEmpty(SerialNumber))
            {
                SerialNumber = String.Concat(SerialNumber.Where(c => !Char.IsWhiteSpace(c)));
            }
        }
    }

}
