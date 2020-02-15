using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float Speed;
    public float FireRate;
    public GameObject MuzzlePrefab;
    public GameObject HitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (MuzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(MuzzlePrefab,transform.position,Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();

            if(psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Speed != 0)
        {
            transform.position += transform.forward * (Speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;

        if(HitPrefab != null)
        {
            var hitVFX = Instantiate(HitPrefab, position, rotation);

            var HitPS = hitVFX.GetComponent<ParticleSystem>();

            if (HitPS != null)
            {
                Destroy(hitVFX, HitPS.main.duration);
            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }

        Destroy(gameObject);
    }
}
