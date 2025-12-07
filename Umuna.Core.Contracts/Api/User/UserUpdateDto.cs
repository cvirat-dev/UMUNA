namespace Umuna.Core.Contracts.Api.User
{
    public class UserUpdateDto
    {
        // The ID comes from the route, not the DTO.
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        // Optional: update only when provided
        public string? Password { get; set; }

        }
}
