using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : Item
{
    private float ammo { get; set; } // ���� źâ��

    private void OnEnable()
    {
        ammo = 10.0f * Random.Range(3.0f, 10.0f);
    }

    protected override void Use(Entity entity)
    {
        Debug.Log("AmmoPack �������� ȹ���Ͽ����ϴ�.");

        //PlayerAttack playerAttack = entity.GetComponent<PlayerAttack>();

        //if ((playerAttack != null) && (playerAttack.weapon != null))
        //{

        //}

        // ���Ǿ����Ƿ� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
