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

    // Mesh and Shader variables
    Renderer render;
    float isStable;


    private void Start()
    {
        // 0 for False, 1 for True
        render = transform.GetComponent<MeshRenderer>();

        isStable = Shader.PropertyToID("_IsStable");
        walkable = false;

        CheckAbove();
        FindNeighbours();
    }

    private void Update()
    {
        Hovered = false;
    }

    private void LateUpdate()
    {
        if (isStable != 0) 
            isStable = TileManager.LerpValue();
        PropertyToShader();
    }

    // Function to find neighbours of the current tile
    void FindNeighbours()
    {
        List<Vector3> dir = TileManager.instance.Directions(transform); 

        for (int i = 0; i < dir.Count; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir[i], out hit, TileManager.instance.layerMask))
                if (hit.collider.tag == "Tile")
                    neighbours.Add(hit.collider.gameObject.GetComponent<HexTile>());
        }
    }

    // Function to get set parent
    public HexTile Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    // Function to return neighbours
    public List<HexTile> ReturnNeighbours() 
    { 
        return neighbours; 
    }

    // Calculates and assigns Fcost
    public void CalculateFCost()
    {
        fCost = (int)hCost + gCost;
    }

    // Checks above the tile, if it is hindered, it deletes itself
    void CheckAbove()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.5f))
            if (hit.collider.tag == "Enviro")
            {
                DestroyMeself();
            }
    }

    // Resets tile values
    public void ResetTileValues()
    {
        hCost = 0;
        gCost = 0;
        fCost = 0;
        energyCost = 0;
        walkable = false;
        selected = false;
        attackable = false;
        parent = null;
        ResetTileColor();
    }

    // Resets tile color and shader properties
    public void ResetTileColor()
    {
        isStable = 0;
        render.material.color = TileManager.normal;
    }

    // Get set for Walkable that changes shader property depending on value
    public bool Walkable
    {
        get { return walkable; }
        set
        {
            walkable = value;
            if (walkable)
            {
                render.material.color = TileManager.instance.whenWalkable;
                isStable = 1;
            }
            else
            {
                render.material.color = TileManager.normal;
                isStable = 0;
            }
        }
    }

    // Get set for Attackable that changes shader property depending on value
    public bool Attackable
    {
        get { return walkable; }
        set
        {
            attackable = value;
            if (attackable)
            {
                render.material.color = TileManager.instance.whenAttackable;
                isStable = 1;
            }
            else
            {
                render.material.color = TileManager.normal;
                isStable = 0;
            }
        }
    }

    // Get set for Hovered that changes shader property depending on value
    public bool Hovered
    {
        get { return hovered; }
        set
        {
            bool prev = hovered;
            hovered = value;
            if (hovered)
            {
                if (!selected)
                {
                    render.material.color = TileManager.instance.whenHovered;
                    isStable = 1;
                }
            }
            else
            {
                if (!prev)
                {
                    if (selected)
                        render.material.color = TileManager.instance.whenSelected;
                    else if (walkable)
                        render.material.color = TileManager.instance.whenWalkable;
                    else if (attackable)
                        render.material.color = TileManager.instance.whenAttackable;
                    else
                    {
                        render.material.color = TileManager.normal;
                        isStable = 0;
                    }
                }
            }
        }
    }

    // Sets the tile as selected and changes shader color
    public void SetSelected()
    {
        selected = true;
        isStable = 1;
        render.material.color = TileManager.instance.whenSelected;
    }

    // Get set for Occupied 
    public bool Occupied
    {
        get { return occupied; }
        set { occupied = value; }
    }

    // Writes the value of the variables into the relevent shader properties for shader manipulation
    void PropertyToShader()
    {
        render.material.SetFloat("_IsStable", isStable);
    }

    // Returns estimated position of the character above said tile
    public Vector3 ReturnTargetPosition(Vector3 charaPos)
    {
        return new Vector3(transform.position.x, charaPos.y, transform.position.z);
    }

    // Returns the enemy relative to the character being passed as parameter if they exist above the said tile
    public Character ReturnTarget(Character source)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.5f))
            if (hit.collider.tag != source.transform.tag)
                return hit.collider.GetComponent<Character>();
        return null;
    }

    // Returns Character above tile if there is any
    public Character ReturnChara()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.5f))
                return hit.collider.GetComponent<Character>();
        return null;
    }

    // Destroys meself!!!
    public void DestroyMeself()
    {
        Destroy(gameObject);
        Destroy(this);
    }
}