using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [field: SerializeField] public Transform fireTransform { get; private set; } // 탄알 발사 위치
    [field: SerializeField] public ParticleSystem muzzleFlashEffect { get; private set; } // 총구 화염 효과
    [field: SerializeField] public ParticleSystem shellEjectEffect { get; private set; } // 탄피 배출 효과

    public int ammoRemain { get; private set; } // 남은 전체 탄알
    public int magAmmo { get; private set; } // 탄창에 남은 탄알

    protected override void Start()
    {
        base.Start();
        equipType = EQUIP_TYPE.TWO_HAND;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        GunData gunData = (GunData)weaponData;

        ammoRemain = gunData.startAmmo;
        magAmmo = gunData.magCapacity;
    }

    public override void Attack()
    {
        if ((state == STATE.READY) && (Time.time >= lastAttackTime + weaponData.attackSpeed))
        {
            // 마지막 총 발사 시점 갱신
            lastAttackTime = Time.time;

            // 실제 발사 처리 실행
            Fire();
        }
    }

    private void Fire()
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();
        audioSource.PlayOneShot(weaponData.attackClip);
    }
}
