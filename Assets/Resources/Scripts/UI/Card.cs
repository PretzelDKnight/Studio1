using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    Vector3 ogPos; // Centre position of the card
    Vector2 delay;
    bool hovered = false;
    bool selected = false;
    bool available = true;
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
        else if (hovered && available)
            transform.localPosition = Vector3.Lerp(ogPos, ogPos + HandCards.instance.hoverPos, time) + HandCards.instance.BreathePos(delay);

        if (selected)
            transform.localPosition = Vector3.Lerp(ogPos, ogPos + HandCards.instance.hoverPos, time) + HandCards.instance.BreathePos(delay);
    }

    public void SetDelay(Vector2 value)
    {
        delay = value;
    }

    public void OnMouseEnter()
    {
        hovered = true;
        if (!selected)
            time = 0;
    }

    public void OnMouseExit()
    {
        hovered = false;
        if (!selected)
            time = 0;
    }

    public void OnMouseDown()
    {
        Selected = true;
        HandCards.instance.SetHand(this);
    }

    public bool Selected
    {
        get { return selected; }
        set 
        { selected = value; }
    }

    void Timer()
    {
        if (time <= 1)
            time += Time.deltaTime * HandCards.instance.hoverSpd;
    }

    public void ResetTimer()
    {
        time = 0;
    }

    public void SetAvailable()
    {
        available = false;
    }
}
