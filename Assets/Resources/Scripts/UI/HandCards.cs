using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HandCards : MonoBehaviour
{
    static public HandCards instance = null;

    [SerializeField] Card cardPrefab;
    [SerializeField] int numberOfCards = 0;
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
    bool buttonCall;
    bool generated;

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

    void Update()
    {

    }

    public void GenerateHand()
    {
        if (!generated)
        {
            cards = new Card[numberOfCards];
            for (int i = 0; i < numberOfCards; i++)
            {
                cards[i] = Instantiate(cardPrefab);
                cards[i].transform.parent = this.transform;
                cards[i].transform.localPosition = new Vector3(i * cardSpace, 0, i * cardDepth);
                cards[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                cards[i].SetDelay(quiverDifference * i);
                cards[i].transform.localRotation = Quaternion.Euler(xTilt, 0, 0);
                cards[i].transform.RotateAround(transform.localPosition + rotPivot, transform.forward, rotStart - i * rotDiff);
            }
            generated = true;
        }
    }

    public void DestroyHand()
    {
        if (generated)
        {
            for (int i = numberOfCards - 1; i >= 0; i--)
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
}
