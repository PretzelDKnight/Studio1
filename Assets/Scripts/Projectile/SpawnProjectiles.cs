using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    public GameObject FirePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public RotateToMouse rotateToMouse;

    private GameObject effectToSpawn;
    private float TimeToFire;

    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && Time.time >= TimeToFire)
        {
            TimeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().FireRate;
            SpawnVFX();
        }
    }

    void SpawnVFX()
    {
        GameObject vfx;

        if (FirePoint != null)
        {
            vfx = Instantiate(effectToSpawn, FirePoint.transform.position, Quaternion.identity);

            if (rotateToMouse != null)
            {
                vfx.transform.localRotation = rotateToMouse.GetRotation();
            }
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }
}
