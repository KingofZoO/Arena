using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventorySlot inventorySlot;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    public void OnItemClick()
    {
        inventoryManager.TakeItem(inventorySlot);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryManager.GetItemPanel.SetValues(inventorySlot.GetItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryManager.GetItemPanel.ResetValues();
    }
}
