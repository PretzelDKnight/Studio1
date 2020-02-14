using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public static HexGrid instance = null;
    public GameObject hexTile;
    public int x = 5;
    public int z = 5;
    public float radius = 0.5f;
    public bool useAsInnerCircleRadius = true;
    private float offsetX, offsetZ;

    // Tile Color
    static public Color normal;
    public Color whenSelected = Color.white;
    public Color whenHovered = Color.white;
    public Color whenWalkable = Color.white;
    public Color whenAttackable = Color.white;

    static float hoverRate = 2;
    static float time = 0;
    static float lerpValue = 0;
    static bool change = false;

    public LayerMask layerMask;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        normal = hexTile.GetComponent<MeshRenderer>().sharedMaterial.color;
    }

    void Start()
    {

    }

    private void Update()
    {
        ShaderLerp();
    }

    // Generates Hexagon Grid (Can only produce grids of even size as it is generated from the centre of the gameObject)
    public void GenerateHexGrid()
    {
        float unitLength;
        if (useAsInnerCircleRadius)
            unitLength = (radius / (Mathf.Sqrt(3) / 2));
        else
            unitLength = radius;
        offsetX = unitLength * Mathf.Sqrt(3);
        offsetZ = unitLength * 1.5f;

        for (int i = -x / 2; i < x / 2; i++)
        {
            for (int j = -z / 2; j < z / 2; j++)
            {
                Vector2 hexpos = HexOffset(i, j);
                Vector3 pos = new Vector3(hexpos.x + transform.position.x, transform.position.y, hexpos.y + transform.position.z);
                Instantiate(hexTile, pos, Quaternion.identity);
            }
        }
    }

    // Calculates HexOffset in relation to passed values for finding position of generated tile
    Vector2 HexOffset(int x, int z)
    {
        Vector2 position = Vector3.zero;
        if (z % 2 == 0)
        {
            position.x = x * offsetX;
            position.y = z * offsetZ;
        }
        else
        {
            position.x = (x + 0.5f) * offsetX;
            position.y = z * offsetZ;
        }
        return position;
    }

    // Calculates and returns a list of the 6 faces of the hexagon tiles in which neighbours should be checked
    public List<Vector3> Directions(Transform trans)
    {
        List<Vector3> dir = new List<Vector3>();

        Vector3 temp;
        // 0 degrees face
        dir.Add(trans.right);
        // 60 degrees face
        temp = trans.TransformDirection(new Vector3(1.732f, 0, 1.732f));
        dir.Add(temp);
        // 120 degrees face
        temp = trans.TransformDirection(new Vector3(-1.732f, 0, 1.732f));
        dir.Add(temp);
        // 180 degrees face
        dir.Add(-trans.right);
        // 240 degrees face
        temp = trans.TransformDirection(new Vector3(-1.732f, 0, -1.732f));
        dir.Add(temp);
        // 300 degrees face
        temp = trans.TransformDirection(new Vector3(1.732f, 0, -1.732f));
        dir.Add(temp);

        return dir;
    }

    // Shader lerp value function for passing property into shader
    static void ShaderLerp()
    {
        time += Time.deltaTime * hoverRate;

        if (change)
        {
            lerpValue = Mathf.Lerp(0.5f, 1f, time);
            if (time >= 1f)
            {
                time = 0;
                change = false;
            }
        }
        else
        {
            lerpValue = Mathf.Lerp(1f, 0.5f, time);
            if (time >= 1f)
            {
                time = 0;
                change = true;
            }
        }
    }

    // Returns shader lerp value for tiles accessing this value
    static public float LerpValue()
    {
        return lerpValue;
    }
}