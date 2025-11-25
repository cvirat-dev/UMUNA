namespace Umuna.Core.Contracts.DTOs.User
{
    public class UserCreateDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
