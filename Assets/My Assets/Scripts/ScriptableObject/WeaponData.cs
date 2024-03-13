using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Weapon Item", fileName = "Weapon Data")]
public class WeaponData : ItemData
{
    public float attackDamage; // ���ݷ�
    public float attackSpeed; // ���� �ӵ�
    public float attackRange; // ���� ����

    public AudioClip attackClip; // ���ݽ� ����� ȿ����
}
