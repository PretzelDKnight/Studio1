using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : HexTile
{    
    List<HexTile> openList;
    List<HexTile> closedList;

    HexTile tile;

    private const int moveCost = 10;

    List<HexTile> FindPath(HexTile start, HexTile destination)
    {
        openList = new List<HexTile> { start };
        closedList = new List<HexTile>();

        for (int i = 0; i < tile.returnNeighbours().Count; i++)
        {
            for (int j = 0; j < tile.returnNeighbours().Count; j++)
            {
                tile.gCost = int.MaxValue;
                tile.CalculateFCost();
                tile.parent = null;
            }
        }

        start.gCost = 0;
        start.hCost = CalculateDistance(start, destination);
        start.CalculateFCost();

        while(openList.Count > 0)
        {
            HexTile currentTile = GetLowestFTile(openList);
            if (currentTile = destination)
                return CalculatePath(destination); //Reached end
        }

        return null;
    }

    List<HexTile> CalculatePath(HexTile tile)
    {
        return null;
    }

    int CalculateDistance(HexTile a, HexTile b)
    {
        int xDist = (int)Mathf.Abs(a.transform.position.x - b.transform.position.x);
        int yDist = (int)Mathf.Abs(a.transform.position.z - b.transform.position.z);
        int remainDist = (int)Mathf.Abs(xDist - yDist);
        return moveCost*Mathf.Min(xDist,yDist) + moveCost*remainDist;
    }

    HexTile GetLowestFTile(List<HexTile> list)
    {
        HexTile lowestf = tile.returnNeighbours()[0];
        for(int i = 1; i< tile.returnNeighbours().Count; i++)
        {
            if (tile.returnNeighbours()[i].fCost < lowestf.fCost)
                lowestf = tile.returnNeighbours()[i];
        }
        return lowestf;
    }
}

