using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // hitPoint: ���� ����
    // hitNormal: ���� ǥ���� ����
    public void OnDamage(Entity from, float damage, Vector3 hitPoint, Vector3 hitNormal);
}
