using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/HeroDataContainer")]
public class HeroDataContainer : ScriptableObject
{
    public static HeroDataContainer instance;

    public Hero[] heroes;
    private int currHero = 0;
    private int enemyCount;

    public static HeroDataContainer Instance
    {
        get
        {
            if (!instance)
                instance = Resources.Load<HeroDataContainer>("HeroDataContainer");
            return instance;
        }
    }

    public void SetHeroArray(int heroCount, int enemyCount)
    {
        this.enemyCount = enemyCount;

        heroes = new Hero[heroCount];
        for (int i = 0; i < heroCount; i++)
            heroes[i] = new Hero();
    }

    public Hero GetHeroAt(int heroIndex)
    {
        if (heroIndex >= 0 && heroIndex < heroes.Length)
        {
            currHero = heroIndex;
            return heroes[currHero];
        }
        return null;
    }

    public void ResetHeroStats()
    {
        foreach (var hero in heroes)
            hero.ResetStats();
    }

    public int HeroCount
    {
        get { return heroes.Length; }
    }

    public int EnemyCount
    {
        get { return enemyCount; }
    }
}
