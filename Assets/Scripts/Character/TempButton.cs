using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempButton : MonoBehaviour
{
    public Character enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Battle()
    {
        BattleManager.instance.StartBattle(new List<Character> { enemy });
    }
}
