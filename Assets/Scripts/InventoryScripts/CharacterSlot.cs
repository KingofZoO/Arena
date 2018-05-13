using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public ItemType[] itemType;
    [HideInInspector] public bool isEmpty = true;
    private InventorySlot item;

    public void EquipItem(InventorySlot equipedItem)
    {
        item = equipedItem;
        item.EquipItem(transform);
        isEmpty = false;
    }

    public void DequipItem()
    {
        if (item != null)
        {
            item.DequipItem();
            item = null;
            isEmpty = true;
        }
    }

    public InventorySlot GetItem
    {
        get { return item; }
    }
}
