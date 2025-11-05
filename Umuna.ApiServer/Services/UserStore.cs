using Umuna.ApiServer.Dtos;

namespace Umuna.ApiServer.Services
{
    public static class UserStore
    {
        public static List<UserDataDto> Users { get; } =
        [
            new("SlimShady", "1", "test@example.com", "realshady"),
            new("testuser", "2", "test@example.com", "123"),
            new("admin", "3", "admin@mail.com", "admin123"),
            new("guest", "4", "guest@mail.com", "guest123")
            // Add more users as needed
        ];

        public static UserDataDto? FindUser(string playerName, string password)
        {
            return Users.FirstOrDefault(u => u.UserName == playerName && u.UserPassword == password);
        }
    }
}