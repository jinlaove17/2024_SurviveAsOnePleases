public interface State<T> where T : Entity
{
    public abstract void Enter(T entity);
    public abstract void FixedUpdate(T entity);
    public abstract void Update(T entity);
    public abstract void Exit(T entity);
}
