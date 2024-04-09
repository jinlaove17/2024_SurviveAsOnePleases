using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUi : MonoBehaviour
{
    [field: SerializeField] private Text itemName { get; set; }
    [field: SerializeField] private Image itemImage { get; set; }
    [field: SerializeField] private Text itemDesc { get; set; }

    public void SetItemInfo(ItemData itemData)
    {
        itemName.text = itemData.name;
        itemImage = itemData.itemImage;
        itemDesc.text = itemData.itemDesc;
    }
}
