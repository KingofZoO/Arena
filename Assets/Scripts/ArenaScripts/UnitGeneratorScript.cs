using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneratorScript : MonoBehaviour
{
    [SerializeField] private UnitScript unitTemplate;

    private int unitCount;
    private int enemyCount;
    private bool isCommandBattle;
    private Sprite[] helmsSprite;
    private List<Sprite> helmsList = new List<Sprite>();
    private List<Vector2> unitPositions = new List<Vector2>();

    private List<Transform> unitList = new List<Transform>();

    private void Start()
    {
        GenerateUnits(HeroDataContainer.Instance.EnemyCount);
    }

    public void GenerateUnits(int enemyCount)
    {
        unitCount = HeroDataContainer.Instance.HeroCount;
        this.enemyCount = enemyCount;

        unitPositions = MainScript.Instance.GetArenaGeneratorScript.UnitPositions;
        helmsSprite = Resources.LoadAll<Sprite>("Helms Sprite");
        RefreshHelmsList();

        SetUnits();
    }

    private void SetUnits()
    {
        for (int i = 0; i < unitCount; i++)
            unitList.Add(CreateHero(HeroDataContainer.Instance.GetHeroAt(i)));

        for (int i = 0; i < enemyCount; i++)
            SetToEnemy(unitList[unitCount - 1 - i]);
    }

    private Transform CreateHero(Hero hero)
    {
        int rand = Random.Range(0, unitPositions.Count);
        Vector3 pos = unitPositions[rand];
        unitPositions.RemoveAt(rand);

        var unit = Instantiate(unitTemplate);

        unit.name = hero.heroName;
        unit.GetComponent<SpriteRenderer>().sprite = hero.heroSprite;
        unit.SetParameters(hero);
        unit.transform.position = pos;
        return unit.transform;
    }

    private void SetToEnemy(Transform unit)
    {
        unit.tag = "Enemy";

        if (isCommandBattle == true)
            FlipUnit(unit);
    }

    private Sprite GetUnitSprite()
    {
        if (helmsList.Count == 0)
            RefreshHelmsList();

        int rand = Random.Range(0, helmsList.Count);
        var sprite = helmsList[rand];
        helmsList.RemoveAt(rand);
        return sprite;
    }

    private void RefreshHelmsList()
    {
        for (int i = 0; i < helmsSprite.Length; i++)
            helmsList.Add(helmsSprite[i]);
    }

    private void FlipUnit(Transform unit)
    {
        var scale = unit.localScale;
        scale.x *= -1;
        unit.localScale = scale;
    }

    public List<Transform> GetUnitList
    {
        get { return unitList; }
    }
}
