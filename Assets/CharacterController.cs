using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public bool able = true;

    Rigidbody rb;

    public float maxVelocity = 5f;
    public float maxAcceleration = 10f;
    public float targetRadius = 0.005f;     // The radius from the target that means we are close enough and have arrived
    public float timeToTarget = 0.1f;       // The time in which we want to achieve the targetSpeed
    public float slowingRadius = 1f;        // Radius for slowing zone

    public LayerMask layer;

    Vector3 destination;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        able = true;
    }

    void Update()
    {
        if (!BattleManager.Battle)
            Move();
    }

    void Move()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, layer))
            {
                rb.velocity = Vector3.zero;
                destination = hit.point;
                Arrive();
            }
        }
    }

    public void Arrive()
    {
        Vector3 desiredVelocity = destination - transform.position; //Desired velocity calculation

        desiredVelocity.y = 0;

        float distance = desiredVelocity.magnitude;

        //___________________________________________________________________________________________________________________
        //|#OLD CODE--------------------------------------------------------------------------------------------------------|
        //|float decelerationFactor = distance / 5;                                                                         |
        //|                                                                                                                 |
        //|float speed = moveSpeed * decelerationFactor;                                                                    |
        //|                                                                                                                 |
        //|Vector3 moveVector = desiredVelocity.normalized * Time.deltaTime * speed; //Calculating the steering vector      |
        //|_________________________________________________________________________________________________________________|

        if (distance < slowingRadius)
        {
            rb.velocity = Vector3.zero;
        }

        /* Calculate the target speed, full speed at slowRadius distance and 0 speed at 0 distance */
        float targetSpeed;
        if (distance > slowingRadius)
        {
            targetSpeed = maxVelocity;
        }
        else
        {
            targetSpeed = maxVelocity * (distance / slowingRadius);
        }

        /* Give desiredVelocity the correct speed */
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;

        /* Calculate the linear acceleration we want */
        Vector3 acceleration = desiredVelocity - rb.velocity;

        /* Rather than accelerate the character to the correct speed in 1 second, accelerate so we reach the desired speed in timeToTarget seconds, as if we were to accelerate for the whole timeToTarget seconds */
        acceleration *= 1 / timeToTarget;

        /* Make sure we are accelerating at max acceleration */
        if (acceleration.magnitude > maxAcceleration)
        {
            acceleration.Normalize();
            acceleration *= maxAcceleration;
        }

        rb.AddForce(acceleration);

        transform.LookAt(destination);

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        transform.rotation = Quaternion.Euler(rotation);
    }
}