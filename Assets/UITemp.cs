using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITemp : MonoBehaviour
{
    public GameObject storyButton;

    Animator animator;

    public BattleManager battleObject;

    public Party battle2enemies;
    public Party battle3enemies;
    public Party battle4enemies;

    public Party allies;

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
        if (other.tag == "StoryTrigger2")
        {
            battleObject.enemies = battle2enemies;

            battleObject.transform.position = new Vector3(90.76f, 30.092f, -133.91f);
            
            animator.ResetTrigger("isMoving");
            animator.SetTrigger("notMoving");
            StorySystem.instance.PlayStory();
        }
        if (other.tag == "StoryTrigger3")
        {
            battleObject.enemies = battle2enemies;

            battleObject.transform.position = new Vector3(19.8f, 24.847f, -157.17f);

            animator.ResetTrigger("isMoving");
            animator.SetTrigger("notMoving");
            StorySystem.instance.PlayStory();
        }
        if (other.tag == "StoryTrigger4")
        {
            battleObject.enemies = battle2enemies;

            battleObject.transform.position = new Vector3(259f, 43.521f, -278.5f);

            animator.ResetTrigger("isMoving");
            animator.SetTrigger("notMoving");
            StorySystem.instance.PlayStory();
        }
    }
}
