using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Gun Item", fileName = "Gun Data")]
public class GunData : WeaponData
{
    public int startAmmo; // 처음에 주어지는 탄알수
    public int magCapacity; // 탄창 용량
    public float reloadTime; // 재장전 소요 시간

    public AudioClip reloadClip; // 재장전시 재생할 효과음
}
