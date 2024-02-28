using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // hitPoint: 맞은 지점
    // hitNormal: 맞은 표면의 방향
    public void OnDamage(Entity from, float damage, Vector3 hitPoint, Vector3 hitNormal);
}
