using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AttackRange
{
    public Vector3 center;
    public Vector3 size;
}

[CreateAssetMenu(menuName = "ScriptableObject/Zombie", fileName = "Zombie Data")]
public class ZombieData : ScriptableObject
{
    public string zombieName;

    public float maxHp; // 최대 체력
    public float moveSpeed; // 이동 속도
    public float attackDamage; // 공격력
    public float attackSpeed; // 공격 속도
    public AttackRange attackRange; // 공격 범위
    public float senseRadius; // 플레이어 감지 범위

    public AudioClip hitClip; // 피격 효과음
    public AudioClip deathClip; // 사망 효과음
}
