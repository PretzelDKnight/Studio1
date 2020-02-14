using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    // TODO: Add method to read input
    [SerializeField] public LayerMask layerMask;
    public abstract void ReadInput(InputData data);
    protected bool newInput;

    // Animation Component
    protected Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
