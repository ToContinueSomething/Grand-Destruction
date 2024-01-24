namespace Sources.Logic.StateMachines
{
    public interface IStateMachine
    {
        void EnterNextState();
        void Restart();
    }
}