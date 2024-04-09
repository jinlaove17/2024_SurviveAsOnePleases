using Gpm.Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData : InfiniteScrollData
{
    public ItemData itemData;
    public int count;
}

public class InventoryUi : MonoBehaviour
{
    [field: SerializeField] private GameObject panel { get; set; }
    [field: SerializeField] public ItemInfoUi itemInfoUi { get; private set; }
    [field: SerializeField] private InfiniteScroll aroundItemScroll { get; set; }
    [field: SerializeField] private InfiniteScroll ownedItemScroll { get; set; }

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
    }

    public void InsertAroundItem(Item item, int count = 1)
    {
        InventoryItemData inventoryItemData = null;
        var itemList = aroundItemScroll.GetItemList();

        for (int i = 0; i < itemList.Count; ++i)
        {
            inventoryItemData = itemList[i] as InventoryItemData;

            if (inventoryItemData.itemData.name == item.itemData.name)
            {
                inventoryItemData.count += count;
                aroundItemScroll.UpdateData(inventoryItemData);
                return;
            }
        }

        inventoryItemData = new InventoryItemData();
        inventoryItemData.itemData = item.itemData;
        inventoryItemData.count = count;
        aroundItemScroll.InsertData(inventoryItemData);
    }

    public void DeleteAroundItem(Item item, int count = 1)
    {
        InventoryItemData inventoryItemData = null;
        var itemList = aroundItemScroll.GetItemList();

        for (int i = 0; i < itemList.Count; ++i)
        {
            inventoryItemData = itemList[i] as InventoryItemData;

            if (inventoryItemData.itemData.name == item.itemData.name)
            {
                inventoryItemData.count -= count;

                if (inventoryItemData.count == 0)
                {
                    aroundItemScroll.RemoveData(inventoryItemData, true);
                }

                return;
            }
        }
    }

    public void AcquireItem(Item item, int count = 1)
    {
        InventoryItemData inventoryItemData = null;
        var itemList = ownedItemScroll.GetItemList();

        for (int i = 0; i < itemList.Count; ++i)
        {
            inventoryItemData = itemList[i] as InventoryItemData;

            if (inventoryItemData.itemData.name == item.itemData.name)
            {
                inventoryItemData.count += count;
                ownedItemScroll.UpdateData(inventoryItemData);
                return;
            }
        }

        inventoryItemData = new InventoryItemData();
        inventoryItemData.itemData = item.itemData;
        inventoryItemData.count = count;
        ownedItemScroll.InsertData(inventoryItemData);
    }
}
