using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [field: SerializeField] public Transform fireTransform { get; private set; } // ź�� �߻� ��ġ
    [field: SerializeField] public ParticleSystem muzzleFlashEffect { get; private set; } // �ѱ� ȭ�� ȿ��
    [field: SerializeField] public ParticleSystem shellEjectEffect { get; private set; } // ź�� ���� ȿ��

    public int ammoRemain { get; private set; } // ���� ��ü ź��
    public int magAmmo { get; private set; } // źâ�� ���� ź��

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
            // ������ �� �߻� ���� ����
            lastAttackTime = Time.time;

            // ���� �߻� ó�� ����
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
