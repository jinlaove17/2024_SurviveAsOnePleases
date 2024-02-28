using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum STATE
    {
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

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnEnable()
    {
        state = STATE.READY;
        lastAttackTime = 0.0f;
    }

    public virtual void Attack()
    {

    }

    public virtual void Reload()
    {

    }
}
