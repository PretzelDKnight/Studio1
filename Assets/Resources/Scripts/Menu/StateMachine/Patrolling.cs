using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : AbstractState
{
    int currentDestIndex;

    // Update is called once per frame
    public override bool CheckTransitions()
    {
        return (agent.charge <= agent.MinCharge);
    }

    public override void Function()
    {
        if (CheckTransitions())
            agent.NextState(new Recharge());

        if (agent.transform.position != agent.patrolPoints[currentDestIndex].position)
            MoveTo(agent.patrolPoints[currentDestIndex]);
        else
            currentDestIndex = ++currentDestIndex % agent.patrolPoints.Count;
    }
}
