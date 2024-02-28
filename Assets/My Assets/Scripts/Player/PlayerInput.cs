using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public AIM_STATE aimState { get; private set; }
    public bool isRun { get; private set; }
    public bool isAttack { get; private set; }
    public bool isReload { get; private set; }
    public bool isAround { get; private set; }

    private void OnEnable()
    {
        move = Vector3.zero;
        rotate = Vector2.zero;
        aimState = AIM_STATE.NONE;
        isRun = false;
        isAttack = false;
        isReload = false;
        isAround = false;
    }

    private void Update()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        rotate = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        isRun = Input.GetKey(KeyCode.LeftShift);
        isAttack = Input.GetButton("Fire1");
        isReload = Input.GetButtonDown("Reload");

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

        // 공격, 줌 중에는 둘러보기 기능을 사용할 수 없음
        isAround = (((isAttack) || (aimState != AIM_STATE.NONE)) ? false : Input.GetKey(KeyCode.LeftAlt));
    }
}
