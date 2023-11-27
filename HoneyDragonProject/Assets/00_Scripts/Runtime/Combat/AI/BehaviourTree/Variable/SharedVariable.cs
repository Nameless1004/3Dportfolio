namespace RPG.Combat.AI.BehaviourTree.Variable
{
    public class SharedVariable<T> where T : struct
    {
        public SharedVariable(T v) => Value = v;

        public static implicit operator T(SharedVariable<T> sharedVariable)
        {
            return sharedVariable.Value;
        }

        public static implicit operator SharedVariable<T>(T val)
        {
            return new SharedVariable<T>(val);
        }

        public T Value { get; set; }
    }
}
