using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [HideInInspector] public int hCost;
    [HideInInspector] public int gCost;
    [HideInInspector] public int fCost;
    
    HexGrid grid;

    List<HexTile> neighbours;

    bool walkable;

    [HideInInspector] public HexTile parent;

    public LayerMask layersToCheck;

    void FindNeighbours()
    {
        for (int i = 0; i < 6; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, grid.directions[i], out hit, 0.5f, layersToCheck))
            {
                if (hit.collider.tag != "Enviro")
                {
                    neighbours.Add(hit.collider.gameObject.GetComponent<HexTile>());
                    walkable = true;
                }
            }
        }
    }

    public HexTile returnParent()
    {
        return parent;
    }

    public List<HexTile> returnNeighbours()
    {
        return neighbours;
    }

    public void CalculateFCost()
    {
        fCost = hCost + gCost;
    }
}
