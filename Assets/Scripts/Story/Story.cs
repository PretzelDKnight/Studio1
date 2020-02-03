﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Story
{
    [SerializeField] string description;
    [SerializeField] public List<Dialogue> dialogues;
    [SerializeField] List<Character> enemies;

    int current = 0;

    public void Update()
    {
        dialogues[current].Update();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            if(StorySystem.instance.CheckTimer())
                NextDialogue();
    }

    public void PlayDialogue()
    {
        if (current == 0)
            StorySystem.instance.SetSideBool(dialogues[current].right);
        dialogues[current].PlayDialogue();
    }

    void NextDialogue()
    {
        current += 1;
        if (current >= dialogues.Count) 
            StorySystem.instance.EndStory();
        else
            dialogues[current].PlayDialogue();
    }
}
