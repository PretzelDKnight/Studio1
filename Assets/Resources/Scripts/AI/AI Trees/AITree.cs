using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITree : MonoBehaviour
{
    public static AITree instance = null;
    
    public AITreeNode rootNode;

    public static Character AIstarget;
    void Start()
    {
        instance = this;
        rootNode = new Selector(new List<AITreeNode> { new Sequence(new List<AITreeNode> { new HasEnergy(), new Selector(new List<AITreeNode> { new AttackMostFatal(new Selector(null)), new AttackLowest(null), new AttackNearest(null) }) }), new DoNothing() });
    }

    public void Update()
    {
        rootNode.Execute();
    }
}
