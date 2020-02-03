using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [SerializeField] string speaker;
    [SerializeField] Sprite portrait;
    [SerializeField] public bool right = false;
    [SerializeField] string text;
    [SerializeField] bool triggerBattle;

    public void Update()
    {

    }

    public void PlayDialogue()
    {
        StorySystem.instance.DialogueBoxOpen(speaker, text, right);
        StorySystem.instance.AssignSprite(portrait, right);
    }

    public void CloseDialogue()
    {
        StorySystem.instance.DialogueBoxClose(right);
        StorySystem.instance.ClearSprites();
    }
}
