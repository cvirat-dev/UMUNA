
namespace Umuna.Core.SharedData.Umuna
{
    public class UserData : IUserData
    {
        private string _playerName = "default_name";
        private string _playerId = string.Empty;
        private string? _playerEmail;
        private string _playerPassword = "default_password";

        public string PlayerName
        {
            get => _playerName;
            set => _playerName = value;
        }

        public string PlayerId
        {
            get => _playerId;
            set => _playerId = value;
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
