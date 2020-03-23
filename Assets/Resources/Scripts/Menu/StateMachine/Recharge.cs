using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge : AbstractState
{
    // Update is called once per frame
    public override bool CheckTransitions()
    {
        return (agent.charge >= agent.MaxCharge);
    }

    public override void Function()
    {
        if (CheckTransitions())
            agent.NextState(new Patrolling());

        if (agent.transform.position != agent.rechargePoint.position)
            MoveTo(agent.rechargePoint);
        else
            agent.charge += Time.deltaTime * agent.chargeSpeed;
    }
}
