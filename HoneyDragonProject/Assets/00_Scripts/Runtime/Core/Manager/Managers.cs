namespace RPG.Core.Manager
{
    public class Managers : Singleton<Managers>
    {
        public DataManager Data { get; private set; }
        public SkillManager Skill { get; private set; }
        public StageManager Stage { get; private set; }


        private void Awake()
        {
            CreateInstance();
            Init();
        }

        private void CreateInstance()
        {
            Data = new DataManager();
            Skill = new SkillManager();
            Stage = new StageManager();
        }

        private void Init()
        {
            Data.Init();
            Stage.Init(Data);
            Skill.Init(Data);
        }
    }
}
