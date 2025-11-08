using Umuna.Core.Domain.Data;

namespace Umuna.ApiServer.DTOs
{
    public class UserDataDto
    {
        public string UserName { get; set; } = "default_name";
        public string UserId { get; set; } = string.Empty;
        public string? UserEmail { get; set; }
        public string UserPassword { get; set; } = "default_password";
        public UserDataDto() { }
        public UserDataDto(string userName, string userId, string? userEmail, string userPassword)
        {
            UserName = userName;
            UserId = userId;
            UserEmail = userEmail;
            UserPassword = userPassword;
        }
        public UserDataDto(User userData)
        {
            UserName = userData.PlayerName;
            UserId = userData.Id;
            UserEmail = userData.PlayerEmail;
            UserPassword = userData.PlayerPassword;
        }

    }
}
