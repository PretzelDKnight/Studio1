using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public bool able = true;

    Rigidbody rb;

    float remainDist;
    public float slowingRadius;
    public float moveSpeed;
    public LayerMask layer;

    Vector3 destination;
    Vector3 moveVector;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        able = true;
    }

    void Update()
    {
        remainDist = Vector3.Distance(destination, transform.position);

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

        float decelerationFactor = distance / 5;

        float speed = moveSpeed * decelerationFactor;

        Vector3 moveVector = desiredVelocity.normalized * Time.deltaTime * speed; //Calculating the steering vector

        rb.AddForce(moveVector);
        transform.LookAt(destination);

        //Clamping the velocity by remaining distance
        if (distance < remainDist)
        {
            desiredVelocity = Vector3.ClampMagnitude(desiredVelocity * distance, moveSpeed);
        }

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
