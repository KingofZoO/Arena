using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private CharacterSlotsManager characterPanel;
    [SerializeField] private InventoryScript inventoryScript;
    [SerializeField] private StatsPanel statsPanel;
    [SerializeField] private ItemPanel itemPanel;

    private CharacterSlot[] charSlots;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        charSlots = characterPanel.GetCharacterSlots;
    }

    public void TakeItem(InventorySlot item)
    {
        bool itemEquiped = false;
        for (int i = 0; i < charSlots.Length; i++)
        {
            var slot = charSlots[i];
            if (CheckTypes(item.GetItem, slot))
            {
                if (item == slot.GetItem)
                {
                    slot.DequipItem();
                    statsPanel.SetValues(charSlots);
                    return;
                }
                if (slot.isEmpty == true)
                {
                    slot.EquipItem(item);
                    itemEquiped = true;
                }
                else
                {
                    slot.DequipItem();
                    slot.EquipItem(item);
                    itemEquiped = true;
                }
            }

            CheckArms();

            if (itemEquiped)
            {
                statsPanel.SetValues(charSlots);
                return;
            }
        }
    }

    private bool CheckTypes(Item item, CharacterSlot slot)
    {
        for (int i = 0; i < slot.itemType.Length; i++)
            if (item.itemType == slot.itemType[i])
                return true;
        return false;
    }

    private void CheckArms()
    {
        CharacterSlot righArm = charSlots[1];
        CharacterSlot leftArm = charSlots[2];

        if (righArm.GetItem != null && righArm.GetItem.GetItem.twoHanded)
            leftArm.DequipItem();
    }

    public void ShowCurrentHeroInventory(int heroIndex)
    {
        characterPanel.ShowCurrentHeroInventory(heroIndex);
        charSlots = characterPanel.GetCharacterSlots;
    }

    public ItemPanel GetItemPanel
    {
        get { return itemPanel; }
    }
}
