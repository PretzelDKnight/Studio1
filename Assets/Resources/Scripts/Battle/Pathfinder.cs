using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public static Pathfinder instance = null;

    List<HexTile> openList;
    List<HexTile> closedList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void FindSelectableTiles(Character source)
    {
        HexTile start = source.GetCurrentTile();
        float energy = source.energy.runTimeValue;
        List<HexTile> tempList = new List<HexTile>() { start };

        while (tempList.Count > 0)
        {
            HexTile tile = GetLowestEnergyCost(tempList);

            tempList.Remove(tile);

            foreach (var item in tile.returnNeighbours())
            {
                if (item.energyCost == 0 && !item.Occupied)
                {
                    item.energyCost = 1 + tile.energyCost;
                    if (item.energyCost <= energy)
                    {
                        tempList.Add(item);
                        item.Walkable = true;
                    }
                }
            }
        }
        start.ResetTileValues();
    }

    public void FindAttackableCharacters(Character source)
    {
        HexTile start = source.GetCurrentTile();
        float range = source.stats.attackRange;
        List<HexTile> temp = new List<HexTile>() { start };
        int checkRange = 0;

        while (checkRange >= range)
        {
            foreach (var tile in temp)
            {
                temp.Add(tile);
                foreach (var item in tile.returnNeighbours())
                {
                    if (item.Occupied)
                    {
                        Character targetable = item.ReturnTarget(source);
                        if (targetable != null)
                        {
                            item.Attackable = true;
                            temp.Add(item);
                        }
                    }
                }
            }
            checkRange++;
        }

        start.ResetTileValues();
    }

    // A Star!!!
    public List<HexTile> FindPath(HexTile start, HexTile destination)
    {
        openList = new List<HexTile>() { start };
        closedList = new List<HexTile>();

        start.gCost = 0;
        start.hCost = CalculateDistance(start, destination);
        start.CalculateFCost();

        while (openList.Count > 0)
        {
            HexTile tile = GetLowestFTile(openList);

            openList.Remove(tile);
            closedList.Add(tile);

            if (tile == destination)
            {
                break; //Reached end
            }

            foreach (var item in tile.returnNeighbours())
            {
                if (!item.Occupied)
                    if (closedList.Contains(item)) { } //Do Nothing
                    else if (openList.Contains(item)) //Found new route with lesser cost
                    {
                        float tempG = tile.gCost + CalculateDistance(item, tile);
                        if (tempG < item.gCost)
                        {
                            item.Parent = tile;
                            item.gCost = tempG;
                            item.fCost = item.gCost + item.hCost;

                        }
                    }
                    else //Continuing to explore current path
                    {
                        item.Parent = tile;

                        item.gCost = tile.gCost + CalculateDistance(item, tile);
                        item.hCost = CalculateDistance(item, destination);
                        item.fCost = item.gCost + item.hCost;
                        openList.Add(item);
                    }
            }
        }

        List<HexTile> temp = new List<HexTile>();
        HexTile ht = closedList[closedList.Count - 1];
        while (ht != null)
        {
            temp.Insert(0,ht);
            ht = ht.Parent;
        }

        return temp;
    }

    float CalculateDistance(HexTile a, HexTile b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }

    HexTile GetLowestFTile(List<HexTile> list)
    {
        HexTile lowest = list[0];
        foreach (var item in list)
        {
            if (item.fCost < lowest.fCost)
                lowest = item;
        }

        return lowest;
    }

    HexTile GetLowestEnergyCost(List<HexTile> list)
    {
        HexTile lowest = list[0];
        foreach (var item in list)
        {
            if (item.energyCost < lowest.energyCost)
                lowest = item;
        }

        return lowest;
    }
}

