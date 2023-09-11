namespace RPG.Core
{
    public class Managers : Singleton<Managers>
    {
        public DataManager Data { get; private set; }
        public SkillManager Skill { get; private set; }


        private void Awake()
        {
            CreateInstance();
            Init();
        }

        private void CreateInstance()
        {
            Data = new DataManager();
            Skill = new SkillManager();
        }

        private void Init()
        {
            Data.Init();
            Skill.Init(Data);
        }
    }
}
