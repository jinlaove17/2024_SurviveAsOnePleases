using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    public string weaponName;

    public float attackDamage; // ���ݷ�
    public float attackRange; // ���� ����
    public float attackSpeed; // ���� �ӵ�

    public AudioClip attackClip; // ���ݽ� ����� ȿ����
}
