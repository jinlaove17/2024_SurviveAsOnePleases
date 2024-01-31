using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerInput playerInput;

    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon.gameObject.SetActive(!weapon.gameObject.activeSelf);
            playerAnimator.SetLayerWeight(1, (weapon.gameObject.activeSelf) ? 1.0f : 0.0f);
        }

        if (playerInput.attack)
        {
            weapon.Attack();
        }
        else if (playerInput.reload)
        {
            weapon.Reload();
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if ((weapon == null) || (!weapon.gameObject.activeSelf))
        {
            return;
        }

        //// IK�� ����Ͽ� �������� ��ġ�� ȸ���� ���� ������ �����̿� ����
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, weapon.rightHandle.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, weapon.rightHandle.rotation);

        if (weapon.equipType == Weapon.EQUIP_TYPE.TWO_HAND)
        {
            // IK�� ����Ͽ� �޼��� ��ġ�� ȸ���� ���� ���� �����̿� ����
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.leftHandle.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.leftHandle.rotation);
        }
    }
}
