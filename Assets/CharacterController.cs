using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public bool able = true;

    Rigidbody rb;

    float remainDist;
    public float slowingRadius;
    public float moveSpeed;
    public float rotSpeed;

    bool moving;

    Vector3 destination;

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

        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Enviro")))
        {
            destination = hit.point;
            moving = true;
        }
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        if (transform.position == destination)
            moving = false;
    }
}
