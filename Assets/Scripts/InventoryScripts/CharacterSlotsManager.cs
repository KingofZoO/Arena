using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlotsManager : MonoBehaviour
{
    private int currHero = 0;
    public GameObject heroTemplate;
    private GameObject[] heroInventories;
    private CharacterSlot[] slots;

    private void Awake()
    {
        CreateTemplates();
    }

    private void CreateTemplates()
    {
        heroInventories = new GameObject[HeroDataContainer.Instance.HeroCount];

        for (int i = 0; i < heroInventories.Length; i++)
        {
            var hero = HeroDataContainer.Instance.GetHeroAt(i);

            if (i == 0)
            {
                heroTemplate.name = hero.heroName;
                heroInventories[i] = heroTemplate;
            }
            else
            {
                var newTemplate = Instantiate(heroTemplate, transform);
                newTemplate.name = hero.heroName;
                newTemplate.SetActive(false);
                heroInventories[i] = newTemplate;
            }
        }
    }

    public void ShowCurrentHeroInventory(int heroIndex)
    {
        currHero = heroIndex;
        for(int i=0; i < heroInventories.Length; i++)
        {
            if (i == currHero)
                heroInventories[i].SetActive(true);
            else heroInventories[i].SetActive(false);
        }
    }

    public CharacterSlot[] GetCharacterSlots
    {
        get { return heroInventories[currHero].GetComponentsInChildren<CharacterSlot>(); }
    }
}
