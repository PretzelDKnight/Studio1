using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[System.Serializable]
public class StorySystem : MonoBehaviour
{
    private static StorySystem instance = null;

    [SerializeField] static List<Story> stories;

    [SerializeField] static Animator dialogueLHS;
    [SerializeField] static Animator dialogueRHS;

    UnityEvent currentEvent;

    public event EventHandler eventHandler;

    static int current = 0;

    static bool playing = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        LoadStoryState();
    }

    public void Update()
    {
        if(playing)
            stories[current].Update();
    }

    void CheckCurrentTrigger()
    {

    }

    static public void PlayStory()
    {
        playing = true;
        stories[current].PlayDialogue();
    }

    static public void EndStory()
    {
        playing = false;
        current++;
    }

    public void LoadStoryState()
    {
        currentEvent = stories[current].trigger;
    }

    static public void DialogueBoxOpen(bool right)
    {
        if (right)
            dialogueRHS.SetTrigger("EnterBox");
        else
            dialogueLHS.SetTrigger("EnterBox");
    }

    static public void DialogueBoxClose(bool right)
    {
        if (right)
            dialogueRHS.SetTrigger("ExitBox");
        else
            dialogueLHS.SetTrigger("ExitBox");
    }
}
