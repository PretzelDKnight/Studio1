using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    Vector3 ogPos; // Centre position of the card
    Vector3 selectPos; // Position on select
    Vector2 delay;
    bool hovered = false;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.localPosition;
        hovered = false;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        Breathe();
    }

    void Breathe()
    {
        if (!hovered)
            transform.localPosition = Vector3.Lerp(ogPos + HandCards.instance.hoverPos, ogPos, time) + HandCards.instance.BreathePos(delay);
        else
            transform.localPosition = Vector3.Lerp(ogPos, ogPos + HandCards.instance.hoverPos, time) + HandCards.instance.BreathePos(delay);
    }

    public void SetDelay(Vector2 value)
    {
        delay = value;
    }

    public void OnMouseEnter()
    {
        hovered = true;
        time = 0;
    }

    public void OnMouseExit()
    {
        hovered = false;
        time = 0;
    }

    void Timer()
    {
        if (time <= 1)
            time += Time.deltaTime * HandCards.instance.hoverSpd;
    }
}
