using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITemp : MonoBehaviour
{
    public GameObject storyTrigger;
    public GameObject battleTrigger;

    // Start is called before the first frame update
    void Start()
    {
        storyTrigger.SetActive(false);
        battleTrigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StoryTrigger")
        {
            Debug.Log(other.gameObject.tag);
            storyTrigger.SetActive(true);
        }
        if (other.gameObject.tag == "BattleTrigger")
        {
            Debug.Log(other.gameObject.tag);
            battleTrigger.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "StoryTrigger")
        {
            Debug.Log(other.gameObject.tag);
            storyTrigger.SetActive(false);
        }
        if (other.gameObject.tag == "BattleTrigger")
        {
            Debug.Log(other.gameObject.tag);
            battleTrigger.SetActive(false);
        }
    }
}
