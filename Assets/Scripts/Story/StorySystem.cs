using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

[System.Serializable]
public class StorySystem : MonoBehaviour
{
    public static StorySystem instance = null;
    [SerializeField] Sprite transparent;
    [SerializeField] Image portraitLHS;
    [SerializeField] Animator dialogueLHS;
    [SerializeField] Text speakerLHS;
    [SerializeField] Text textLHS;
    [SerializeField] Image portraitRHS;
    [SerializeField] Animator dialogueRHS;
    [SerializeField] Text speakerRHS;
    [SerializeField] Text textRHS;
    [SerializeField] List<Story> stories;
    static int current = 0;
    static bool playing = false;
    static bool sideVal = false;
    static bool noMoreStories = false;

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
        ResetExitTrigger();
        if(playing)
            stories[current].Update();
    }

    void CheckCurrentTrigger()
    {

    }

    public void PlayStory()
    {
        if (!noMoreStories)
        {
            playing = true;
            stories[current].PlayDialogue();
        }
        else
            Debug.Log("NO MORE STORIES FOR YOU");
    }

    public void EndStory()
    {
        DialogueBoxClose(sideVal);
        playing = false;
        if (current < stories.Count - 1)
            current++;
        else
            noMoreStories = true;
        Debug.Log(current);
    }

    public void LoadStoryState()
    {
        Debug.Log("Accessing current trigger");
    }

    public void DialogueBoxOpen(string speaker, string text, bool right)
    {
        DialogueBoxText(speaker, text, right);

        if (right != sideVal)
        {
            if (right)
            {
                dialogueRHS.SetTrigger("EnterBox");
            }
            else
            {
                dialogueLHS.SetTrigger("EnterBox");
            }
            sideVal = right;

            DialogueBoxClose(!right);
        }
        else
        {
            if (right)
                dialogueRHS.SetTrigger("SameSideAgain");
            else
                dialogueLHS.SetTrigger("SameSideAgain");
            Debug.Log("SAME SIDE BUB");
        }
    }

    public void DialogueBoxClose(bool right)
    {
        if (right)
            dialogueRHS.SetBool("ExitBox", true);
        else
            dialogueLHS.SetBool("ExitBox", true);
        ClearSprites();
    }

    public void ResetExitTrigger()
    {
        if (dialogueRHS.GetBool("ExitBox"))
            dialogueRHS.SetBool("ExitBox", false);
        if (dialogueLHS.GetBool("ExitBox"))
            dialogueLHS.SetBool("ExitBox", false);
    }

    /// <summary>
    /// This is to make sure Animation plays correctly for the Dialogue boxes
    /// </summary>
    /// <param name="right"></param>
    public void SetSideBool(bool right)
    {
        sideVal = !right;
    }

    void DialogueBoxText(string speaker, string text, bool right)
    {
        if (right)
        {
            speakerRHS.text = speaker;
            textRHS.text = text;
        }
        else
        {
            speakerLHS.text = speaker;
            textLHS.text = text;
        }
    }

    public void AssignSprite(Sprite sprite, bool right)
    {
        ClearSprites();

        if (right)
            portraitRHS.sprite = sprite;
        else
            portraitLHS.sprite = sprite;
    }

    public void ClearSprites()
    {
        portraitRHS.sprite = transparent;
        portraitLHS.sprite = transparent;
    }
}
