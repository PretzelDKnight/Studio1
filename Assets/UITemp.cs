using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITemp : MonoBehaviour
{
    public GameObject storyTrigger;

    // Start is called before the first frame update
    void Start()
    {
        storyTrigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StoryTrigger")
        {
            Debug.Log(other.gameObject.tag);
            StorySystem.instance.PlayStory();
        }
    }
}
