using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umuna.Core.Data.Umuna
{
    [Serializable]
    public class UserData
    {
        private string _playerName = "default_name";
        private string _playerId = "default_id";

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
    }
}
