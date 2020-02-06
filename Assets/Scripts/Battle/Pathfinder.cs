using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public static Pathfinder instance = null;

    List<HexTile> openList;
    List<HexTile> closedList;
    
    private const int moveCost = 10;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public List<HexTile> FindSelectableTiles(Character source)
    {
        HexTile start = source.GetCurrentTile();
        float energy = source.currentStats.energyPool;
        List<HexTile> temp = new List<HexTile>() { start };
        int energyUsed = 0;

        while (energyUsed >= energy)
        {
            foreach (var tile in temp)
            {
                temp.Add(tile);
                foreach (var item in tile.returnNeighbours())
                {
                    if (item.energyCost == 0)
                        if (!item.Occupied)
                        {
                            item.energyCost = energyUsed;
                            item.Walkable = true;
                            temp.Add(item);
                        }
                }
            }
            energyUsed++;
        }

        start.ResetTileValues();

        return temp;
    }

    public List<HexTile> FindAttackableCharacters(Character source)
    {
        HexTile start = source.GetCurrentTile();
        float range = source.currentStats.range;
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

        return temp;
    }

    // A Star!!!
    public List<HexTile> FindPath(HexTile start, HexTile destination)
    {
        openList = new List<HexTile>() { start };
        closedList = new List<HexTile>();

        start.gCost = 0;
        start.hCost = CalculateDistance(start, destination);
        start.CalculateFCost();

        while(openList.Count > 0)
        {
            HexTile tile = GetLowestFTile(openList);

            closedList.Add(tile);

            if (tile = destination)
                return closedList; //Reached end

            foreach (var item in tile.returnNeighbours())
            {
                if (closedList.Contains(tile)) { } //Do Nothing
                else if (openList.Contains(tile)) //Found new route with lesser cost
                {
                    float tempG = tile.gCost + CalculateDistance(item,tile);
                    if (tempG < item.gCost)
                    {
                        item.Parent = tile;
                        item.gCost = tempG;
                        item.CalculateFCost();
                    }
                }
                else //Continuing to explore current path
                {
                    item.Parent = tile;

                    item.gCost = tile.gCost + CalculateDistance(item, tile);
                    item.hCost = CalculateDistance(item, destination);
                    item.CalculateFCost();

                    openList.Add(item);
                }
            }
        }
        return closedList;
    }

    float CalculateDistance(HexTile a, HexTile b)
    {
        float xDist = Mathf.Abs(a.transform.position.x - b.transform.position.x);
        float yDist = Mathf.Abs(a.transform.position.z - b.transform.position.z);
        float remainDist = Mathf.Abs(xDist - yDist);
        return moveCost*remainDist;
    }

    HexTile GetLowestFTile(List<HexTile> list)
    {
        HexTile lowestf = list[0];
        for(int i = 1; i< list.Count; i++)
        {
            if (list[i].fCost < lowestf.fCost)
                lowestf = list[i];
        }
        return lowestf;
    }
}

