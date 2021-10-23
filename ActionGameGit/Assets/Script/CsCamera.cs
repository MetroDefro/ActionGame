using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsCamera : MonoBehaviour
{
    public GameObject Player;
    private Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, 0, -10);
        originPos = transform.localPosition;

        if(Player.transform.position.x < 10.2f && Player.transform.position.x > -10.2f)
            originPos = transform.position = new Vector3(Player.transform.position.x, 0, -10);
        else if(Player.transform.position.x >= 10.2f)
            originPos = transform.position = new Vector3(10.2f, 0, -10);
        else
            originPos = transform.position = new Vector3(-10.2f, 0, -10);
    }

    public void GoShake1()
    {
        StartCoroutine("Shake1");
    }
    public void GoShake2()
    {
        StartCoroutine("Shake2");
    }
    public void GoShake3()
    {
        StartCoroutine("Shake3");
    }

    public IEnumerator Shake1()
    {
        float timer = 0;
        while (timer <= 0.1f)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * 0.1f + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;

    }
    public IEnumerator Shake2()
    {
        float timer = 0;
        while (timer <= 0.1f)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * 0.2f + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;

    }
    public IEnumerator Shake3()
    {
        float timer = 0;
        while (timer <= 0.1f)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * 0.3f + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;

    }
}
