using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITemp : MonoBehaviour
{
    public GameObject storyButton;

    // Start is called before the first frame update
    void Start()
    {
        storyButton.SetActive(false);
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
