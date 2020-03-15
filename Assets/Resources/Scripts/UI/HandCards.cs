using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HandCards : MonoBehaviour
{
    static public HandCards instance = null;

    [SerializeField] Card[] cardPrefab;
    [SerializeField] float cardSpace = 0;
    [SerializeField] float cardDepth = 0;
    [SerializeField] Vector2 quiverAmount = Vector2.zero;
    [SerializeField] Vector2 quiverDifference = Vector2.zero;
    [SerializeField] float rotStart = 0;
    [SerializeField] float rotDiff = 0;
    [SerializeField] Vector3 rotPivot = Vector3.zero;
    [SerializeField] float xTilt = 0;
    [SerializeField] public Vector3 hoverPos = Vector3.zero;
    [SerializeField] public float hoverSpd = 0;

    Card[] cards;
    bool generated = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        generated = false;
    }

    public void GenerateHand()
    {
        if (!generated)
        {
            cards = new Card[cardPrefab.Length];
            for (int i = 0; i < cardPrefab.Length; i++)
            {
                cards[i] = Instantiate(cardPrefab[i]);
                cards[i].transform.parent = this.transform;
                cards[i].transform.localPosition = new Vector3(i * cardSpace, 0, i * cardDepth);
                cards[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                cards[i].SetDelay(quiverDifference * i);
                cards[i].transform.localRotation = Quaternion.Euler(xTilt, 0, 0);
                cards[i].transform.RotateAround(transform.localPosition + rotPivot, transform.forward, rotStart - i * rotDiff);
            }
            generated = true;
            SetHandMove();
        }
    }

    public void DestroyHand()
    {
        if (generated)
        {
            for (int i = cardPrefab.Length - 1; i >= 0; i--)
            {
                Destroy(cards[i].gameObject);
            }
            generated = false;
        }
    }

    public Vector3 BreathePos(Vector2 delay)
    {
        Vector3 temp = new Vector3();
        temp.x = Mathf.Sin((delay.x + Time.realtimeSinceStartup) * quiverAmount.x);
        temp.y = Mathf.Sin((delay.y + Time.realtimeSinceStartup) * quiverAmount.y);
        return temp;
    }

    public void SetHand(Card caller)
    {
        if (caller == cards[0])
            BattleManager.instance.Move();
        else if (caller == cards[1])
            BattleManager.instance.currentChar.Attack(BattleManager.instance.currentChar);
        //else if (caller == cards[2])
        //    BattleManager.instance.Skill1();
        //else if (caller == cards[3])
        //    BattleManager.instance.Skill2();
        else if (caller == cards[4])
            BattleManager.instance.Pass();
        else
            Debug.Log("Uninitiated Card function!");

        ResetHand(caller);
    }

    public void ResetHand(Card caller)
    {
        foreach (var card in cards)
        {
            if (caller != card)
            {
                if (card.Selected)
                    card.ResetTimer();
                card.Selected = false;
            }
        }
    }

    public void SetHandMove()
    {
        cards[0].Selected = true;
        ResetHand(cards[0]);
    }
}
