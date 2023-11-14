namespace StateMachine
{
    public class MoveTargetToVerticalState : MoveTargetState
    {
        protected override void MoveTo()
        {
            Movable.MoveToVertical();
        }
    }
}