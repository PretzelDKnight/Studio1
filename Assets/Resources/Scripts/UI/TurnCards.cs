using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCards : MonoBehaviour
{
    public static TurnCards instance = null;

    public CharacterVariable currentChara;
    public Card cardPrefab;
    public int numberOfCards;
    public Vector3 cardDiff;
    public Vector2 quiverDifference;
    public float xTilt;
    public float yTilt;
    public float faceCardTilt;
    public Vector3 rotPivot;
    public float rotStart;
    public float rotDiff;
    public float faceCardRot;
    public float faceCardHeight;

    Card[] downCards;
    Card faceCard;
    bool generated = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateStatCards()
    {
        if (!generated)
        {
            downCards = new Card[numberOfCards];
            int x = 0;
            for (int i = 0; i < numberOfCards; i++)
            {
                downCards[i] = Instantiate(cardPrefab);
                downCards[i].transform.parent = this.transform;
                downCards[i].transform.localPosition = new Vector3(i * cardDiff.x, i * cardDiff.y, i * cardDiff.z);
                downCards[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                downCards[i].SetDelay(quiverDifference * i);
                downCards[i].transform.localRotation = Quaternion.Euler(xTilt, yTilt, 0);
                downCards[i].transform.RotateAround(transform.localPosition + rotPivot, transform.forward, rotStart - i * rotDiff);
                x = i;
            }
            ++x;
            faceCard = Instantiate(cardPrefab);
            faceCard.transform.parent = this.transform;
            faceCard.transform.localPosition = new Vector3(x * cardDiff.x, x * cardDiff.y, x * cardDiff.z + faceCardHeight);
            faceCard.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            faceCard.SetDelay(quiverDifference * x);
            faceCard.transform.localRotation = Quaternion.Euler(faceCardTilt,0, 0);
            faceCard.transform.RotateAround(transform.localPosition + rotPivot, transform.forward, faceCardRot);

            generated = true;
        }
    }

    public void DestroyStatCards()
    {
        if (generated)
        {
            for (int i = numberOfCards - 1; i >= 0; i--)
            {
                Destroy(downCards[i].gameObject);
            }
            generated = false;
            Destroy(faceCard.gameObject);
        }
    }
}
