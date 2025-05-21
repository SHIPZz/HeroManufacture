using System;

namespace CodeBase.Data
{
    [Serializable]
    public class ProgressData
    {
        public SettingsData SettingsData = new();
        public PlayerData PlayerData = new();
    }

    [Serializable]
    public class SettingsData
    {
        public bool IsSoundEnabled = true;
    }
    
    [Serializable]
    public class PlayerData
    {
    }
}