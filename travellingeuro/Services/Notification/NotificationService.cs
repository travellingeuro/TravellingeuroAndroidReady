﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using travellingeuro.Services.Request;

namespace travellingeuro.Services.Notification
{
    public class NotificationService : INotificationService
    {
        readonly IRequestService requestService;

        public NotificationService(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        public Task SendNotification(string serialnumber)
        {
            var builder = new UriBuilder(AppSettings.NotificationEndPoint);

            builder.Query = $"?serial={serialnumber}";

            var uri = builder.ToString();

            return requestService.GetAsync<string>(uri);
        }
    }
}
