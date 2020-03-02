﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public bool able = true;
    [HideInInspector] public bool storyInteract = true;
    [HideInInspector] public bool battleInteract = true;
    
    Rigidbody rb;

    public float moveDisPerSec = 1;
    public LayerMask layer;

    Vector3 destination;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        able = true;
        destination = transform.position;
    }

    void Update()
    {
        if (!BattleManager.Battle)
            GetNewLocation();
    }

    void GetNewLocation()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100) && hit.transform.gameObject.layer == layer)
            {
                destination = hit.point;
                Debug.Log("Moving!");
            }
        }
    }

    private void FixedUpdate()
    {
        float distanceToMove = Vector3.Distance(transform.position, destination);
        if (distanceToMove > 0)
        {
            float moveDistance = Mathf.Clamp(moveDisPerSec * Time.fixedDeltaTime, 0, distanceToMove);

            Vector3 move = (destination - transform.position).normalized * moveDistance;

            rb.AddForce(move);
        }
    }
}
