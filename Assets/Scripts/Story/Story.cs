using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Story
{
    [SerializeField] string description;

    [SerializeField] public List<Dialogue> dialogues;

    [SerializeField] public UnityEvent trigger;

    [SerializeField] List<Character> enemies;

    int current = 0;

    public void Update()
    {
        dialogues[current].Update();
    }

    public void PlayDialogue()
    {
        dialogues[current].PlayDialogue();
    }

    void NextDialogue()
    {
        dialogues[current].CloseDialogue();
        current += 1;
        if (current > dialogues.Count)
            StorySystem.EndStory();
        else
            dialogues[current].PlayDialogue();
    }
}
