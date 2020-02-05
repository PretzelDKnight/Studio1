using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : Controller
{
    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        HoverTile();
    }

    public override void ReadInput(InputData data)
    {
        throw new System.NotImplementedException();
    }

    public void HoverTile()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, layerMask))
            if (hit.transform.tag == "Tile")
                hit.transform.GetComponent<HexTile>().Hovered = true;

        Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.red);
    }
}
