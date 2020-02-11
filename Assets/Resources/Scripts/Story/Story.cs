using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Story
{
    [SerializeField] string description = "";
    [SerializeField] List<Dialogue> dialogues = null;
    [SerializeField] List<Character> enemies = null;

    int current = 0;
    bool playingCutScene = false;

    public void Update()
    {
        dialogues[current].Update();

        // Replace Input commands with Input script functions as soon as possible!!!
        if (Input.GetKeyDown(KeyCode.Mouse0))
            if (StorySystem.instance.CheckTimer())
                if (!playingCutScene)
                    NextDialogue();
    }

    // Plays current dialogue
    public void PlayDialogue()
    {
        if (current == 0)
            StorySystem.instance.SetSideBool(dialogues[current].right);
        dialogues[current].PlayDialogue();
    }

    // Plays next Dialogue
    void NextDialogue()
    {
        current += 1;
        if (current >= dialogues.Count) 
            StorySystem.instance.EndStory();
        else
            dialogues[current].PlayDialogue();
    }

    // Plays cutscene if the boolean is true
    public void CutScene()
    {
        playingCutScene = true;
    }
}
