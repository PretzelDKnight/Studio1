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

    List<HexTile> FindPath(HexTile start, HexTile destination)
    {
        openList = new List<HexTile>();
        closedList = new List<HexTile> { start };

        for (int i = 0; i < tile.returnNeighbours().Count; i++)
        {
            for (int j = 0; j < tile.returnNeighbours().Count; j++)
            {
                tile.gCost = int.MaxValue;
                tile.CalculateFCost();
                tile.Parent = null;
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

            openList.Remove(currentTile);
            closedList.Add(currentTile);
        }               

        return null;
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
        HexTile lowestf = tile.returnNeighbours()[0];
        for(int i = 1; i< tile.returnNeighbours().Count; i++)
        {
            if (tile.returnNeighbours()[i].fCost < lowestf.fCost)
                lowestf = tile.returnNeighbours()[i];
        }
        return lowestf;
    }
}

