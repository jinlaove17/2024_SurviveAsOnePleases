public class ZombieChaseState : Singleton<ZombieChaseState>, State<Zombie>
{
    public void Enter(Zombie zombie)
    {
        zombie.PrepareChase();
    }

    public void Update(Zombie zombie)
    {

    }

    public void FixedUpdate(Zombie zombie)
    {
        if (zombie.isDead)
        {
            return;
        }

        zombie.Chase();
    }

    public void Exit(Zombie zombie)
    {

    }
}
