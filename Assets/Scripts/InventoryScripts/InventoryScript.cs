using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] private InventorySlot slotImage;
    [SerializeField] private int slotsPerHero = 4;
    private int slotCount;

    private Item[] itemsArr;
    private InventorySlot[] slotArr;

    private void Awake()
    {
        slotCount = HeroDataContainer.Instance.HeroCount * slotsPerHero;
        slotArr = new InventorySlot[slotCount];
        itemsArr = Resources.LoadAll<Item>("Items");

        GenerateInventory();
    }

    private void GenerateInventory()
    {
        for (int i = 0; i < slotCount; i++)
        {
            var slot = Instantiate(slotImage, transform);
            slot.name = "slot" + "(" + i + ")";
            slot.SetItem(itemsArr[Random.Range(0, itemsArr.Length)]);

            slotArr[i] = slot;
        }
    }

    public InventorySlot[] GetInventorySlots
    {
        get { return slotArr; }
    }
}
