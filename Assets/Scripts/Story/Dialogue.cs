using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [SerializeField] public bool cutScene = false;
    // [SerializeField] insert scene to load here!!!
    [SerializeField] string speaker = "";
    [SerializeField] Sprite portrait = null;
    [SerializeField] public bool right = false;
    [SerializeField] string text = "";
    [SerializeField] bool triggerBattle = false;

    public void Update()
    {

    }

    public void PlayDialogue()
    {
        if (!cutScene)
        {
            StorySystem.instance.Battle = triggerBattle;
            StorySystem.instance.DialogueBoxOpen(speaker, text, right);
            StorySystem.instance.AssignSprite(portrait, right);
        }
        else
        {
            // Insert Code Snippet to load cutscene scene!!!


        }
    }

    public void CloseDialogue()
    {
        StorySystem.instance.DialogueBoxClose(right);
        StorySystem.instance.ClearSprites();
    }
}
