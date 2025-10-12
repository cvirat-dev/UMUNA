using Umuna.Core.SharedData.Umuna;

namespace Umuna.ApiServer.Dtos
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
        public UserDataDto(UserData userData)
        {
            UserName = userData.PlayerName;
            UserId = userData.PlayerId;
            UserEmail = userData.PlayerEmail;
            UserPassword = userData.PlayerPassword;
        }

    }
}
