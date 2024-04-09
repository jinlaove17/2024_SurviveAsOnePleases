using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gpm.Ui;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ItemSlotUi : InfiniteScrollItem, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] private Image itemImage { get; set; }
    [field: SerializeField] private Text itemText { get; set; }
    [field: SerializeField] private Text countText { get; set; }
    public ItemData itemData { get; private set; }
    public int itemCount { get; private set; }

    private void OnDisable()
    {
        UiManager.instance.inventoryUi.itemInfoUi.gameObject.SetActive(false);
    }

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        InventoryItemData inventoryItemData = scrollData as InventoryItemData;

        itemData = inventoryItemData.itemData;
        itemText.text = itemData.name;
        //itemImage.sprite = itemData.itemImage.sprite;
        itemCount = inventoryItemData.count;
        countText.text = itemCount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform rectTransform = UiManager.instance.inventoryUi.itemInfoUi.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = Input.mousePosition;
        UiManager.instance.inventoryUi.itemInfoUi.SetItemInfo(itemData);
        UiManager.instance.inventoryUi.itemInfoUi.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UiManager.instance.inventoryUi.itemInfoUi.gameObject.SetActive(false);
    }
}
