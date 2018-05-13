using UnityEngine;

public enum ItemType
{
    Axe,
    Helmet,
    Shield,
    Sword,
}

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public bool twoHanded;

    [SerializeField] private int actionPoints;
    [SerializeField] private int damagePoints;
    [SerializeField] private int healthPoints;

    [HideInInspector] public string itemName;
    private Sprite[] itemIcon;
    private int itemNum = -1;

    private void OnEnable()
    {
        itemName = itemType.ToString();
        itemIcon = Resources.LoadAll<Sprite>("Items_Img/" + name);
    }

    public Sprite GetItemIcon
    {
        get
        {
            itemNum++;
            if (itemNum == itemIcon.Length)
                itemNum = 0;
            return itemIcon[itemNum];
        }
    }

    public int ActionPoints
    {
        get { return actionPoints; }
    }

    public int DamagePoints
    {
        get { return damagePoints; }
    }

    public int HealthPoints
    {
        get { return healthPoints; }
    }
}
