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

    public float maxHp; // �ִ� ü��
    public float moveSpeed; // �̵� �ӵ�
    public float attackDamage; // ���ݷ�
    public float attackSpeed; // ���� �ӵ�
    public AttackRange attackRange; // ���� ����
    public float senseRadius; // �÷��̾� ���� ����

    public AudioClip hitClip; // �ǰ� ȿ����
    public AudioClip deathClip; // ��� ȿ����
}
