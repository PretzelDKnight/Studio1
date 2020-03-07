using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public bool able = true;

    Rigidbody rb;

    float remainDist;
    public float slowingRadius;
    public float moveSpeed;
    public float rotSpeed;
    public LayerMask layer;

    bool moving;

    Vector3 destination;
    Vector3 lookAtTarget;
    Quaternion playerRot;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        able = true;
    }

    void Update()
    {
        remainDist = Vector3.Distance(destination, transform.position);

        if (!BattleManager.Battle)
        {
            if (Input.GetMouseButtonDown(0))
            {                
                    SetDestination();
            }
            if (moving)
                Move();
        }
    }

    void SetDestination()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            destination = hit.point;
            lookAtTarget = new Vector3(destination.x - transform.position.x, transform.position.y, destination.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            moving = true;
        }
    }

    public void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        if (transform.position == destination)
            moving = false;
    }
}
