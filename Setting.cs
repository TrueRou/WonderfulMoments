using Sync.Tools;
using Sync.Tools.ConfigurationAttribute;

namespace WonderfulMoments
{
    class Setting : IConfigurable
    {
        internal static class SettingValue
        {
            public static string WebSocketURL = "ws://localhost:4444";
            public static string Password = "";
            public static bool FullMode = true;
            public static int Limit = 10;
        }

        public ConfigurationElement WebSocketURL
        {
            get => SettingValue.WebSocketURL;
            set => SettingValue.WebSocketURL = value.ToString();
        }

        public ConfigurationElement Password
        {
            get => SettingValue.Password;
            set => SettingValue.Password = value.ToString();
        }

        [Bool(RequireRestart = false)]
        public ConfigurationElement FullMode
        {
            get => SettingValue.FullMode.ToString();
            set => SettingValue.FullMode = bool.Parse(value);
        }

        [Integer(RequireRestart = false, MaxValue = 1000, MinValue = 1)]
        public ConfigurationElement VideosLimit
        {
            get => SettingValue.Limit.ToString();
            set => SettingValue.Limit = int.Parse(value);
        }

        public void onConfigurationLoad()
        {
        }

        public void onConfigurationReload()
        {
        }

        public void onConfigurationSave()
        {
        }
    }
}
