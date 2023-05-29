using BlazorWalletTrackerBL.Models;
using BlazorWalletTrackerBL.Shared.Settings;
using BlazorWalletTrackerDAL.Models;
using BlazorWalletTrackerDAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlazorWalletTrackerBL.Models;

namespace BlazorWalletTrackerBL.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _userRepo;
        private readonly TokenSettings _tokenSettings;
        public UserServices(IUserRepo userRepo, IOptions<TokenSettings> tokenSettings)
        {
            _userRepo = userRepo;
            _tokenSettings = tokenSettings.Value;
        }

        private static string HashedPassword(string plainPassword)
        {
            byte[] salt = new byte[16];
            using (var randomGenerator = RandomNumberGenerator.Create())
            {
                randomGenerator.GetBytes(salt);
            }
            var rfcPassowrd = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);
            byte[] rfcPasswordHash = rfcPassowrd.GetBytes(20);
            byte[] passwordHash = new byte[36];
            Array.Copy(salt, 0, passwordHash, 0, 16);
            Array.Copy(rfcPasswordHash, 0, passwordHash, 16, 20);
            return Convert.ToBase64String(passwordHash);
        }

        public bool CheckUserUniqueEmail(string email)
        {
            return !_userRepo.IsUserAvailable(email);
        }

        public async Task<(bool IsLoginSuccess, JWTTokenResponse? TokenResponse)> LoginUser(LoginUserModel loginpayload)
        {
            if (string.IsNullOrEmpty(loginpayload.Email) ||
        string.IsNullOrEmpty(loginpayload.Password))
            {
                return (false, null);
            }

            User user = await _userRepo.GetUserByEmail(loginpayload.Email);

            if (user == null) { return (false, null); }

            bool validUserPassowrd = PasswordVerification(loginpayload.Password, user.Password);
            if (!validUserPassowrd) { return (false, null); }

            string jwtAccessToken = GenerateJwtAccessToken(user);
            var result = new JWTTokenResponse
            {
                AccessToken = jwtAccessToken,
            };
            return (true, result);
        }

        public async Task<(bool IsUserRegistered, string? Message)> RegisterNewUser(UserBL _user)
        {
            if (_userRepo.IsUserAvailable(_user.Email))
                return (false, "Email already registered");
            var mapper = AutoMapperconfig.InitializeAutomapper();
            User userDAL = mapper.Map<User>(_user);
            userDAL.Password = HashedPassword(userDAL.Password);
            return await _userRepo.AddUser(userDAL);
        }

        private static bool PasswordVerification(string plainPassword, string dbPassword)
        {
            byte[] dbPasswordHash = Convert.FromBase64String(dbPassword);

            byte[] salt = new byte[16];
            Array.Copy(dbPasswordHash, 0, salt, 0, 16);

            var rfcPassowrd = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);
            byte[] rfcPasswordHash = rfcPassowrd.GetBytes(20);

            for (int i = 0; i < rfcPasswordHash.Length; i++)
            {
                if (dbPasswordHash[i + 16] != rfcPasswordHash[i])
                {
                    return false;
                }
            }
            return true;
        }

        private string GenerateJwtAccessToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));

            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user?.Name ?? string.Empty),
                new Claim("Email", user?.Email ?? string.Empty)
            };

            var securityToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public async Task<string> GetUserSecurityQuestion(string email)
        {
            return await _userRepo.GetUserSecurityQuestion(email);
        }
        public async Task<bool> IsUserSecurityAnswerValid(string email, string answer)
        {
            return await _userRepo.ValidateUserSecurityAnswer(email, answer);
        }

        public async Task<bool> ChangeUserPassword(string email, string newPassword)
        {
            string hashedPassword = HashedPassword(newPassword);
            return await _userRepo.ChangePassword(email, hashedPassword);
        }
    }

}
