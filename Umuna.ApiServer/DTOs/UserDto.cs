using Umuna.Core.Domain.Data;

namespace Umuna.ApiServer.DTOs
{
    public class UserDto
    {
        public string Name { get; set; } = "default_name";
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = "default_email";
        public string Password { get; set; } = "default_password";
        
        public UserDto() { }
        public UserDto(string userName, string userId, string userEmail, string userPassword)
        {
            Name = userName;
            Id = userId;
            Email = userEmail;
            Password = userPassword;
        }
        public UserDto(User userData)
        {
            Name = userData.Name;
            Id = userData.Id;
            Email = userData.Email;
            Password = userData.Password;
        }

    }
}
