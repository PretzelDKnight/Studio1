using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDownAutomata : MonoBehaviour
{
    public static PushDownAutomata instance = null;

    Stack<AbstractMenu> pushDown = new Stack<AbstractMenu>();

    public AbstractMenu Game;
    public AbstractMenu Help;
    public AbstractMenu Main;
    public AbstractMenu Pause;

    AbstractMenu current;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        pushDown.Push(Main);
    }

    // Update is called once per frame
    void Update()
    {
        current = pushDown.Peek();
        current.Function();
    }

    public void AddGame()
    {
        pushDown.Push(Game);
    }

    public void AddHelp()
    {
        pushDown.Push(Help);
    }

    public void AddPause()
    {
        pushDown.Push(Pause);
    }

    public void ClearStack()
    {
        pushDown.Clear();
        pushDown.Push(Main);
    }

    public void PrevMenu()
    {
        pushDown.Pop();
    }
}
