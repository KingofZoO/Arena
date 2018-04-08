using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovingScript : MonoBehaviour
{
    private Transform unit;
    private List<Transform> units = new List<Transform>();
    private ArenaPathScript arenaPath;
    private List<Transform> pathList = new List<Transform>();
    private UnitScript unitScript;
    private LayerMask unitLayer;

    private float movingSpeed = 0.05f;
    private bool isMoving = false;
    private bool isStarted = false;

    private int currHex = 0;
    private int currHero = 0;

    private bool isHeroTurn = false;
    private bool isEnemyTurn = false;

    private void Start()
    {
        arenaPath = GetComponent<ArenaPathScript>();
        unitLayer = 1 << LayerMask.NameToLayer("Unit");

        foreach (var obj in GameObject.FindGameObjectsWithTag("Hero"))
            units.Add(obj.transform);

        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
            units.Add(obj.transform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isStarted)
        {
            pathList = arenaPath.GetHitList;
            isStarted = true;
            SelectHero();
        }

        if (Input.GetMouseButtonDown(1))
        {
            isMoving = true;
            TurnPassed();
        }

        if (isMoving)
        {
            MoveUnit();
        }

        if (unit != null && Time.frameCount % 5 == 0)
        {
            if (isHeroTurn)
                arenaPath.CastLine(unitScript.CurrentActionPoints);
        }
    }

    private void SelectHero()
    {
        if (isMoving)
            return;

        unit = units[currHero];
        unitScript = unit.GetComponent<UnitScript>();
        arenaPath.SelectNewHex(unit.position);
        if (unit.tag == "Hero")
        {
            isHeroTurn = true;
        }
        else if (unit.tag == "Enemy")
        {
            isEnemyTurn = true;
            arenaPath.CastLineToObj(GetNearHero(), unitScript.CurrentActionPoints);
        }
        currHero++;
        if (currHero == units.Count)
            currHero = 0;

        arenaPath.HighlightHex();
    }

    private void MoveUnit()
    {
        if (unit == null)
            return;

        if (pathList.Count == 0)
        {
            TurnEnded();
            return;
        }

        Vector3 endPos = pathList[currHex].position;
        Vector2 destinationPos = (endPos - unit.position).normalized * movingSpeed;
        unit.position += new Vector3(destinationPos.x, destinationPos.y, 0);
        arenaPath.HighlightHex();

        if (Mathf.Abs((unit.position - endPos).magnitude) <= movingSpeed)
        {
            currHex++;
            unitScript.GetAction();
            if (currHex == pathList.Count)
                TurnEnded();
        }
    }

    private Vector3 GetNearHero()
    {
        Vector3 nearHero = Vector3.zero;
        float dist = Mathf.Infinity;

        for(int i = 0; i < units.Count; i++)
        {
            if (units[i].tag == "Hero" && Vector3.Distance(units[i].position, unit.position) < dist)
            {
                dist = Vector3.Distance(units[i].position, unit.position);
                nearHero = units[i].position;
            }
        }
        return nearHero;
    }

    private void TurnPassed()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, unitLayer);
        if (hit.collider && hit.transform == unit)
        {
            TurnEnded();
            MovePassed();
        }
    }

    private void TurnEnded()
    {
        isMoving = false;
        arenaPath.TurnEnded();
        currHex = 0;
        arenaPath.SelectNewHex(unit.position);

        if (unitScript.CurrentActionPoints == 0 || unit.tag == "Enemy")
        {
            MovePassed();
        }
    }

    private void MovePassed()
    {
        unitScript.RefreshAP();
        isHeroTurn = false;
        isEnemyTurn = false;
        SelectHero();
    }
}
