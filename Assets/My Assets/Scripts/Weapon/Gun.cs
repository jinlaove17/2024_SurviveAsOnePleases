using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [field: SerializeField] public Transform fireTransform { get; private set; } // 탄알 발사 위치
    [field: SerializeField] public ParticleSystem muzzleFlashEffect { get; private set; } // 총구 화염 효과
    [field: SerializeField] public ParticleSystem shellEjectEffect { get; private set; } // 탄피 배출 효과
    public int remainAmmo { get; private set; } // 남은 전체 탄알
    public int magAmmo { get; private set; } // 탄창에 남은 탄알

    protected override void Awake()
    {
        base.Awake();
        equipType = EQUIP_TYPE.TWO_HAND;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        GunData gunData = (GunData)weaponData;

        remainAmmo = gunData.startAmmo;
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

    public override void Reload()
    {
        GunData gunData = (GunData)weaponData;

        if ((state == STATE.WAIT) || (remainAmmo <= 0) || (magAmmo >= (gunData.magCapacity)))
        {
            return;
        }

        StartCoroutine(ReloadRoutine(gunData.reloadTime));
    }

    private IEnumerator ReloadRoutine(float duration)
    {
        state = STATE.WAIT;

        GunData gunData = (GunData)weaponData;

        if (gunData.reloadClip != null)
        {
            audioSource.PlayOneShot(gunData.reloadClip);
        }

        yield return new WaitForSeconds(duration);

        // 채워야 할 탄알수
        int ammoToFill = gunData.magCapacity - magAmmo;

        if (ammoToFill >= remainAmmo)
        {
            ammoToFill = remainAmmo;
        }

        magAmmo += ammoToFill;
        remainAmmo -= ammoToFill;
        state = STATE.READY;
    }

    private void Fire()
    {
        RaycastHit rayHit;
        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHit, weaponData.attackRange, LayerMask.GetMask("Enemy")))
        {
            IDamageable target = rayHit.collider.gameObject.GetComponent<IDamageable>();

            if (target != null)
            {
                target.OnDamage(owner, weaponData.attackDamage, rayHit.point, rayHit.normal);
                hitPosition = rayHit.point;
            }
            else
            {
                // 레이가 다른 물체와 충돌하지 않은 경우, 탄알이 최대 사정거리까지 날아갔을 때의 위치를 충돌 위치로 사용
                hitPosition = fireTransform.position + fireTransform.forward * weaponData.attackRange;
            }
        }

        muzzleFlashEffect.Play();
        shellEjectEffect.Play();
        audioSource.PlayOneShot(weaponData.attackClip);
    }
}
