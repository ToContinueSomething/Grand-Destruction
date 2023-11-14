namespace StateMachine
{
    public class MoveTargetToHorizontalState : MoveTargetState
    {
        protected override void MoveTo()
        {
            Movable.MoveToHorizontal();
        }
    }
}