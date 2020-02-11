using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnterStoryTrigger : MonoBehaviour
{
    [SerializeField] Story story;

    CharacterController characterController;

    public
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StoryTrigger")
        {
            Debug.Log("Story is being triggered!");
            StorySystem.instance.PlayStory();
        }
    }
}
