using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using travellingeuro.Models;

namespace travellingeuro.Services.User
{
    public interface IUserService
    {
        Task<List<Users>> GetUserEmail(string email);
        Task<string> PostUser(Models.Users user);
    }
}
