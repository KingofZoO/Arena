using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPathScript : MonoBehaviour
{
    private Transform hex;
    private IColoredHex iHex;
    private LayerMask hexLayer;
    private LayerMask unitLayer;
    private List<Transform> hitList = new List<Transform>();

    private float hexDist;
    private bool isCasting = true;

    private void Start()
    {
        hexLayer = 1 << LayerMask.NameToLayer("MapHex");
        unitLayer = 1 << LayerMask.NameToLayer("Unit");
        var arena = GameObject.Find("MainObject").GetComponent<ArenaGeneratorScript>();
        hexDist = arena.WidthDistBtwHex;
    }

    public void SelectNewHex(Vector3 point)
    {
        if (!isCasting)
            return;

        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, Mathf.Infinity, hexLayer);

        if (hit.transform == hex)
            return;
        if (hex != null)
        {
            iHex.DeselectHex();
        }
        hex = hit.transform;
        iHex = hex.GetComponent<IColoredHex>();
        iHex.SelectHex();
    }

    public void HighlightHex()
    {
        if (hitList.Count == 0)
            return;

        isCasting = false;
        hitList[hitList.Count - 1].GetComponent<IColoredHex>().SelectHex();
    }

    public void CastLine(int steps)
    {
        if (!isCasting)
            return;

        Transform checkHex = hex;
        RaycastHit2D checkHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, hexLayer);
        if (!checkHit.collider || checkHit.collider.GetComponent<IColoredHex>() == null)
            return;
        else checkHex = checkHit.transform;

        List<Transform> newList = new List<Transform>();

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Transform temp = hex;

        for (int i = 1; i <= steps; i++)
        {
            if (temp == checkHex)
                break;

            Vector2 tempPos = new Vector2(temp.position.x, temp.position.y);
            Vector2 castPoint = tempPos + (mousePos - tempPos).normalized * hexDist;

            RaycastHit2D checkUnitHit = Physics2D.Raycast(castPoint, Vector2.zero, Mathf.Infinity, unitLayer);
            if (checkUnitHit.collider)
            {
                break;
                /*Vector2 vec = (mousePos - tempPos).normalized * hexDist;

                var angle = (Mathf.Atan2(mousePos.y, mousePos.x) - Mathf.Atan2(tempPos.y, tempPos.x)) * Mathf.Rad2Deg;

                vec = Quaternion.Euler(0, 0, angle > 0 ? 45f : -45f) * vec;
                castPoint = tempPos + vec;*/
            }

            RaycastHit2D hit = Physics2D.Raycast(castPoint, Vector2.zero, Mathf.Infinity, hexLayer);
            if (hit.collider)
            {
                hit.collider.GetComponent<IColoredHex>().SetToPathColor();
                temp = hit.transform;
                newList.Add(temp);
            }
        }

        HitListReload(newList);
    }

    public void CastLineToObj(Vector3 castedObj, int steps)
    {
        if (!isCasting)
            return;

        List<Transform> newList = new List<Transform>();

        Vector2 objPos = castedObj;
        Transform temp = hex;

        for (int i = 1; i <= steps; i++)
        {
            Vector2 tempPos = new Vector2(temp.position.x, temp.position.y);
            Vector2 castPoint = tempPos + (objPos - tempPos).normalized * hexDist;

            RaycastHit2D checkUnitHit = Physics2D.Raycast(castPoint, Vector2.zero, Mathf.Infinity, unitLayer);
            if (checkUnitHit.collider)
                break;
            /*if (checkUnitHit.collider && checkUnitHit.collider.tag == "Hero")
            {
                break;
            }
            else if (checkUnitHit.collider && checkUnitHit.collider.tag == "Enemy")
            {
                Vector2 vec = (objPos - tempPos).normalized * hexDist;

                var angle = (Mathf.Atan2(objPos.y, objPos.x) - Mathf.Atan2(tempPos.y, tempPos.x)) * Mathf.Rad2Deg;

                vec = Quaternion.Euler(0, 0, angle > 0 ? 60f : -60f) * vec;
                castPoint = tempPos + vec;
            }*/

            RaycastHit2D hit = Physics2D.Raycast(castPoint, Vector2.zero, Mathf.Infinity, hexLayer);
            if (hit.collider)
            {
                hit.collider.GetComponent<IColoredHex>().SetToPathColor();
                temp = hit.transform;
                newList.Add(temp);
            }
        }

        HitListReload(newList);
    }

    private void HitListReload(List<Transform> newList)
    {
        if (hitList.Count == 0)
            hitList.AddRange(newList);

        foreach (var obj in hitList)
        {
            if (obj.GetComponent<IColoredHex>().IsHighlighted)
                continue;

            if (!newList.Contains(obj))
                obj.GetComponent<IColoredHex>().SetToBasicColor();
        }

        hitList.RemoveRange(0, hitList.Count);
        hitList.AddRange(newList);
    }

    public void TurnEnded()
    {
        iHex.DeselectHex();
        hex = null;

        for(int i = 0; i < hitList.Count; i++)
        {
            hitList[i].GetComponent<IColoredHex>().SetToBasicColor();

            if (i == hitList.Count - 1)
                hitList[i].GetComponent<IColoredHex>().DeselectHex();
        }
        hitList.RemoveRange(0, hitList.Count);
        isCasting = true;
    }

    public List<Transform> GetHitList
    {
        get { return hitList; }
    }
}
