public class FiniteStateMachine<T> where T : Entity
{
    public State<T> curState { get; private set; }

    public void ChangeState(T owner, State<T> state)
    {
        if (curState != null)
        {
            curState.Exit(owner);
        }

        curState = state;

        if (curState != null)
        {
            curState.Enter(owner);
        }
    }

    public void FixedUpdate(T owner)
    {
        if (curState != null)
        {
            curState.FixedUpdate(owner);
        }
    }

    public void Update(T owner)
    {
        if (curState != null)
        {
            curState.Update(owner);
        }
    }
}
