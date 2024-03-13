using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Gun Item", fileName = "Gun Data")]
public class GunData : WeaponData
{
    public int startAmmo; // ó���� �־����� ź�˼�
    public int magCapacity; // źâ �뷮
    public float reloadTime; // ������ �ҿ� �ð�

    public AudioClip reloadClip; // �������� ����� ȿ����
}
