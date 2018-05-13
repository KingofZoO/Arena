using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsPanel : MonoBehaviour
{
    private Text actionPoints;
    private Text damagePoints;
    private Text healthPoints;
    private Text heroName;
    private Image heroImage;
    private Button nextHeroButton;

    private Hero hero;

    private int currHero = 0;
    private bool isStatsSetted = false;

    private void Awake()
    {
        SetUIElements();
        HeroDataContainer.Instance.ResetHeroStats();

        hero = HeroDataContainer.Instance.GetHeroAt(currHero);
        LoadHeroStats(hero);
    }

    private void SetUIElements()
    {
        actionPoints = transform.Find("APLabel").GetComponent<Text>();
        damagePoints = transform.Find("DPLabel").GetComponent<Text>();
        healthPoints = transform.Find("HPLabel").GetComponent<Text>();
        heroName = transform.Find("HeroName").GetComponent<Text>();

        heroImage = transform.Find("HeroImage").GetComponent<Image>();
        heroImage.preserveAspect = true;

        nextHeroButton = transform.Find("NextHeroButton").GetComponent<Button>();
    }

    public void SetValues(CharacterSlot[] slots)
    {
        var stats = hero.GetBasicStats();
        int AP = stats[0], DP = stats[1], HP = stats[2];
        for(int i = 0; i < slots.Length; i++)
        {
            var slot = slots[i];
            if (slot.GetItem != null)
            {
                AP += slot.GetItem.GetItem.ActionPoints;
                DP += slot.GetItem.GetItem.DamagePoints;
                HP += slot.GetItem.GetItem.HealthPoints;
            }
        }

        SetTextValues(AP.ToString(), DP.ToString(), HP.ToString());
    }

    private void SetTextValues(string ap, string dp, string hp)
    {
        actionPoints.text = ap;
        damagePoints.text = dp;
        healthPoints.text = hp;
    }

    private void LoadHeroStats(Hero hero)
    {
        this.hero = hero;
        var stats = hero.GetCurrentStats();

        heroImage.sprite = hero.heroSprite;
        heroName.text = hero.heroName;
        SetTextValues(stats[0].ToString(), stats[1].ToString(), stats[2].ToString());
    }

    private void SaveHeroStats(Hero hero)
    {
        var AP = int.Parse(actionPoints.text);
        var DP = int.Parse(damagePoints.text);
        var HP = int.Parse(healthPoints.text);

        hero.SetCurrentStats(new List<int> { AP, DP, HP });
    }

    public void NextHero()
    {
        currHero++;
        SaveHeroStats(hero);

        if (isStatsSetted)
        {
            SceneManager.LoadScene("ArenaScene");
            return;
        }

        if (currHero == HeroDataContainer.Instance.HeroCount - 1)
        {
            isStatsSetted = true;
            nextHeroButton.GetComponentInChildren<Text>().text = "To the ARENA!";
        }

        InventoryManager.Instance.ShowCurrentHeroInventory(currHero);
        LoadHeroStats(HeroDataContainer.Instance.GetHeroAt(currHero));
    }
}
