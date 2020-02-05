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

    List<HexTile> FindSelectableTiles()
    {

        return null;
    }

    // A Star!!!
    List<HexTile> FindPath(HexTile start, HexTile destination)
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
                return CalculatePath(destination); //Reached end

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

    List<HexTile> CalculatePath(HexTile tile)
    {
        return null;
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

