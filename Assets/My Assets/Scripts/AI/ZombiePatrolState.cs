public class ZombiePatrolState : Singleton<ZombiePatrolState>, State<Zombie>
{
    public void Enter(Zombie zombie)
    {
        zombie.PreparePatrol();
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

        zombie.Patrol();
    }

    public void Exit(Zombie zombie)
    {

    }
}
