﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Vector3 forward, right;

    [HideInInspector] public bool interactionS = true;
    [HideInInspector] public bool interactionB = true;
    
    Rigidbody rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && interactionS && interactionB)
            Move();
    }

    public void Move()
    {
        {
            Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
            Vector3 rightMove = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
            Vector3 forwardMove = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

            Vector3 unison = Vector3.Normalize(rightMove + forwardMove);

            {
                transform.forward = unison;
                transform.position += rightMove;
                transform.position += forwardMove;
            }
        }
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
