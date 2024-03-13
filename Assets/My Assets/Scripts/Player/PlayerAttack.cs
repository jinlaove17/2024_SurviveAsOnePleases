using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [field: SerializeField] private Transform weaponPivot { get; set; }
    [field: SerializeField] public Weapon weapon { get; private set; }
    private PlayerInput playerInput { get; set; }
    private Animator playerAnimator { get; set; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon.gameObject.SetActive(!weapon.gameObject.activeSelf);
            playerAnimator.SetLayerWeight(1, (weapon.gameObject.activeSelf) ? 1.0f : 0.0f);
        }

        if (weapon.gameObject.activeSelf)
        {
            if ((playerInput.input & ACTION.ATTACK) > 0)
            {
                weapon.Attack();
            }
            else if ((playerInput.input & ACTION.RELOAD) > 0)
            {
                weapon.Reload();
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if ((weapon == null) || (!weapon.gameObject.activeSelf))
        {
            return;
        }

        // 무기의 기준점 weaponPivot을 3D 모델의 오른쪽 팔꿈치 위로 이동
        weaponPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춤
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, weapon.rightHandle.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, weapon.rightHandle.rotation);

        if (weapon.equipType == Weapon.EQUIP_TYPE.TWO_HAND)
        {
            // IK를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.leftHandle.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.leftHandle.rotation);
        }
    }
}
