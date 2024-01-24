using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float moveHorizontal { get; private set; }
    public float moveVertical { get; private set; }
    public Vector2 rotate {  get; private set; }

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
            moveHorizontal *= 0.5f;
            moveVertical *= 0.5f;
        }

        rotate = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
