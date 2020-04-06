using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIScript : MonoBehaviour
{
    public static BattleUIScript instance = null;

    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject moveButton;
    [SerializeField] GameObject BAButton;
    [SerializeField] GameObject S1Button;
    [SerializeField] GameObject S2Button;
    
    public Text tempUIforInfo;

    private void Start()
    {
        instance = this;
        battleUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.Battle && !battleUI.activeSelf)
        {
            battleUI.SetActive(true);
        }
        else if (!BattleManager.Battle && battleUI.activeSelf)
        {
            battleUI.SetActive(false);
        }

        if(BattleManager.Battle && battleUI.activeSelf)
        {
            if (BattleManager.instance.currentChar.energy.runTimeValue < BattleManager.instance.currentChar.Skill2Energy())
                S2Button.SetActive(false);
            if (BattleManager.instance.currentChar.energy.runTimeValue < BattleManager.instance.currentChar.Skill1Energy())
                S1Button.SetActive(false);
            if (BattleManager.instance.currentChar.energy.runTimeValue < BattleManager.instance.currentChar.AttackEnergy())
                BAButton.SetActive(false);
            if (BattleManager.instance.currentChar.energy.runTimeValue < BattleManager.instance.currentChar.MoveEnergy())
                moveButton.SetActive(false);
        }

    }

    public void ResetUI()
    {
        moveButton.SetActive(true);
        BAButton.SetActive(true);
        S1Button.SetActive(true);
        S2Button.SetActive(true);
    }
}
