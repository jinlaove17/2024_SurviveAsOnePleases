using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class ACTION
{
    public const int RUN      = 0x01;
    public const int ATTACK   = 0x02;
    public const int RELOAD   = 0x04;
    public const int AROUND   = 0x08;
    public const int INTERACT = 0x10;
}

public class PlayerInput : MonoBehaviour
{
    public enum AIM_STATE
    {
        NONE,
        ZOOM_IN,
        ZOOM,
        ZOOM_OUT
    }

    public Vector3 move { get; private set; }
    public Vector2 rotate { get; private set; }
    public int input { get; private set; }
    public AIM_STATE aimState { get; private set; }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        move = Vector3.zero;
        rotate = Vector2.zero;
        input = 0;
        aimState = AIM_STATE.NONE;
    }

    private void Update()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        rotate = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        input = 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            input |= ACTION.RUN;
        }

        if (Input.GetButton("Fire1"))
        {
            input |= ACTION.ATTACK;
        }

        if (Input.GetButtonDown("Reload"))
        {
            input |= ACTION.RELOAD;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            input |= ACTION.INTERACT;
        }

        if (Input.GetMouseButtonDown(1))
        {
            aimState = AIM_STATE.ZOOM_IN;
        }
        else if (Input.GetMouseButton(1))
        {
            aimState = AIM_STATE.ZOOM;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            aimState = AIM_STATE.ZOOM_OUT;
        }
        else
        {
            aimState = AIM_STATE.NONE;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            // 공격, 줌 중에는 둘러보기 기능을 사용할 수 없음
            if (((input & ACTION.ATTACK) == 0) && (aimState == AIM_STATE.NONE))
            {
                input |= ACTION.AROUND;
            }
        }
    }
}
