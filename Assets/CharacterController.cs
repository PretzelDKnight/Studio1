using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public bool able = true;
    bool moving;

    Animator animator;

    Rigidbody rb;

    float destDist;
    public float speed = 5f;
    public float rotSpeed = 1f;

    public LayerMask layer;

    Vector3 destination;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        able = true;
        destination = transform.position;
        moving = false;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!BattleManager.Battle && !StorySystem.instance.StoryPlaying())
        {
            Move();
        }
    }

    private void FixedUpdate()
    {
        if (!BattleManager.Battle && !StorySystem.instance.StoryPlaying())
        {
            MoveTowards();
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
                //transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                moving = true;
            }
        }
    }

    public void MoveTowards()
    {
        if (moving)
        {
            destDist = Vector3.Distance(destination, transform.position);

            if (destDist > 2f)
            {
                moving = true;
                animator.SetTrigger("isMoving");
                animator.ResetTrigger("notMoving");
            }
            else
            {
                moving = false;
                Debug.Log(animator.GetBool("notMoving"));
                animator.SetTrigger("notMoving");
                animator.ResetTrigger("isMoving");
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position,destination, speed * Time.deltaTime);

            Vector3 dir = destination - transform.position;
            dir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);
        }
    }
}