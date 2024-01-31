using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxVertAngle;
    [SerializeField] private float minVertAngle;
    [SerializeField] private Transform followTarget;
    [SerializeField] private CinemachineVirtualCamera followCamera;
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        switch (playerInput.aim)
        {
            case 1:
                followCamera.gameObject.SetActive(false);
                aimCamera.gameObject.SetActive(true);
                break;
            case 2:
                aimCamera.gameObject.SetActive(false);
                followCamera.gameObject.SetActive(true);
                break;
        }
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();

        // 입력값에 따라 애니메이터의 Move 파라미터 값을 변경
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
            Vector3 moveDir = (playerInput.moveHorizontal * transform.right + playerInput.moveVertical * transform.forward).normalized;
            Vector3 moveDistance = moveSpeed * moveDir * Time.deltaTime;
            Vector3 newPosition = playerRigidbody.position + moveDistance;

            playerRigidbody.MovePosition(newPosition);
        }
    }

    private void Rotate()
    {
        Vector2 rotateAngle = rotateSpeed * playerInput.rotate * Time.deltaTime;
        Vector3 newAngle = Vector3.zero;
        
        // eulerAngles는 0 ~ 360도의 값을 반환하기 때문에 180도를 넘어가는 값에 대해서는 360도를 빼주어야 한다.
        newAngle.x = followTarget.localEulerAngles.x - rotateAngle.y;
        newAngle.x = Mathf.Clamp((newAngle.x >= 180.0f) ? newAngle.x - 360.0f : newAngle.x, minVertAngle, maxVertAngle);
        newAngle.y = followTarget.localEulerAngles.y + rotateAngle.x;
        followTarget.localEulerAngles = aimCamera.transform.localEulerAngles = new Vector3(newAngle.x, newAngle.y, 0.0f);

        if (!playerInput.around)
        {
            transform.forward = new Vector3(followTarget.forward.x, 0.0f, followTarget.forward.z).normalized;
            followTarget.localEulerAngles = aimCamera.transform.localEulerAngles = new Vector3(followTarget.localEulerAngles.x, 0.0f, 0.0f);
        }
    }
}
