using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [field: SerializeField] public Transform fireTransform { get; private set; } // ź�� �߻� ��ġ
    [field: SerializeField] public ParticleSystem muzzleFlashEffect { get; private set; } // �ѱ� ȭ�� ȿ��
    [field: SerializeField] public ParticleSystem shellEjectEffect { get; private set; } // ź�� ���� ȿ��
    public int remainAmmo { get; private set; } // ���� ��ü ź��
    public int magAmmo { get; private set; } // źâ�� ���� ź��

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
            // ������ �� �߻� ���� ����
            lastAttackTime = Time.time;

            // ���� �߻� ó�� ����
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

        // ä���� �� ź�˼�
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
                // ���̰� �ٸ� ��ü�� �浹���� ���� ���, ź���� �ִ� �����Ÿ����� ���ư��� ���� ��ġ�� �浹 ��ġ�� ���
                hitPosition = fireTransform.position + fireTransform.forward * weaponData.attackRange;
            }
        }

        muzzleFlashEffect.Play();
        shellEjectEffect.Play();
        audioSource.PlayOneShot(weaponData.attackClip);
    }
}
