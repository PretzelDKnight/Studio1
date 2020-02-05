using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [HideInInspector] public float hCost = 0;
    [HideInInspector] public int gCost = 0;
    [HideInInspector] public int fCost = 0;

    List<HexTile> neighbours = new List<HexTile>();

    bool walkable = true;

    HexTile parent;

    public LayerMask layersToCheck;

    private void Start()
    {
        CheckAbove();
        FindNeighbours();
    }

    void FindNeighbours()
    {
        List<Vector3> dir = HexGrid.instance.Directions(this.transform); 

        for (int i = 0; i < 6; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir[i], out hit, 0.5f, layersToCheck))
            {
                if (hit.collider.tag == "Tile")
                {
                    neighbours.Add(hit.collider.gameObject.GetComponent<HexTile>());
                    walkable = true;
                }
            }
        }
    }

    public HexTile Parent
    {
        get
        {
            return parent;
        }
        set
        {
            parent = value;
        }
    }

    public List<HexTile> returnNeighbours()
    {
        return neighbours;
    }

    public void CalculateFCost()
    {
        fCost = (int)hCost + gCost;
    }

    void CheckAbove()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.5f))
        {
            if (hit.collider.tag == "Enviro")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
