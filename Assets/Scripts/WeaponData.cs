using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    public string weaponName;

    public float attackDamage; // 공격력
    public float attackRange; // 공격 범위
    public float attackSpeed; // 공격 속도

    public AudioClip attackClip; // 공격시 재생할 효과음
}
