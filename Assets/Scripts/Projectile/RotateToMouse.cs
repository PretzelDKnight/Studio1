using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera cam;
    public float MaximumLength;

    private Ray rayMouse;
    private Vector3 position;
    private Vector3 direction;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            RaycastHit hit;
            var mousePosition = Input.mousePosition;
            rayMouse = cam.ScreenPointToRay(mousePosition);

            if(Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, MaximumLength))
            {
                RotateToMouseDirection(gameObject, hit.point);
            }
            else
            {
                var position = rayMouse.GetPoint(MaximumLength);
                RotateToMouseDirection(gameObject, position);
            }
        }

        else
        {
            Debug.Log("No Camera");
        }
    }

    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);

        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    public Quaternion GetRotation()
    {
        return rotation;
    }
}
