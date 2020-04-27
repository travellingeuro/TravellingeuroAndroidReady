using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace travellingeuro.Services.SMS
{
    public interface ISmsService
    {
        Task<string> SendSMSAsync(string phonenumber);
    }
}
