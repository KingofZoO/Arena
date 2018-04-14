using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneratorScript : MonoBehaviour
{
    private int unitCount;
    private int enemyCount;
    private bool isCommandBattle;
    private Sprite[] helmsSprite;
    private List<Sprite> helmsList = new List<Sprite>();
    private List<Vector2> unitPositions = new List<Vector2>();

    private int heroNum = -1;
    private int enemyNum = -1;
    private int helmNum = -1;

    private List<Transform> unitList = new List<Transform>();

    public void GenerateUnits(int unitCount, int enemyCount, bool isCommandBattle)
    {
        this.unitCount = unitCount;
        this.enemyCount = enemyCount;
        this.isCommandBattle = isCommandBattle;

        unitPositions = MainScript.Instance.GetArenaGeneratorScript.UnitPositions;
        helmsSprite = Resources.LoadAll<Sprite>("Helms Sprite");
        RefreshHelmsList();

        SetUnits();
    }

    private void SetUnits()
    {
        for (int i = 0; i < unitCount; i++)
            unitList.Add(CreateHero());

        for (int i = 0; i < enemyCount; i++)
            SetToEnemy(unitList[unitCount - 1 - i]);
    }

    private Transform CreateHero()
    {
        int rand = Random.Range(0, unitPositions.Count);
        Vector3 pos = unitPositions[rand];
        unitPositions.RemoveAt(rand);

        heroNum++;
        GameObject unit = new GameObject("Hero" + "(" + heroNum + ")");
        unit.AddComponent<SpriteRenderer>().sprite = GetUnitSprite();
        unit.GetComponent<SpriteRenderer>().sortingLayerName = "Unit";
        unit.AddComponent<HeroScript>();
        unit.AddComponent<BoxCollider2D>().isTrigger = true;
        unit.tag = "Hero";
        unit.layer = LayerMask.NameToLayer("Unit");
        unit.transform.position = pos;
        return unit.transform;
    }

    private void SetToEnemy(Transform unit)
    {
        enemyNum++;
        Destroy(unit.GetComponent<HeroScript>());
        unit.gameObject.AddComponent<EnemyScript>();
        unit.tag = "Enemy";
        unit.name = "Enemy" + "(" + enemyNum + ")";

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
