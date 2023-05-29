using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWalletTrackerBL.Models;

namespace BlazorWalletTrackerBL.Services
{
    public interface IUserServices
    {
        Task<(bool IsUserRegistered, string? Message)> RegisterNewUser(UserBL _user);
        Task<(bool IsLoginSuccess, JWTTokenResponse? TokenResponse)> LoginUser(LoginUserModel _user);

        bool CheckUserUniqueEmail(string email);

        Task<string> GetUserSecurityQuestion(string email);
        Task<bool> IsUserSecurityAnswerValid(string email, string answer);
        Task<bool> ChangeUserPassword(string email, string newPassword);
    }
}
