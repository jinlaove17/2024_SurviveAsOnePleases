using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [field: SerializeField] private Image itemImage { get; set; }
    [field: SerializeField] private Text itemText { get; set; }
    [field: SerializeField] private Text countText { get; set; }
    public ItemData itemData { get; private set; }
    public int itemCount { get; private set; }

    public void AddItem(ItemData item, int cnt = 1)
    {
        itemData = item;
        itemImage = itemData.itemImage;
        itemText.text = itemData.itemName;
        itemCount = cnt;
        countText.text = itemCount.ToString();
    }

    public void SetItemCount(int cnt)
    {
        itemCount += cnt;
        countText.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
