namespace Sources.Infrastructure.States
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload nameScene);
    }
}