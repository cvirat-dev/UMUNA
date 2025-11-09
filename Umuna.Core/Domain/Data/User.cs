using System;
using Umuna.Core.Domain.Interfaces;

namespace Umuna.Core.Domain.Data
{
    public class User : IUser
    {
        private string _playerName = "default_name";
        private string _id = string.Empty;
        private string _playerEmail = "default_email";
        private string _playerPassword = "default_password";

        public string Name
        {
            get => _playerName;
            set => _playerName = value;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public string Email
        {
            get => _playerEmail;
            set => _playerEmail = value;
        }

        public string Password
        {
            get => _playerPassword;
            set => _playerPassword = value;
        }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
