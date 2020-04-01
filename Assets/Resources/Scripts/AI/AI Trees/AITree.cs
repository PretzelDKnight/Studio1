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
        rootNode = new Selector(new List<AITreeNode> { new Sequence(new List<AITreeNode> { new HasEnergy(), new Selector(new List<AITreeNode> {
            new AttackMostFatal(new Selector(new List<AITreeNode> { new HasS2Energy(new InS2Range(new AIS2())), new HasS1Energy(new InS1Range(new AIS1())),
                new HasBAEnergy(new InBARange(new AIBasic())), new HasMoveBAEnergy(new MoveBA()) })), new AttackLowest(new Selector(new List<AITreeNode>
                { new HasS2Energy(new InS2Range(new AIS2())), new HasS1Energy(new InS1Range(new AIS1())), new HasBAEnergy(new InBARange(new AIBasic())),
                    new HasMoveBAEnergy(new MoveBA()) })), new AttackNearest(new Selector(new List<AITreeNode> { new HasS2Energy(new InS2Range(new AIS2())),
                        new HasS1Energy(new InS1Range(new AIS1())), new HasBAEnergy(new InBARange(new AIBasic())), new HasMoveBAEnergy(new MoveBA()) })) }) }), new DoNothing() });
    }
    
    public void Execute()
    {
        rootNode.Execute();
    }
}
