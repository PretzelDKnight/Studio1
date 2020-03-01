using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIScript : MonoBehaviour
{
    [SerializeField] GameObject battleUI;

    private void Start()
    {
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
    }
}
