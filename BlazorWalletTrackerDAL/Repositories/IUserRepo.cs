using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWalletTrackerDAL.Models;

namespace BlazorWalletTrackerDAL.Repositories
{
    public interface IUserRepo
    {
        Task<(bool IsUserRegistered, string Message)> AddUser(User _user);
        Task<bool> RemoveUser(User _user);
        Task<bool> ChangePassword(string email, string password);
        bool IsUserAvailable(string email);

        Task<User?> GetUserByEmail(string email);
        Task<string> GetUserSecurityQuestion(string email);
        Task<bool> ValidateUserSecurityAnswer(string email, string answer);

    }
}
