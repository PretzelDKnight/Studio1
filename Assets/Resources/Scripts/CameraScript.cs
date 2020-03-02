using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    static public CameraScript instance = null;

    [SerializeField] Character current;

    public Vector3 camOffset;
    public float speed = 1;

    static bool busy = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        current = Player.instance.protagonist;
    }

    private void Update()
    {
        if (!busy)
            CamPos();
    }

    void CamPos()
    {
        transform.position = current.transform.position + camOffset;

        transform.LookAt(current.transform);
    }

    public IEnumerator ChangeCurrent(Character next)
    {
        float time = 0;
        busy = true;
        Vector3 currentPos = transform.position;
        Vector3 destPos = next.transform.position + camOffset;

        while (time <= 1)
        {
            transform.position = Vector3.Lerp(currentPos, destPos, time);
            time += Time.deltaTime * speed;

            yield return null;
        }

        current = next;
        busy = false;
    }
}
