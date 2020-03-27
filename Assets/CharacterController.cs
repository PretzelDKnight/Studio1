using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public bool able = true;

    Rigidbody rb;

    public float speed = 5f;
    public float slowingRadius = 1f;        // Radius for slowing zone

    public LayerMask layer;

    Vector3 destination;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        able = true;
        destination = transform.position;
    }

    void Update()
    {
        if (!BattleManager.Battle && !StorySystem.instance.StoryPlaying())
        {
            Move();
        }
    }

    void Move()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, layer))
            {
                destination = hit.point;
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                Arrive();
            }
        }
        else
        {
            Arrive();
        }
    }

    public void Arrive()
    {
        Vector3 distToDest = destination - transform.position;
        Vector3 desiredVel = distToDest.normalized * speed;
        desiredVel.y = 0;
        Vector3 steering = desiredVel - velocity;

        velocity += steering * Time.deltaTime;

        float slowDownFactor = Mathf.Clamp01(distToDest.magnitude / slowingRadius);

        transform.position += velocity * Time.deltaTime;
        velocity *= slowDownFactor;
    }
}