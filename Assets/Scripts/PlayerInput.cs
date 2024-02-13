using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float moveHorizontal { get; private set; }
    public float moveVertical { get; private set; }
    public Vector2 rotate { get; private set; }
    public bool attack { get; private set; }
    public bool reload { get; private set; }
    public bool around { get; private set; }
    public int aim { get; private set; } // 0: None/Down(~ing), 1: Down, 2: Up

    private void OnEnable()
    {
        moveHorizontal = 0.0f;
        moveVertical = 0.0f;
        rotate = Vector2.zero;
    }

    private void Update()
    {
        bool isRun = Input.GetKey(KeyCode.LeftShift);

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        if (!isRun)
        {
            moveHorizontal *= 0.25f;
            moveVertical *= 0.25f;
        }

        rotate = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        attack = Input.GetButton("Fire1");
        reload = Input.GetButtonDown("Reload");

        if (Input.GetMouseButtonDown(1))
        {
            aim = 1;
        }
        else if (Input.GetMouseButton(1))
        {
            aim = 2;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            aim = 3;
        }
        else
        {
            aim = 0;
        }

        // 공격, 줌 중에는 둘러보기 기능을 사용할 수 없음
        around = (((attack) || (aim != 0)) ? false : Input.GetKey(KeyCode.LeftAlt));
    }
}
