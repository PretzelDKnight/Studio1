using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float charge = 0;
    public float MaxCharge = 100;
    public float MinCharge = 30;
    public float chargeSpeed = 1;

    public float speed = 10;

    public AbstractState currentState;

    public List<Transform> patrolPoints;

    public Transform rechargePoint;

    // Start is called before the first frame update
    void Start()
    {
        NextState(new Recharge());
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Function();
    }

    public void NextState(AbstractState state)
    {
        currentState = state;
        currentState.AssignAgent(this);
    }
}
