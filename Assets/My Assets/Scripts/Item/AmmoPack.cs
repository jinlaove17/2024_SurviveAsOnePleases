using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : Item
{
    private float ammo { get; set; } // 충전 탄창율

    private void OnEnable()
    {
        ammo = 10.0f * Random.Range(3.0f, 10.0f);
    }

    protected override void Use(Entity entity)
    {
        Debug.Log("AmmoPack 아이템을 획득하였습니다.");

        //PlayerAttack playerAttack = entity.GetComponent<PlayerAttack>();

        //if ((playerAttack != null) && (playerAttack.weapon != null))
        //{

        //}

        // 사용되었으므로 비활성화
        gameObject.SetActive(false);
    }
}
