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
    bool occupied = false;
    bool walkable = false;
    bool selected = false;
    bool hovered = false;
    bool attackable = false;
    HexTile parent;
    Vector3 position;

    // Mesh and Shader variables
    Renderer render;
    float isStable;

    private void Start()
    {
        // 0 for False, 1 for True
        render = transform.GetComponent<MeshRenderer>();
        if(render == null)
        {
            Debug.Log("Bruh, we got this");
        }

        isStable = Shader.PropertyToID("_IsStable");
        Walkable = false;

        CheckAbove();
        FindNeighbours();
        position = transform.position;
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
            {
                HexGrid.tiles.Remove(this);
                Destroy(this.gameObject);
            }
    }

    public void ResetTileValues()
    {
        hCost = 0;
        gCost = 0;
        fCost = 0;
        energyCost = 0;
        walkable = false;
        selected = false;
        hovered = false;
        ResetTileColor();
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
                Debug.Log(render.material.color.ToString());
                isStable = 0;
            }
        }
    }

    public bool Attackable
    {
        get { return walkable; }
        set
        {
            walkable = value;
            if (value)
            {
                render.material.color = HexGrid.instance.whenAttackable;
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

    public bool Occupied
    {
        get { return occupied; }
        set { occupied = value; }
    }

    void PropertyToShader()
    {
        render.material.SetFloat("_IsStable", isStable);
    }

    /// <summary>
    /// Returns determines position of the character on top of the said tile
    /// </summary>
    /// <param name="charaPos"></param>
    /// <returns></returns>
    public Vector3 ReturnPosition(Vector3 charaPos)
    {
        return new Vector3(position.x, charaPos.y, position.z);
    }

    public Character ReturnTarget(Character source)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.5f))
            if (hit.collider.tag != source.transform.tag)
                return hit.collider.GetComponent<Character>();
        return null;
    }
}