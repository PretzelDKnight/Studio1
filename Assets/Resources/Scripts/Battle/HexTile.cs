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
    [SerializeField] bool occupied = false;
    [SerializeField] bool walkable = false;
    [SerializeField] bool selected = false;
    [SerializeField] bool hovered = false;
    [SerializeField] bool attackable = false;
    HexTile parent;
    Vector3 position;

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
        position = transform.position;
    }

    private void Update()
    {
        Hovered = false;
    }

    private void LateUpdate()
    {
        if (isStable != 0) 
            isStable = HexGrid.LerpValue();
        PropertyToShader();
    }

    void FindNeighbours()
    {
        List<Vector3> dir = HexGrid.instance.Directions(transform); 

        for (int i = 0; i < dir.Count; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir[i], out hit, HexGrid.instance.layerMask))
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
                DestroyMeself();
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
        attackable = false;
        parent = null;
        ResetTileColor();
    }

    public void ResetTileColor()
    {
        isStable = 0;
        render.material.color = HexGrid.normal;
    }

    public bool Walkable
    {
        get { return walkable; }
        set
        {
            walkable = value;
            if (walkable)
            {
                render.material.color = HexGrid.instance.whenWalkable;
                isStable = 1;
            }
            else
            {
                render.material.color = HexGrid.normal;
                isStable = 0;
            }
        }
    }

    public bool Attackable
    {
        get { return walkable; }
        set
        {
            attackable = value;
            if (attackable)
            {
                render.material.color = HexGrid.instance.whenAttackable;
                isStable = 1;
            }
            else
            {
                render.material.color = HexGrid.normal;
                isStable = 0;
            }
        }
    }

    public bool Hovered
    {
        get { return hovered; }
        set
        {
            hovered = value;
            if (hovered)
            {
                render.material.color = HexGrid.instance.whenHovered;
                isStable = 1;
            }
            else
            {
                if (selected)
                    render.material.color = HexGrid.instance.whenSelected;
                else if (walkable)
                    render.material.color = HexGrid.instance.whenWalkable;
                else
                {
                    render.material.color = HexGrid.normal;
                    isStable = 0;
                }
            }
        }
    }

    public void SetSelected()
    {
        selected = true;
        render.material.color = HexGrid.instance.whenSelected;
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

    public Vector3 ReturnTargetPosition(Vector3 charaPos)
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

    public void DestroyMeself()
    {
        Destroy(gameObject);
        Destroy(this);
    }
}