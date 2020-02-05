using System.Collections;
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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        GenerateHexGrid();
    }

    private void Update()
    {
        
    }

    void GenerateHexGrid()
    {
        float unitLength;
        if (useAsInnerCircleRadius)
            unitLength = (radius / (Mathf.Sqrt(3) / 2));
        else
            unitLength = radius;
        offsetX = unitLength * Mathf.Sqrt(3);
        offsetZ = unitLength * 1.5f;

        for (int i = -x/2; i < x/2; i++)
        {
            for (int j = -z/2; j < z/2; j++)
            {
                Vector2 hexpos = HexOffset(i, j);
                Vector3 pos = new Vector3(hexpos.x, transform.position.y, hexpos.y);
                Instantiate(hexTile, pos, Quaternion.identity);
            }
        }
    }

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

    public List<Vector3> Directions(Transform trans)
    {
        List<Vector3> dir = new List<Vector3>();

        Vector3 temp;
        // 0 degrees face
        dir.Add(trans.right);
        // 60 degrees face
        temp = trans.InverseTransformDirection(new Vector3(Mathf.Tan(60), 0, Mathf.Tan(60)));
        dir.Add(temp);
        // 120 degrees face
        temp = trans.InverseTransformDirection(new Vector3(-Mathf.Tan(60), 0, Mathf.Tan(60)));
        dir.Add(temp);
        // 180 degrees face
        dir.Add(-trans.right);
        // 240 degrees face
        temp = trans.InverseTransformDirection(new Vector3(-Mathf.Tan(60), 0, -Mathf.Tan(60)));
        dir.Add(temp);
        // 300 degrees face
        temp = trans.InverseTransformDirection(new Vector3(Mathf.Tan(60), 0, Mathf.Tan(60)));
        dir.Add(temp);

        return dir;
    }
}
