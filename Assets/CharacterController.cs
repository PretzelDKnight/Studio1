using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public bool able = true;

    Rigidbody rb;

    public float moveSpeed;

    bool moving;

    Vector3 destination;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        able = true;
    }

    void Update()
    {
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

        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Enviro")))
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
