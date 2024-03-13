using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField] private float moveSpeed { get; set; }
    [field: SerializeField] private float rotateSpeed { get; set; }
    [field: SerializeField] private Vector2 verticalAngle { get; set; } // x: Min, y: Max 
    [field: SerializeField] private float smoothness { get; set; }
    [field: SerializeField] private Transform followTarget { get; set; }
    [field: SerializeField] private CinemachineVirtualCamera followCamera { get; set; }
    [field: SerializeField] private CinemachineVirtualCamera aimCamera { get; set; }
    private Vector3 shift { get; set; }
    private PlayerInput playerInput { get; set; }
    private PlayerHealth playerHealth { get; set; }
    private Rigidbody playerRigidBody { get; set; }
    private Animator playerAnimator { get; set; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerHealth = GetComponent<PlayerHealth>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerHealth.isDead)
        {
            return;
        }

        UpdateAnimation();
        UpdateCamera();
    }

    private void FixedUpdate()
    {
        Rotate();

        if ((playerHealth.isDead) || (playerHealth.isInteract))
        {
            return;
        }

        Move();
    }

    private void UpdateAnimation()
    {
        // 입력값에 따라 애니메이터의 Move 파라미터 값을 변경
        Vector3 move = new Vector3(playerInput.move.x, 0.0f, playerInput.move.z);

        // 앞으로만 뛸 수 있다.
        if (((playerInput.input & ACTION.RUN) > 0) && (move.z > 0.0f))
        {
            move *= 2.0f;
        }

        move.x = Mathf.Lerp(shift.x, move.x, smoothness * Time.deltaTime);
        move.z = Mathf.Lerp(shift.z, move.z, smoothness * Time.deltaTime);
        shift = move;
        playerAnimator.SetFloat("PosX", shift.x);
        playerAnimator.SetFloat("PosY", shift.z);
    }

    private void UpdateCamera()
    {
        switch (playerInput.aimState)
        {
            case PlayerInput.AIM_STATE.ZOOM_IN: // GetMouseButtonDown(1)
                followCamera.gameObject.SetActive(false);
                aimCamera.gameObject.SetActive(true);
                verticalAngle /= 2.0f;
                break;
            case PlayerInput.AIM_STATE.ZOOM_OUT: // GetMouseButtonUp(1)
                followCamera.gameObject.SetActive(true);
                aimCamera.gameObject.SetActive(false);
                verticalAngle *= 2.0f;
                break;
        }
    }

    private void Move()
    {
        float mag = shift.magnitude;

        if (mag > 0.0f)
        {
            Vector3 adjustedShift = shift;

            if ((playerInput.input & ACTION.RUN) > 0)
            {
                if (mag > 2.0f)
                {
                    adjustedShift = 2.0f * shift.normalized;
                }
            }
            else
            {
                if (mag > 1.0f)
                {
                    adjustedShift = shift.normalized;
                }
            }

            Vector3 moveDistance = (adjustedShift.x * transform.right + adjustedShift.z * transform.forward) * moveSpeed * Time.deltaTime;
            Vector3 newPosition = playerRigidBody.position + moveDistance;

            playerRigidBody.MovePosition(newPosition);
        }
    }

    private void Rotate()
    {
        Vector2 rotateAngle = playerInput.rotate * rotateSpeed * Time.deltaTime;
        Vector3 newAngle = Vector3.zero;

        // eulerAngles는 0 ~ 360도의 값을 반환하기 때문에 180도를 넘어가는 값에 대해서는 360도를 빼주어야 한다.
        newAngle.x = followTarget.localEulerAngles.x - rotateAngle.y;
        newAngle.x = Mathf.Clamp((newAngle.x >= 180.0f) ? newAngle.x - 360.0f : newAngle.x, verticalAngle.x, verticalAngle.y);
        newAngle.y = followTarget.localEulerAngles.y + rotateAngle.x;
        followTarget.localEulerAngles = aimCamera.transform.localEulerAngles = new Vector3(newAngle.x, newAngle.y, 0.0f);

        if ((playerInput.input & ACTION.AROUND) == 0)
        {
            transform.forward = new Vector3(followTarget.forward.x, 0.0f, followTarget.forward.z).normalized;
            followTarget.localEulerAngles = aimCamera.transform.localEulerAngles = new Vector3(followTarget.localEulerAngles.x, 0.0f, 0.0f);
        }
    }
}
