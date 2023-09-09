namespace RPG.Core
{
    public class Managers : Singleton<Managers>
    {
        DataManager Data;

        private void Awake()
        {
            CreateInstance();
            Init();
        }

        private void CreateInstance()
        {
            Data = new DataManager();
        }

        private void Init()
        {
            Data.Init();
        }
    }
}
