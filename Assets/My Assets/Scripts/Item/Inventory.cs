using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] private GameObject panel { get; set; }
    [field: SerializeField] private Transform slotRoot { get; set; }
    [field: SerializeField] private PlayerInput playerInput { get; set; }
    private bool isSelected { get; set; }

    public bool isActive
    {
        get
        {
            return panel.activeSelf;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            panel.SetActive(!panel.activeSelf);

            if (panel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        //if (panel.activeSelf)
        //{
        //    if (isSelected)
        //    {
        //        if (Input.GetMouseButtonUp(0))
        //        {
        //            isSelected = false;
        //        }
        //        else
        //        {
        //            float rotateY = playerInput.rotate.x * 200.0f * Time.deltaTime;
        //            Vector3 newAngle = avatarTransform.eulerAngles;

        //            newAngle.y -= rotateY;
        //            avatarTransform.eulerAngles = newAngle;
        //        }
        //    }
        //    else
        //    {
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            Ray ray = UiManager.instance.canvasCamera.ScreenPointToRay(Input.mousePosition);
        //            RaycastHit rayHit;

        //            if (Physics.Raycast(ray, out rayHit, 100.0f, LayerMask.GetMask("UI")))
        //            {
        //                isSelected = true;
        //            }
        //        }
        //    }
        //}
    }

    public void AcquireItem(Item item, int count = 1)
    {
        ItemSlot itemSlot = null;

        for (int i = 0; i < slotRoot.childCount; ++i)
        {
            GameObject obj = slotRoot.GetChild(i).gameObject;

            if (obj.activeSelf)
            {
                itemSlot = obj.GetComponent<ItemSlot>();

                if (itemSlot.itemData.itemName == item.itemData.itemName)
                {
                    itemSlot.SetItemCount(count);
                    return;
                }
            }
        }

        // 풀링 매니저로부터 슬롯을 가져와 아이템을 등록한다.
        itemSlot = PoolManager.instance.GetObject<ItemSlot>("Item Slot", Vector3.zero);
        itemSlot.AddItem(item.itemData, count);
        itemSlot.gameObject.SetActive(true);
    }
}
