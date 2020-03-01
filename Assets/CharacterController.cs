using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Vector3 forward, right;

    [HideInInspector] public bool able = true;

    [HideInInspector] public bool interactionS = true;
    [HideInInspector] public bool interactionB = true;
    
    Rigidbody rb;

    CameraScript owCam;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        owCam = Camera.main.gameObject.GetComponent<CameraScript>();
        able = true;
    }

    void Update()
    {
        if (rb.velocity == Vector3.zero)
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z));
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (interactionS && interactionB && able)
                Move();
        }
        else
            rb.velocity = Vector3.zero;
    }

    public void Move()
    {
        rb.velocity = Vector3.zero;

        Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
        Vector3 rightMove = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 forwardMove = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        Vector3 unison = Vector3.Normalize(rightMove + forwardMove);

        rb.velocity = transform.forward;

        transform.forward = unison;
        rb.velocity += rightMove;
        rb.velocity += forwardMove;
    }
    
    public void StoryEventStart()
    {
        interactionS = false;
    }

    public void StoryEventEnd()
    {
        interactionS = true;
    }

    public void BattleEventStart()
    {
        interactionB = false;
    }
    public void BattleEventEnd()
    {
        interactionB = true;
    }
}
