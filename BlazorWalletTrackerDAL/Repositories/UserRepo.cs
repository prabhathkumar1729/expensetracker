using BlazorWalletTrackerDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWalletTrackerDAL.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ExpenseTrackerDbContext _db;
        public UserRepo(ExpenseTrackerDbContext db) { _db = db; }
        public async Task<(bool IsUserRegistered, string Message)> AddUser(User _user)
        {
            _db.Users.Add(_user);
            await _db.SaveChangesAsync();
            return (true, "Successfully Registered");

        }

        public async Task<bool> ChangePassword(string email, string password)
        {
            User user = await GetUserByEmail(email);
            user.Password = password;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _db.Users.Where(_ => _.Email.ToLower() == email.ToLower())
        .FirstOrDefaultAsync();
        }

        public bool IsUserAvailable(string email)
        {
            return _db.Users.Any(u => u.Email.ToLower() == email.ToLower());
        }

        public Task<bool> RemoveUser(User _user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserSecurityQuestion(string email)
        {
            User user = await _db.Users.Where(_ => _.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            return user.SecurityQuestion;
        }

        public async Task<bool> ValidateUserSecurityAnswer(string email, string securityAnswer)
        {
            User user = await _db.Users.Where(_ => _.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            return user.SecurityAnswer == securityAnswer;
        }
    }
}
