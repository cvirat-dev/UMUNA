namespace Umuna.Core.Domain.Interfaces
{
    public interface IUserData
    {
        string? PlayerEmail { get; set; }
        string Id { get; set; }
        string PlayerName { get; set; }
        string PlayerPassword { get; set; }
    }
}