using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tempstatui : MonoBehaviour
{
    Text tempTextforStats;
    
    // Start is called before the first frame update
    void Start()
    {
        tempTextforStats = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tempTextforStats.text = "Current character: " + BattleManager.instance.currentChar.name + "\n" + "Health: " + BattleManager.instance.currentChar.health.runTimeValue + "\n" + "Energy:" + BattleManager.instance.currentChar.energy.runTimeValue;
    }
}
