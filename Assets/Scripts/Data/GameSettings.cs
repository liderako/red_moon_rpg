using RedMoonRPG.Utils;
using RedMoonRPG.Settings.Objects;

namespace RedMoonRPG.Settings
{
    public class GameSettings : Singleton<GameSettings>
    {
        public Data Model;
        public void Start()
        {
            DontDestroyOnLoad(gameObject);
            LoadingData();
        }

        public void LoadingData()
        {
            FileReader reader = new FileReader();
            Model = reader.GetGameSettings();
        }
    }
}