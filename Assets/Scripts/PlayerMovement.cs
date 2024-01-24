using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private float maxVertAngle;
    [SerializeField]
    private float minVertAngle;

    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    [SerializeField]
    private Transform followTarget;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();

        // �Է°��� ���� �ִϸ������� Move �Ķ���� ���� ����
        if (Mathf.Abs(playerInput.moveVertical) > 0.0f)
        {
            playerAnimator.SetFloat("Move", playerInput.moveVertical);
        }
        else
        {
            playerAnimator.SetFloat("Move", Mathf.Abs(playerInput.moveHorizontal));
        }
    }

    private void Move()
    {
        if ((Mathf.Abs(playerInput.moveHorizontal) > 0.0f) || (Mathf.Abs(playerInput.moveVertical) > 0.0f))
        {
            Vector3 lookForward = new Vector3(followTarget.forward.x, 0.0f, followTarget.forward.z).normalized;
            Vector3 lookRight = new Vector3(followTarget.right.x, 0.0f, followTarget.right.z).normalized;
            Vector3 moveDir = (playerInput.moveHorizontal * lookRight + playerInput.moveVertical * lookForward).normalized;
            Vector3 moveDistance = moveSpeed * moveDir * Time.deltaTime;
            Vector3 newPosition = playerRigidbody.position + moveDistance;

            playerRigidbody.MovePosition(newPosition);
            transform.forward = lookForward;
            followTarget.localEulerAngles = new Vector3(followTarget.localEulerAngles.x, 0.0f, 0.0f);
        }
    }

    private void Rotate()
    {
        Vector2 rotateAngle = rotateSpeed * playerInput.rotate * Time.deltaTime;
        Vector3 newAngle = Vector3.zero;

        // eulerAngles�� 0 ~ 360���� ���� ��ȯ�ϱ� ������ 180���� �Ѿ�� ���� ���ؼ��� 360���� ���־�� �Ѵ�.
        newAngle.x = followTarget.localEulerAngles.x - rotateAngle.y;
        newAngle.x = Mathf.Clamp((newAngle.x >= 180.0f) ? newAngle.x - 360.0f : newAngle.x, -20.0f, 20.0f);
        newAngle.y = followTarget.localEulerAngles.y + rotateAngle.x;
        followTarget.localEulerAngles = new Vector3(newAngle.x, newAngle.y, 0.0f);
    }
}
