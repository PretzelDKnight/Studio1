using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SINGLETON CLASS!
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
    Story currentStory = null;
    bool mainStory = false;

    public float storyEndWaitTime = 1f;

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
        if (playing)
        {
            ResetExitTrigger();
            exitTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0))
                if (CheckTimer())
                        currentStory.NextDialogue();
        }

        if (BattleManager.Battle)
            this.gameObject.SetActive(false);
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

    // Function to play story and raise relevant event flags
    public void PlayStory()
    {
        if (!noMoreStories && !playing)
        {
            playing = true;
            currentStory = stories[current];
            currentStory.PlayDialogue();
            mainStory = true;
        }
    }

    public void PlayStory(Story sideStory)
    {
        if (!playing)
        {
            playing = true;
            currentStory = sideStory;
            currentStory.PlayDialogue();
        }
    }

    // Function to end story and raise relevent event flags
    public void EndStory()
    {
        if (playing)
        {
            DialogueBoxClose(sideVal);
            if (mainStory)
            {
                if (current < stories.Count - 1)
                    current++;
                else
                    noMoreStories = true;
                mainStory = false;
            }
            StartCoroutine(StoryEndInvoke());
        }
    }

    // Loads current story state from the save file
    public void LoadStoryState()
    {
        // Insert Loading Story state from save file code snippet!!!



    }

    // Plays current Dialogue of the story
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
            BattleManager.instance.StartBattle();
        }
    }

    // Closes the dialogue box on a specific side
    public void DialogueBoxClose(bool right)
    {
        if (right)
            dialogueRHS.SetBool("ExitBox", true);
        else
            dialogueLHS.SetBool("ExitBox", true);
        ClearSprites();
    }

    // Resets exit trigger for the animation controller of both the dialogue boxes
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

    // Function to initialize the text, speaker and direction of the dialogue box
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

    // Function to assign sprite to the required position relevant to dialogue box direction
    public void AssignSprite(Sprite sprite, bool right)
    {
        ClearSprites();

        if (right)
            portraitRHS.sprite = sprite;
        else
            portraitLHS.sprite = sprite;
    }

    // Function to set images of the sprites back to transparent
    public void ClearSprites()
    {
        portraitRHS.sprite = transparent;
        portraitLHS.sprite = transparent;
    }

    // Function to check exitTime of the dialogue boxes
    public bool CheckTimer()
    {
        if (exitTime >= boxTimeGap)
        {
            exitTime = 0;
            return true;
        }
        return false;
    }

    // Get set for battle boolean
    public bool Battle
    {
        get { return battle; }
        set { battle = value; }
    }

    // Coroutine to raise story end event flag after said time
    IEnumerator StoryEndInvoke()
    {
        yield return new WaitForSeconds(storyEndWaitTime);
        playing = false;
    }

    public bool StoryPlaying()
    {
        return playing;
    }
}