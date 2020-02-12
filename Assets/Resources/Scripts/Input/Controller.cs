using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Controller : MonoBehaviour
{
    // TODO: Add method to read input
    public abstract void ReadInput(InputData data);

    protected Rigidbody rb;
    protected Collider coll;
    protected bool newInput;

    // Animation Component
    protected Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }
}
