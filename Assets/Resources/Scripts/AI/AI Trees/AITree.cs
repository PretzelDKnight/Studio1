using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITree
{
    public static AITree instance = null;
    
    public AITreeNode rootNode;

    public static Character AIstarget;

    public static HexTile tileToMoveTo;

    public AITree(AITreeNode node)
    {
        rootNode = node;
    }

    void Start()
    {
        instance = this;
    }
    
    public void Execute()
    {
        rootNode.Execute();
    }
}
