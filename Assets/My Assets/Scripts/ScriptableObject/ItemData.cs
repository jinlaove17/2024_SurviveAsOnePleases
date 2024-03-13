using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Basic Item", fileName = "Item Data")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        Ingredient,
        Etc
    }

    public string itemName;
    public ItemType itemType;
    public string itemDesc;
}
