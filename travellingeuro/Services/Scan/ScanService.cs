using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using travellingeuro.Services.Dialogs;

namespace travellingeuro.Services.Scan
{
    public class ScanService : IScanService
    {
        readonly IDialogService dialogService;
        public ScanService(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
        public async Task<string> GetSearchAsync()
        {

            MediaFile photo = await GetPhotoAsync();

            try
            {
                ApiKeyServiceClientCredentials apiKeyServiceClient = new ApiKeyServiceClientCredentials(AppSettings.CognitiveServiceKey);
                var handler = new HttpClient();
                OcrResult text;
                ComputerVisionClient client = new ComputerVisionClient(apiKeyServiceClient, new DelegatingHandler[] { })
                {
                    Endpoint = AppSettings.CognitiveServiceEndPoint
                };

                if (photo != null)

                {
                    using (Stream photoStream = photo.GetStream())

                    {
                        text = await client.RecognizePrintedTextInStreamAsync(true, photoStream);

                    }

                    foreach (var region in text.Regions)
                    {
                        foreach (var line in region.Lines)
                        {
                            foreach (var word in line.Words)
                            {
                                var result = Convert.ToString(word.Text).ToUpper();
                                if (string.IsNullOrEmpty(result))
                                {
                                    await dialogService.ShowAlertAsync(Resources.NoMatch, Resources.ErrorTitle, Resources.DialogOk);
                                }
                                return result;

                            }
                        }
                    }
                }
                await dialogService.ShowAlertAsync(Resources.NoPicture, Resources.ErrorTitle, Resources.DialogOk);
                return string.Empty;
            }
            catch (Exception ex)
            {
                await dialogService.ShowAlertAsync(ex.Message, ex.InnerException.ToString(), "OK");
                await dialogService.ShowAlertAsync(Resources.HttpRequestExceptionMessage, Resources.ErrorTitle, Resources.DialogOk);
            }

            return String.Empty;
        }

        private async Task<MediaFile> GetPhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable)
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = false,
                    Directory = "Sample",
                    Name = "SerialPic",
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    CompressionQuality = 65,
                    AllowCropping = true
                });
                return photo;
            }
            else
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                return photo;
            }

        }


    }
}
