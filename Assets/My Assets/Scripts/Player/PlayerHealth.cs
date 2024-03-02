using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Entity
{
    [field: SerializeField] private float maxHp { get; set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        hp = maxHp;
        UiManager.instance.SetPlayerHpBar(hp, maxHp);
    }

    public override void OnDamage(Entity from, float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if ((isDamaged) || (isDead))
        {
            return;
        }

        base.OnDamage(from, damage, hitPoint, hitNormal);
        UiManager.instance.SetPlayerHpBar(maxHp, hp);
    }
}
