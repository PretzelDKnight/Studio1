using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITemp : MonoBehaviour
{
    public GameObject storyButton;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        storyButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StoryTrigger")
        {
            animator.ResetTrigger("isMoving");
            animator.SetTrigger("notMoving");
            StorySystem.instance.PlayStory();
        }
    }
}
