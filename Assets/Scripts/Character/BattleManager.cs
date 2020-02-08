using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    Attack,
    Skill1,
    Skill2,
    Move
}

public class BattleManager : MonoBehaviour
{
    static public BattleManager instance = null;

    [SerializeField] LayerMask layerMask;
    bool battle = false;
    static List<HexTile> currentList;
    static Character currentChar;
    State state;
    HexTile targetTile;
    Character targetChara;

    [SerializeField] ButtonScript move;
    [SerializeField] ButtonScript attack;
    [SerializeField] ButtonScript skill1;
    [SerializeField] ButtonScript skill2;
    [SerializeField] ButtonScript pass;

    [SerializeField] public Color whenHovered = Color.white;
    [SerializeField] public Color whenTargetable = Color.white;
    [SerializeField] public Color whenSelected = Color.white;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentChar != null)
        {
            ButtonsSet();

            if (currentChar.currentStats.energyPool == 0)
            {
                TurnManager.EndTurn();
            }
        }
    }

    private void LateUpdate()
    {
        MouseFunction();
    }

    public void MouseFunction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, layerMask))
        {
            if (hit.transform.tag == "Tile")
            {
                HexTile temp = hit.transform.GetComponent<HexTile>();
                if (temp.Walkable && state == State.Move)
                {
                    temp.Hovered = true;
                    if (Input.GetButtonDown(KeyCode.Mouse0.ToString()))
                    {
                        targetTile = temp;
                        targetTile.SetSelected();
                        RecievedInput();
                    }
                }
            }
            else if (hit.transform.tag == "Enemy" && currentChar.tag == "Ally" && state == State.Attack)
            {

            }
        }
    }

    void StateSwitch()
    {
        ResetTargets();
        HexGrid.ResetAllTiles();
        switch (state)
        {
            case State.Attack:
                currentList = Pathfinder.instance.FindAttackableCharacters(currentChar);
                break;
            case State.Move:
                currentList = Pathfinder.instance.FindSelectableTiles(currentChar);
                break;
            case State.Skill1:
                break;
            case State.Skill2:
                break;
            default:
                break;
        }
    }

    void RecievedInput()
    {
        switch (state)
        {
            case State.Attack:
                currentChar.Attack(targetChara);
                break;
            case State.Move:
                StartCoroutine(currentChar.MoveDownPath(Pathfinder.instance.FindPath(currentChar.GetCurrentTile(), targetTile)));
                break;
            case State.Skill1:
                break;
            case State.Skill2:
                break;
            default:
                break;
        }
    }

    public void Attack()
    {
        state = State.Attack;
        StateSwitch();
    }

    public void Skill1()
    {
        state = State.Skill1;
        StateSwitch();
    }

    public void Skill2()
    {
        state = State.Skill2;
        StateSwitch();
    }

    public void Move()
    {
        state = State.Move;
        StateSwitch();
    }

    public void Pass()
    {
        StartCoroutine(TurnManager.EndTurn());
    }

    public bool BusyCheck()
    {
        return currentChar.CheckExecuting();
    }

    public bool Battle
    {
        get { return battle; }
        set { battle = value; }
    }

    void ResetTargets()
    {
        targetChara = null;
        targetTile = null;
    }

    public void StartBattle(List<Character> enemies)
    {
        Battle = true;
        StartCoroutine(TurnManager.NewBattle(enemies));
    }

    void ButtonsSet()
    {
        if (BusyCheck())
        {
            move.Interactable = false;
            attack.Interactable = false;
            skill1.Interactable = false;
            skill2.Interactable = false;
            pass.Interactable = false;
        }
        else
        {
            move.Interactable = true;
            attack.Interactable = true;
            skill1.Interactable = true;
            skill2.Interactable = true;
            pass.Interactable = true;
        }
    }

    public void SwitchChara(Character newChara)
    {
        currentChar = newChara;
        Move();
    }
}