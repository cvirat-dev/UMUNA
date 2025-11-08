using Umuna.Core.Domain.Interfaces;

namespace Umuna.Core.Domain.Data
{
    public class User : IUserData
    {
        private string _playerName = "default_name";
        private string _id = string.Empty;
        private string? _playerEmail;
        private string _playerPassword = "default_password";

        public string PlayerName
        {
            get => _playerName;
            set => _playerName = value;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public string? PlayerEmail
        {
            get => _playerEmail;
            set => _playerEmail = value;
        }

        public string PlayerPassword
        {
            get => _playerPassword;
            set => _playerPassword = value;
        }
    }
}
