using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum STATE
    {
        NONE,
        READY,
        WAIT
    }

    public enum EQUIP_TYPE
    {
        ONE_HAND,
        TWO_HAND
    }

    [field: SerializeField] public WeaponData weaponData { get; private set; }
    [field: SerializeField] public Transform leftHandle { get; private set; }
    [field: SerializeField] public Transform rightHandle { get; private set; }
    [field: SerializeField] public Entity owner { get; private set; }
    public STATE state { get; protected set; }
    public EQUIP_TYPE equipType { get; protected set; }
    public float lastAttackTime { get; protected set; }
    public AudioSource audioSource { get; private set; }
    public CinemachineImpulseSource cinemachineImpulseSource { get; private set; }

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    protected virtual void OnEnable()
    {
        state = STATE.READY;
        lastAttackTime = 0.0f;
        
        // 무기가 활성화 되면 플레이어 애니메이터의 Upper Body Layer의 가중치를 1로 만든다.
        owner.GetComponent<Animator>().SetLayerWeight(1, 1.0f);
    }
    
    protected virtual void OnDisable()
    {
        // 무기가 비활성화 되면 플레이어 애니메이터의 Upper Body Layer의 가중치를 0으로 만든다.
        owner.GetComponent<Animator>().SetLayerWeight(1, 0.0f);
    }

    public virtual void Attack()
    {

    }

    public virtual void Reload()
    {

    }
}
