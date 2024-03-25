using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Basic Item", fileName = "Item Data")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Equipment,
        Consumable,
        Ingredient,
        Etc
    }

    public string itemName;
    public ItemType itemType;
    public Image itemImage;
    public string itemDesc;
}
