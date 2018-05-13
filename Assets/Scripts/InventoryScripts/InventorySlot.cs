using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private ItemButton itemButton;

    private Item item;
    private bool isEmpty;

    public void SetItem(Item addedItem)
    {
        item = addedItem;
        isEmpty = false;
        itemButton.name = item.itemName;

        var img = itemButton.GetComponent<Image>();
        img.type = Image.Type.Simple;
        img.sprite = item.GetItemIcon;
        img.preserveAspect = true;
    }

    public void EquipItem(Transform newParent)
    {
        isEmpty = true;
        itemButton.transform.SetParent(newParent, false);
    }

    public void DequipItem()
    {
        isEmpty = false;
        itemButton.transform.SetParent(transform, false);
    }

    public Item GetItem
    {
        get { return item; }
    }

    public GameObject GetItemButton
    {
        get { return itemButton.gameObject; }
    }
}
