using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    public float hCost = 0;
    public float gCost = 0;
    public float fCost = 0;
    public int energyCost = 0;

    List<HexTile> neighbours = new List<HexTile>();
    bool walkable = false;
    bool selected = false;
    bool hovered = false;
    HexTile parent;

    // Mesh and Shader variables
    Renderer render;
    float isStable;

    private void Start()
    {
        // 0 for False, 1 for True
        render = GetComponent<MeshRenderer>();
        isStable = Shader.PropertyToID("_IsStable");

        CheckAbove();
        FindNeighbours();
    }

    private void Update()
    {
        Hovered = false;
    }

    private void LateUpdate()
    {
        if (hovered)
            isStable = HexGrid.instance.ShaderLerp();
        PropertyToShader();
    }

    void FindNeighbours()
    {
        List<Vector3> dir = HexGrid.instance.Directions(transform); 

        for (int i = 0; i < 6; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir[i], out hit, 0.5f, HexGrid.instance.layerMask))
                if (hit.collider.tag == "Tile")
                    neighbours.Add(hit.collider.gameObject.GetComponent<HexTile>());
        }
    }

    public HexTile Parent
    {
        get { return parent; }
        set { parent = value; }
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
            if (hit.collider.tag == "Enviro")
                Destroy(this.gameObject);
    }

    public void ResetTileValues()
    {
        hCost = 0;
        gCost = 0;
        fCost = 0;
        energyCost = 0;
    }

    public void ResetTileColor()
    {
        isStable = 0;
        render.material.color = HexGrid.instance.normal;
    }

    public bool Walkable
    {
        get { return walkable; }
        set
        {
            walkable = value;
            if (value)
            {
                render.material.color = HexGrid.instance.whenWalkable;
                isStable = 1;
            }
            else
            {
                render.material.color = HexGrid.instance.normal;
                isStable = 0;
            }
        }
    }

    public void SetSelected()
    {
        selected = true;
        render.material.color = HexGrid.instance.whenSelected;
    }

    public bool Hovered
    {
        get { return hovered; }
        set
        {
            hovered = value;
            if (value)
            {
                render.material.color = HexGrid.instance.whenHovered;
            }
            else
            {
                if (selected)
                    render.material.color = HexGrid.instance.whenSelected;
                else if (walkable)
                    render.material.color = HexGrid.instance.whenWalkable;
                else
                {
                    render.material.color = HexGrid.instance.normal;
                    isStable = 0;
                }
            }
        }
    }

    void PropertyToShader()
    {
        render.material.SetFloat("_IsStable", isStable);
    }
}