namespace RPG.Core.Skill
{
    public interface ISkill
    {
        public SkillData Data { get; }
        public void LevelUp();
    }
}
