using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Weapon Item", fileName = "Weapon Data")]
public class WeaponData : ItemData
{
    public float attackDamage; // 공격력
    public float attackSpeed; // 공격 속도
    public float attackRange; // 공격 범위

    public AudioClip attackClip; // 공격시 재생할 효과음
}
