using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class CsEnemy : MonoBehaviour
{
    public GameObject Player;
    public int hp;
    public int speed;
    private Animator anim;

    public Transform pos;
    public Vector2 boxSize;

    private bool isMove = false;
    private bool isAttack = false;
    private bool isHit = false;
    private bool isDead = false;
    private bool attackAble = true;
    private float rang;

    private bool isHitMissile = false;
    //공격 범위
    public Transform aPos;
    public Vector2 aBoxSize;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerCha");
        anim = gameObject.GetComponent<Animator>();
        rang = UnityEngine.Random.Range(1.5f, 1.7f);

        anim.SetFloat("Attack", 0);

        StartCoroutine("Movement");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AttackMove();
    }
    void AttackMove()
    {
        Vector2 playerPos = Player.transform.position;
        float translateMove = speed * Time.smoothDeltaTime;

        float f = Mathf.Abs(playerPos.x - transform.position.x);
        if (isAttack)
        {
            if(f > 1.36f)
            {
                if (playerPos.x < transform.position.x)
                {
                    transform.localScale = new Vector2(1, 1);
                    transform.Translate(new Vector3(translateMove * -0.5f, 0f));
                }
                else if (playerPos.x > transform.position.x)
                {
                    transform.localScale = new Vector2(-1, 1);
                    transform.Translate(new Vector3(translateMove * 0.5f, 0f));
                }
            }
        }
    }
    void Move()
    {
        Vector2 playerPos = Player.transform.position;
        float translateMove = speed * Time.smoothDeltaTime;

        float f = Mathf.Abs(playerPos.x - transform.position.x);
        if(isMove && !isAttack && !isHit && !isDead)
        {
            if (f < rang)
            {
                anim.SetBool("Move", false);
                StartCoroutine("Delay");
                return;
            }
            else if (playerPos.x < transform.position.x)
            {
                transform.localScale = new Vector2(1, 1);
                transform.Translate(new Vector3(translateMove * -0.5f, 0f));
                anim.SetBool("Move", true);
            }
            else if (playerPos.x > transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
                transform.Translate(new Vector3(translateMove * 0.5f, 0f));
                anim.SetBool("Move", true);
            }
        }
        else
            anim.SetBool("Move", false);

    }
    IEnumerator Movement()
    {
        yield return new WaitForSeconds(1f);
        isMove = true;
        yield return new WaitForSeconds(5f);
        isMove = false;

        StartCoroutine("Movement");
    }
    IEnumerator Delay()
    {
        if (isHit || !attackAble)
            yield break;
        attackAble = false;
        yield return new WaitForSeconds(0.5f);
        if (isHit)
            yield break;

        anim.SetFloat("Attack", 1);
        yield return new WaitForSeconds(0.5f);
        if (isHit)
        {
            anim.SetFloat("Attack", 0);
            yield break;
        }
        anim.SetFloat("Attack", 2);
    }
    void Attack()
    {
        if(!isDead && !isHit && !isAttack )
        {
            Vector2 playerPos = Player.transform.position;
            float f = Mathf.Abs(playerPos.x - transform.position.x);

            if (f < rang)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(aPos.position, aBoxSize, 0);

                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Player")
                    {
                        collider.SendMessage("HitAttack", SendMessageOptions.DontRequireReceiver);
                    }
                }
                isAttack = true;
                //UnityEngine.Debug.Log("공격!");
            }
        }

    }

    public void AttackEnd()
    {
        isAttack = false;
        anim.SetFloat("Attack", 0);
        attackAble = true;
    }
    void HitAttack()
    {
        //UnityEngine.Debug.Log("맞음");
        if (!isDead)
        {
            hp -= 5;
            if (hp <= 0)
            {
                StartCoroutine("DestroySelf");
            }
            StartCoroutine("HitAnimation");
        }
    }
    void HitJumpAttack()
    {
        StartCoroutine("HitMissile");
    }
    IEnumerator HitMissile()
    {
        if (!isDead&&!isHit)
        {
            SoundManager.instance.PlaySoundJumpattackDamaged();
            isHitMissile = false;
            hp -= 3;
            if (hp <= 0)
            {
                StartCoroutine("DestroySelf");
            }
            StartCoroutine("HitAnimation");
            yield return new WaitForSeconds(1f);
            isHitMissile = true;
        }
    }
    void HitSupuerAttack()
    {
        if (!isDead)
        {
            hp -= 10;
            if (hp <= 0)
            {
                StartCoroutine("DestroySelf");
            }
            StartCoroutine("HitSuperAnimation");
        }
    }
    IEnumerator HitAnimation()
    {
        isHit = true;
        anim.SetBool("Hit", true);
        anim.SetFloat("Attack", 0);
        attackAble = true;
        isAttack = false;
        yield return new WaitForSeconds(0.5f);
        isHit = false;
        anim.SetBool("Hit", false);
    }
    IEnumerator HitSuperAnimation()
    {
        Vector2 playerPos = Player.transform.position;
        isHit = true;
        anim.SetBool("Hit", true);
        if (playerPos.x < transform.position.x)
        {
            transform.Translate(new Vector3(2, 0f));
        }
        else if (playerPos.x > transform.position.x)
        {
            transform.Translate(new Vector3(-2, 0f));
        }
        anim.SetFloat("Attack", 0);
        attackAble = true;
        isAttack = false;
        yield return new WaitForSeconds(0.5f);
        isHit = false;
        anim.SetBool("Hit", false);

    }
    IEnumerator DestroySelf()
    {
        //죽는 애니메이션
        isDead = true;
        //StartCoroutine("Dead");
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
        CsStage.instance.PlusCount();
    }
    /*
    IEnumerator Dead()
    {
        float translateMove = speed * Time.smoothDeltaTime;
        transform.localScale = (new Vector3(1, translateMove * -1));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Dead");
    }
    */

        /*
    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.blue;
        Gizmos.DrawWireCube(aPos.position, aBoxSize);
    }
    */
}
