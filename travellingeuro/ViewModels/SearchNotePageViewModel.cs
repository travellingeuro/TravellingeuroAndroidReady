using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using travellingeuro.Exceptions;
using travellingeuro.Helper;
using travellingeuro.Models;
using travellingeuro.Services.Dialogs;
using travellingeuro.Services.Scan;
using travellingeuro.Services.SearchNote;

namespace travellingeuro.ViewModels
{
    class SearchNotePageViewModel : BindableBase, INavigationAware
    {
        //Service
        public INavigationService NavigationService { get; private set; }
        public IDialogService DialogService { get; private set; }
        public ISearchNote SearchNote { get; private set; }
        public IScanService ScanService { get; private set; }


        //Commands
        public DelegateCommand SearchNoteCommand { get; set; }
        public DelegateCommand ScanCommand { get; set; }
        public DelegateCommand ShowSpecimenCommand { get; set; }


        //properties
        private string serialnumber;
        public string SerialNumber
        {
            get { return serialnumber; }
            set { SetProperty(ref serialnumber, value); }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private List<Uploads> uploads;
        public List<Uploads> Uploads
        {
            get { return uploads; }
            set { SetProperty(ref uploads, value); }
        }


        public SearchNotePageViewModel(INavigationService navigationService, IDialogService dialogService, ISearchNote searchNote, IScanService scanService)
        {
            this.NavigationService = navigationService;
            this.DialogService = dialogService;
            this.ScanService = scanService;
            this.SearchNote = searchNote;
            this.SearchNoteCommand = new DelegateCommand(async () => await SearchNoteMethod());
            this.ScanCommand = new DelegateCommand(async () => await ScanAsyncMethod());
            this.ShowSpecimenCommand = new DelegateCommand(ShowSpecimenAsyncMethod);
        }



        async Task ScanAsyncMethod()
        {
            try
            {
                IsBusy = true;
                SerialNumber = await ScanService.GetSearchAsync();
            }
            catch (Exception ex)

            {
                await DialogService.ShowAlertAsync(ex.Message, Resources.ErrorTitle, Resources.DialogOk);
            }
            finally
            {
                IsBusy = false;
            }

        }

        async Task SearchNoteMethod()
        {

            Checkvalue checkvalue = new Checkvalue();
            var result = checkvalue.Checknumber(SerialNumber);
            if (!string.IsNullOrEmpty(SerialNumber) && result == true)
            {
                await SearchNoteAsync();
            }
            else
            {
                await DialogService.ShowAlertAsync("Review your entry", checkvalue.Message, "OK");
            }

        }

        async Task SearchNoteAsync()
        {
            try
            {
                IsBusy = true;
                Uploads = await SearchNote.GetSearchAsync(SerialNumber);
                var parameters = new NavigationParameters { { "Uploads", Uploads } };
                await NavigationService.NavigateAsync("ViewUpload", parameters);

            }
            catch (HttpRequestException httpEx)
            {

                Debug.WriteLine($"[Booking Where Step] Error retrieving data: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await DialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }

            }
            catch (ConnectivityException cex)
            {

                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await DialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(Resources.NotFound))
                {
                    await DialogService.ShowAlertAsync("This note has not yet been entered", Resources.ErrorTitle, Resources.DialogOk);
                    var parameters = new NavigationParameters { { "SerialNumber", SerialNumber } };
                    await NavigationService.NavigateAsync("AddNote", parameters);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ShowSpecimenAsyncMethod()
        {
            await NavigationService.NavigateAsync("SpecimenPage", useModalNavigation: true);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            if (Uploads != null && Uploads.Any())
            {
                parameters.Add("Uploads", Uploads);
            }

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Uploads = (List<Uploads>)parameters["Uploads"] ?? null;

        }



    }
}
