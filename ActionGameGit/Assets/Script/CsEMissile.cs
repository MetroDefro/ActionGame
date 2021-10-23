using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CsEMissile : MonoBehaviour
{
    private GameObject Enemy2;
    public float moveSpeed;
    public Transform pos;
    public Vector2 boxSize;
    private bool loca;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(MissileAttack());
        player = GameObject.Find("PlayerCha");
        //UnityEngine.Debug.Log(transform.position.x - player.transform.position.x);
        if (transform.position.x - player.transform.position.x > 0)
            loca = true;
        else
            loca = false;
    }

    // Update is called once per frame
    void Update()
    {
        float translateMove = moveSpeed * Time.smoothDeltaTime;
        if(loca)
            transform.Translate(new Vector2(-0.5f * translateMove, 0));
        else
            transform.Translate(new Vector2(0.5f * translateMove, 0));

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                collider.SendMessage("HitAttack", SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }

        }
        if ((transform.position.x >= 18) || (transform.position.x <= -18) || (transform.position.y <= -4))
        { 
            Destroy(gameObject); 
        }
    }
    /*
     IEnumerator MissileAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                
                collider.SendMessage("HitAttack", SendMessageOptions.DontRequireReceiver);
                yield return new WaitForSeconds(1);
            }

        }
    }
     */
    void HitAttack()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            //coll.SendMessage("HitAttack", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        if(coll.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }
}
