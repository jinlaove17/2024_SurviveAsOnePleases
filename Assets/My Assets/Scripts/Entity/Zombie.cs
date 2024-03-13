using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Entity
{
    [field: SerializeField] public ZombieData zombieData { get; private set; }
    public bool isAttack { get; protected set; }
    public float lastAttackTime { get; protected set; }
    public Animator animator { get; private set; }
    public NavMeshAgent navMeshAgent { get; private set; }
    public FiniteStateMachine<Zombie> fsm { get; private set; }
    protected Coroutine patrolRoutine { get; set; }
    public Entity target { get; private set; }
    private Vector3[] pathCorners { get; set; } // Debug

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        fsm = new FiniteStateMachine<Zombie>();
        onDeath += () => Inactive(5.0f);
        onDeath += () => UiManager.instance.enemyStatusUi.gameObject.SetActive(false); // 좀비가 죽으면 상태 UI를 비활성화 한다.
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        hp = zombieData.maxHp;
        isAttack = false;
        lastAttackTime = Time.time;
        navMeshAgent.speed = zombieData.moveSpeed;
        navMeshAgent.enabled = true;
        fsm.ChangeState(this, ZombiePatrolState.instance);
        target = null;
    }

    private void Update()
    {
        fsm.Update(this);
    }

    private void FixedUpdate()
    {
        fsm.FixedUpdate(this);
    }

    private void OnDrawGizmos()
    {
        if ((pathCorners == null) || (pathCorners.Length <= 0))
        {
            return;
        }

        Vector3 prevPosition = pathCorners[0];

        for (int i = 1; i < pathCorners.Length; ++i)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(prevPosition, pathCorners[i]);
            Gizmos.DrawSphere(pathCorners[i], 0.1f);
            prevPosition = pathCorners[i];
        }
    }

    public override void OnDamage(Entity from, float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if ((isDamaged) || (isDead))
        {
            return;
        }

        float prevHp = hp;

        base.OnDamage(from, damage, hitPoint, hitNormal);

        if (!isDead)
        {
            if (target == null)
            {
                target = from;
                fsm.ChangeState(this, ZombieChaseState.instance);
            }
            
            audioSource.PlayOneShot(zombieData.hitClip);
            UiManager.instance.enemyStatusUi.gameObject.SetActive(true);
            UiManager.instance.enemyStatusUi.hpBar.UpdateHpBar(prevHp / zombieData.maxHp, zombieData.maxHp, hp);
        }
    }

    public override void Recovery(float healPer)
    {
        // 이미 사망한 경우 체력을 회복할 수 없음
        if (isDead)
        {
            return;
        }

        base.Recovery(healPer);

        // 회복이후 hp가 최대 hp를 초과하지 않도록 보정
        hp = Mathf.Min(hp, zombieData.maxHp);
    }

    public virtual void OnAttack()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + zombieData.attackRange.center, 2.0f * zombieData.attackRange.size, transform.rotation, LayerMask.GetMask("Player"));

        for (int i = 0; i < colliders.Length; ++i)
        {
            Entity entity = colliders[i].GetComponent<Entity>();

            if ((entity != null) && (entity == target))
            {
                // 상대방의 피격 위치와 피격 방향을 근삿값으로 계산
                Vector3 hitPoint = colliders[i].ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - entity.transform.position;

                entity.OnDamage(this, zombieData.attackDamage, hitPoint, hitNormal);
                break;
            }
        }
    }

    public virtual void OnAttackExit()
    {
        isAttack = false;
    }

    public override void Die()
    {
        base.Die();
        StopCoroutine(patrolRoutine);
        collider.enabled = false;
        navMeshAgent.enabled = false;
        animator.SetTrigger("Die");
        audioSource.PlayOneShot(zombieData.deathClip);
    }

    public void PreparePatrol()
    {
        navMeshAgent.speed = zombieData.moveSpeed;
        patrolRoutine = StartCoroutine(PatrolRoutine(Random.Range(2.0f, 4.0f)));
    }

    private IEnumerator PatrolRoutine(float duration)
    {
        // 현재 위치를 중심으로 반지름이 zombieData.senseRadius인 구 안에서의 랜덤한 위치 하나를 저장
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = Random.insideUnitSphere * zombieData.senseRadius + transform.position;
        NavMeshHit navMeshHit;

        // zombieData.senseRadius 반경 안에서, randomPos에 가장 가까운 내비메시 위의 한 점을 찾아 목적지로 설정
        randomPos.y = 0.0f;

        while (!NavMesh.SamplePosition(randomPos, out navMeshHit, zombieData.senseRadius, NavMesh.AllAreas)) { }

        NavMeshPath navMeshPath = new NavMeshPath();

        navMeshAgent.isStopped = true;
        navMeshAgent.CalculatePath(navMeshHit.position, navMeshPath);
        navMeshAgent.SetPath(navMeshPath);
        animator.SetBool("Walk", false);
        pathCorners = navMeshPath.corners; // Debug

        yield return new WaitForSeconds(duration);

        navMeshAgent.isStopped = false;
        animator.SetBool("Walk", true);
    }

    public void Patrol()
    {
        if (target == null)
        {
            FindTarget();

            if (target == null)
            {
                if (navMeshAgent.remainingDistance <= 0.05f)
                {
                    PreparePatrol();
                }
            }
        }
        else
        {
            fsm.ChangeState(this, ZombieChaseState.instance);
        }
    }

    private void FindTarget()
    {
        // 구를 그렸을 때 구와 겹치는 모든 콜라이더를 가져온다.
        Collider[] colliders = Physics.OverlapSphere(transform.position, zombieData.senseRadius, LayerMask.GetMask("Player"));

        // 모든 콜라이더를 순회하면서 살아 있는 Entity 찾기
        for (int i = 0; i < colliders.Length; ++i)
        {
            // 콜라이더로부터 Entity 컴포넌트 가져오기
            Entity entity = colliders[i].GetComponent<Entity>();

            // Entity 컴포넌트가 존재하며, 해당 객체가 살아 있다면
            if ((entity != null) && (!entity.isDead))
            {
                // 추적 대상을 해당 Entity로 변경
                target = entity;
                break;
            }
        }
    }

    public void PrepareChase()
    {
        StopCoroutine(patrolRoutine);
        isAttack = false;
        lastAttackTime = Time.time;
        navMeshAgent.speed = 2.0f;
    }

    public void Chase()
    {
        if (isAttack)
        {
            return;
        }
        
        if (IsInAttackRange())
        {
            if (Time.time >= lastAttackTime + zombieData.attackSpeed)
            {
                isAttack = true;
                lastAttackTime = Time.time;
                transform.LookAt(target.transform);
                navMeshAgent.isStopped = true;
                animator.SetBool("Chase", false);
                animator.SetFloat("Attack Speed", 1.0f / zombieData.attackSpeed);
                animator.SetTrigger((Random.value < 0.5f) ? "Attack Left" : "Attack Right");
            }
        }
        else
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.transform.position);
            animator.SetBool("Chase", true);
        }
    }

    private bool IsInAttackRange()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + zombieData.attackRange.center, 2.0f * zombieData.attackRange.size, transform.rotation, LayerMask.GetMask("Player"));

        for (int i = 0; i < colliders.Length; ++i)
        {
            Entity entity = colliders[i].GetComponent<Entity>();

            if ((entity != null) && (entity == target))
            {
                return true;
            }
        }

        return false;
    }
}
