using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState
{
    protected Agent agent;

    public AbstractState nextState;

    public void AssignAgent(Agent a)
    {
        agent = a;
    }

    public abstract void Function();

    public abstract bool CheckTransitions();

    public virtual void MoveTo(Transform transform)
    {
        agent.transform.position = Vector3.MoveTowards(agent.transform.position, transform.position, agent.speed);
        agent.charge -= Time.deltaTime;
    }
}
