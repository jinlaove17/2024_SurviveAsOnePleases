using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : Item
{
    private float healPer { get; set; } // 체력 회복 비율

    private void OnEnable()
    {
        healPer = 10.0f * Random.Range(1.0f, 10.0f);
    }

    protected override void Use(Entity entity)
    {
        entity.Recovery(healPer);

        // 사용되었으므로 비활성화
        gameObject.SetActive(false);
    }
}
