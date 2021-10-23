using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CsMissile : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    public Transform pos;
    public Vector2 boxSize;
    private bool loca;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(MissileAttack());
        player = GameObject.Find("PlayerCha");
        if (player.transform.localScale.x > 0)
            loca = true;
        else
        {
            loca = false;
            transform.localScale = new Vector2(-1, 1);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        float translateMove = moveSpeed * Time.smoothDeltaTime;
        if (loca)
            transform.Translate(new Vector2(-2 * translateMove, -1 * translateMove));
        else
            transform.Translate(new Vector2(2 * translateMove, -1 * translateMove));

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.SendMessage("HitJumpAttack", SendMessageOptions.DontRequireReceiver);
                //Destroy(gameObject);
            }
            else if (collider.tag == "EMissile")
            {
                SoundManager.instance.PlaySoundJumpattackDamaged();
                collider.SendMessage("HitAttack", SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
            if ((transform.position.x >= 18) || (transform.position.x <= -18) || (transform.position.y <= -4))
            {
                Destroy(gameObject);
            }
        }
    }
    void Attack()
    {

    }
    /*
    IEnumerator Attack()
    {

            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine("Attack");
    }
    */

}
