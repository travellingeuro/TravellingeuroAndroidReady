using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace travellingeuro.Services.Notification
{
    public interface INotificationService
    {
        Task SendNotification(string serialnumber);
    }
}
