using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    [SerializeField] string speaker;
    GameObject portrait;
    [SerializeField] bool right = false;
    [SerializeField] string text;

    [SerializeField] bool triggerBattle;

    public void Update()
    {

    }

    public void PlayDialogue()
    {
        StorySystem.DialogueBoxOpen(right);
    }

    public void CloseDialogue()
    {
        StorySystem.DialogueBoxClose(right);
    }

    public void PlayText()
    {

    }
}
