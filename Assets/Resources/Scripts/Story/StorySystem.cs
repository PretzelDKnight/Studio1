using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

[System.Serializable]
public class StorySystem : MonoBehaviour
{
    // Singleton Instance
    public static StorySystem instance = null;

    // Objects which StorySystem can change values for
    [SerializeField] float boxTimeGap = 0.6f;
    [SerializeField] Font font = null;
    [SerializeField] Color fontColor = Color.white;
    [SerializeField] int speakerFontSize = 24;
    [SerializeField] int textFontSize = 24;
    [SerializeField] Sprite transparent = null;
    [SerializeField] Image portraitLHS = null;
    [SerializeField] Animator dialogueLHS = null;
    [SerializeField] Text speakerLHS = null;
    [SerializeField] Text textLHS = null;
    [SerializeField] Image portraitRHS = null;
    [SerializeField] Animator dialogueRHS = null;
    [SerializeField] Text speakerRHS = null;
    [SerializeField] Text textRHS = null;
    [SerializeField] List<Story> stories = null;

    public GameEvent storyStart;  // Input streamlining while in story
    public GameEvent storyEnd;

    // StorySystem centric variables
    static float exitTime = 0;
    static int current = 0;
    static bool playing = false;
    static bool sideVal = false;
    static bool noMoreStories = false;
    static bool battle = false;

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
        Initialize();
    }

    public void Update()
    {
        ResetExitTrigger();
        if (playing)
        {
            exitTime += Time.deltaTime;
            stories[current].Update();
        }
    }

    public bool CheckStoryCall(Story story)
    {
        if (stories[current] == story)
        {
            PlayStory();
            return true;
        }
        return false;
    }

    // Adds in the fonts and values that go in at game start
    private void Initialize()
    {
        speakerLHS.font = font;
        speakerLHS.color = fontColor;
        speakerLHS.fontSize = speakerFontSize;

        speakerRHS.font = font;
        speakerRHS.color = fontColor;
        speakerRHS.fontSize = speakerFontSize;

        textLHS.font = font;
        textLHS.color = fontColor;
        textLHS.fontSize = textFontSize;

        textRHS.font = font;
        textRHS.color = fontColor;
        textRHS.fontSize = textFontSize;
    }

    public void PlayStory()
    {
        if (!noMoreStories)
        {
            playing = true;
            storyStart.Raise();
            stories[current].PlayDialogue();
        }
    }

    public void EndStory()
    {
        DialogueBoxClose(sideVal);
        playing = false;
        if (current < stories.Count - 1)
            current++;
        else
            noMoreStories = true;
        storyEnd.Raise();
    }

    public void LoadStoryState()
    {
        // Insert Loading Story state from save file code snippet!!!



    }

    public void DialogueBoxOpen(string speaker, string text, bool right)
    {
        if (!Battle)
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
            }
        }
        else
        {
            DialogueBoxClose(right);
            // Insert Initiate Battle Manager Code Snippet!!!


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

    public bool CheckTimer()
    {
        if (exitTime >= boxTimeGap)
        {
            exitTime = 0;
            return true;
        }
        return false;
    }

    public bool Battle
    {
        get
        {
            return battle;
        }
        set
        {
            battle = value;
        }
    }
}