namespace RPG.Core
{

    public interface ISaveable
    {
        public void Save(); 
    }

    public interface ILoadable
    {
        public void Load(); 
    }

    public interface ISaveLoadable : ISaveable, ILoadable
    {

    }

}
