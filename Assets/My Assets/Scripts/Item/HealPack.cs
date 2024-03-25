using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : Item
{
    private float healPer { get; set; } // ü�� ȸ�� ����

    private void OnEnable()
    {
        healPer = 10.0f * Random.Range(1.0f, 10.0f);
    }

    protected override void Use(Entity entity)
    {
        entity.Recovery(healPer);

        // ���Ǿ����Ƿ� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
