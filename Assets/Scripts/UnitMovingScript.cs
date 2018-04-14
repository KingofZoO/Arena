using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovingScript : MonoBehaviour
{
    private Transform targetUnit;
    private int attackCost = 1;
    private UnitScript targetScript;

    private Transform unit;
    private List<Transform> units = new List<Transform>();
    private ArenaPathScript arenaPath;
    private List<Transform> pathList = new List<Transform>();
    private UnitScript unitScript;
    private LayerMask unitLayer;

    private int moveCost = 1;
    private float movingSpeed = 0.15f;
    private float sqrPositionLimit = 0.01f;
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

        units = MainScript.Instance.GetUnitGeneratorScript.GetUnitList;
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
            SelectTarget();
        }

        if (Input.GetMouseButtonDown(1))
        {
            isMoving = true;
        }

        if (Input.GetMouseButtonDown(2))
        {
            SkipTurn();
        }

        if (unit != null && Time.frameCount % 5 == 0)
        {
            if (isHeroTurn)
                arenaPath.CastLine(unitScript.CurrentActionPoints);
        }
    }

    private void FixedUpdate()
    {
        if (isMoving || isEnemyTurn)
        {
            MoveUnit();
        }
    }

    private void SelectHero()
    {
        if (isMoving)
            return;

        if (currHero >= units.Count)
        currHero = 0;

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
            arenaPath.CastLineToObj(GetNearUnit(), unitScript.CurrentActionPoints);
        }
        currHero++;
        arenaPath.HighlightHex();
    }

    private void MoveUnit()
    {
        if (unit == null)
            return;

        if (pathList.Count == 0)
        {
            MoveEnded();
            return;
        }

        Vector3 endPos = pathList[currHex].position;
        Vector2 destinationPos = (endPos - unit.position).normalized * movingSpeed;
        unit.Translate(destinationPos);
        arenaPath.HighlightHex();

        if ((unit.position - endPos).sqrMagnitude <= sqrPositionLimit)
        {
            currHex++;
            unitScript.GetAction(moveCost);
            if (currHex == pathList.Count)
            {
                MoveEnded();
            }
        }
    }

    private Vector3 GetNearUnit()
    {
        Transform nearUnit = null;
        float dist = Mathf.Infinity;

        for(int i = 0; i < units.Count; i++)
        {
            if (units[i] != unit && Vector3.Distance(units[i].position, unit.position) < dist)
            {
                dist = Vector3.Distance(units[i].position, unit.position);
                nearUnit = units[i];
            }
        }

        targetUnit = nearUnit;
        targetScript = nearUnit.GetComponent<UnitScript>();

        return nearUnit.position;
    }

    private void SkipTurn()
    {
        if (!isMoving && isHeroTurn)
        {
            MoveEnded();
            TurnPassed();
        }
    }

    private void MoveEnded()
    {
        isMoving = false;
        arenaPath.TurnEnded();
        currHex = 0;
        arenaPath.SelectNewHex(unit.position);

        AttackTarget();

        if (unitScript.CurrentActionPoints == 0)
        {
            TurnPassed();
        }

        if (unit.tag == "Enemy")
            arenaPath.CastLineToObj(GetNearUnit(), unitScript.CurrentActionPoints);
    }

    private void TurnPassed()
    {
        unitScript.RefreshAP();
        isHeroTurn = false;
        isEnemyTurn = false;
        SelectHero();
    }

    private void AttackTarget()
    {
        if (targetUnit == unit)
        {
            targetUnit = null;
            targetScript = null;
        }

        if (targetUnit != null && unitScript.CurrentActionPoints >= attackCost)
        {
            unitScript.GetAction(attackCost);
            targetScript.TakeDamage(unitScript.DamagePoints);

            if (targetScript.HealthPoints <= 0)
                KillTarget();

            targetUnit = null;
            targetScript = null;
        }
    }

    private void KillTarget()
    {
        var index = units.IndexOf(targetUnit);
        units.RemoveAt(index);
        targetUnit.gameObject.SetActive(false);

        if (index < currHero)
            currHero--;
    }

    private void SelectTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, unitLayer);
        if (hit.collider && hit.transform != unit)
        {
            targetUnit = hit.transform;
            targetScript = targetUnit.GetComponent<UnitScript>();
        }
    }
}
