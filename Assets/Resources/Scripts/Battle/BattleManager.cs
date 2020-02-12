using UnityEngine;

enum State
{
    Attack,
    Skill1,
    Skill2,
    Move
}

// SINGLETON CLASS!
public class BattleManager : MonoBehaviour
{
    static public BattleManager instance = null;

    public CharacterVariable currentChar;

    [SerializeField] LayerMask layerMask;
    static bool battle = false;
    bool busy = false;
    static State state;
    static HexTile targetTile;
    static Character targetChara;

    [SerializeField] public Color whenHovered = Color.white;
    [SerializeField] public Color whenTargetable = Color.white;
    [SerializeField] public Color whenSelected = Color.white;
    [SerializeField] public Color whenNormal = Color.white;

    public GameEvent resetTiles;
    public GameEvent battleStart;
    public GameEvent endChara;

    bool interaction = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        state = State.Move;
    }

    private void LateUpdate()
    {
        if (!busy && interaction)
            MouseFunction();
    }

    // Mouse functions for battle phase
    public void MouseFunction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit,1000,layerMask))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Tile")
            {
                HexTile temp = hit.transform.GetComponent<HexTile>();
                temp.Hovered = true;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (targetTile == null)
                    {
                        if (temp.Walkable && state == State.Move)
                        {
                            targetTile = temp;
                            targetTile.SetSelected();
                            RecievedInput();
                        }
                    }
                }
            }
            else if (hit.transform.tag == "Enemy" && currentChar.character.tag == "Ally" && state == State.Attack)
            {

            }
        }
    }

    // Executes the function depending on state of the battle manager
    void RecievedInput()
    {
        busy = true;
        switch (state)
        {
            case State.Attack:
                currentChar.character.Attack(targetChara);
                break;
            case State.Move:
                StartCoroutine(currentChar.character.MoveDownPath(Pathfinder.instance.FindPath(currentChar.character.GetCurrentTile(), targetTile)));
                currentChar.character.energy.runTimeValue -= targetTile.energyCost;
                break;
            case State.Skill1:
                break;
            case State.Skill2:
                break;
            default:
                break;
        }
    }

    // Attack function to change state of battle manager functions
    public void Attack()
    {
        state = State.Attack;
    }

    // Skill 1 Function to change state of battle manager functions
    public void Skill1()
    {
        state = State.Skill1;
    }

    // Skill 2 Function to change state of battle manager functions
    public void Skill2()
    {
        state = State.Skill2;
    }

    // Move function to change state of battle manager functions
    public void Move()
    {
        state = State.Move;
        Pathfinder.instance.FindSelectableTiles(currentChar.character);
    }

    // Pass to send the turn to the next character in queue
    public void Pass()
    {
        currentChar.character.SetNotShown();
        endChara.Raise();
    }

    // Get set battle function
    public bool Battle
    {
        get { return battle; }
        set { battle = value; }
    }

    // Starts the battle after event is raised
    public void StartBattle()
    {
        if (!Battle)
        {
            Battle = true;
            busy = true;
            battleStart.Raise();
        }
        else
            Debug.Log("Battle already running!");
    }

    // Is raised when a new character enters the Battle manager
    public void NewChara()
    {
        currentChar.character.RefreshEnergy();
        currentChar.character.SetShown();
        ResetEverything();
        Move();
    }

    // If the same character has energy left and is making a move after having done a move, an event raises this function
    public void NextMove()
    {
        ResetEverything();

        if (currentChar.character.energy.runTimeValue != 0)
            Move();
        else
            Pass();
    }

    // Resets the all targeting values to null and raises events
    public void ResetEverything()
    {
        targetChara = null;
        targetTile = null;
        resetTiles.Raise();
        busy = false;
    }

    // Sets interaction with battle manager to false when story or other events are raised in battle
    public void SetInteractionFalse()
    {
        interaction = false;
    }

    // Sets interaction with battle manager back to true after said story
    public void SetInteractionTrue()
    {
        interaction = true;
    }
}