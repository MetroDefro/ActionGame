using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class CsEnemy2 : MonoBehaviour
{
    private GameObject Player;
    public int hp;
    private Animator anim;

    private bool isAttack = false;
    private bool isHit = false;
    private bool isDead = false;
    private bool attackAble = true;

    private float rang;

    public GameObject missile;
    public Transform missilePosition;

    // Start is called before the first frame update
    void Start()
    {
        rang = UnityEngine.Random.Range(-0.3f, 0.3f);
        transform.Translate(new Vector2(rang, 0));
        Player = GameObject.Find("PlayerCha");
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Stand();
        StartCoroutine("Delay");
    }
    void Stand()
    {
        Vector2 playerPos = Player.transform.position;

        float f = Mathf.Abs(playerPos.x - transform.position.x);
        if(!isHit && !isDead)
        {
            if (playerPos.x < transform.position.x)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (playerPos.x > transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }

    }

    IEnumerator Delay()
    {
        if (isHit || !attackAble)
            yield break;
        attackAble = false;
        for(int i = 0; i<6; i++)
        {
            if (isHit)
                yield break;
            yield return new WaitForSeconds(0.5f);
        }

        if (isHit)
            yield break;
        anim.SetBool("Attack", true);
    }
    void Attack()
    {
        //UnityEngine.Debug.Log("공격");
        if(!isDead && !isHit && !isAttack )
        {
            Instantiate(missile, missilePosition.position, missilePosition.rotation);
            attackAble = true;
            anim.SetBool("Attack", false);
        }

    }
    void HitAttack()
    {
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
        if (!isDead && !isHit)
        {
            SoundManager.instance.PlaySoundJumpattackDamaged();
            //isHitMissile = false;
            hp -= 3;
            if (hp <= 0)
            {
                StartCoroutine("DestroySelf");
            }
            StartCoroutine("HitAnimation");
            yield return new WaitForSeconds(1f);
            //isHitMissile = true;
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
        attackAble = false;
        anim.SetBool("Attack", false);
        isAttack = false;
        yield return new WaitForSeconds(0.5f);
        attackAble = true;
        isHit = false;
        anim.SetBool("Hit", false);
        anim.SetBool("Attack", true);
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
        attackAble = false;
        anim.SetBool("Attack", false);
        isAttack = false;
        yield return new WaitForSeconds(0.5f);
        attackAble = true;
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
}
