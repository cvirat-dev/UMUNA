using System;
using Umuna.Core.SharedData.Umuna;

namespace Umuna.Core.SharedData
{
    [Serializable]
    public class UmunaData
    {
        public int version = 1;
        public string GameName = "default_name";
        public string UserName = "default_user_name";
        public UserData UserData = new UserData();
        public CameraData CameraData = new CameraData();
        public SettingsData SettingsData = new SettingsData();
    }


}
