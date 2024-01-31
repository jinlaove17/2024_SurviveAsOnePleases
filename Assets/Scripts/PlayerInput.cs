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
        around = Input.GetKey(KeyCode.LeftAlt);

        if (Input.GetMouseButtonDown(1))
        {
            aim = 1;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            aim = 2;
        }
        else
        {
            aim = 0;
        }
    }
}
