using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CsPlayerC : MonoBehaviour
{
    public Image fade;
    private float fades = 0f;

    //기본 동작에 대하여
    public int speed;
    public int jumpForce;
    private Rigidbody2D rigid2D;
    private bool isJump = false;
    //private bool isJumpAttack = false;
    private Animator anim;
    SpriteRenderer spr;

    //콤보관련
    public int comboCount = 0;
    float lastComboTime = 0;
    public float maxComboDelay = 0.25f;

    public Transform cPos1;
    public Vector2 boxSize1;

    public Transform cPos2;
    public Vector2 boxSize2;

    public Transform cPos3;
    public Vector2 boxSize3;

    private bool doAttack = false;

    //점프 투사체 공격
    public GameObject missile;
    public Transform missilePosition;

    //캐릭터 상태
    public int hp;
    private bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        fade.color = new Color(0f, 0f, 0f, 0f);
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(1f, 1f, 1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!(comboCount > 0))
        {
            Move();
            if (Input.GetKeyDown(KeyCode.C))
                jump();
        }

        if (Time.time - lastComboTime > maxComboDelay)
        {
            comboCount = 0;
            anim.SetFloat("Combo", 0);
        }
        if (!isJump)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                lastComboTime = Time.time;
                comboCount++;
                if (comboCount == 1)
                {
                    anim.SetFloat("Combo", 1);
                }
                comboCount = Mathf.Clamp(comboCount, 0, 3);
            }
        } else if (isJump)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //isJumpAttack = true;
                anim.SetBool("JumpAttack", true);
            }
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float translateMove = speed * Time.smoothDeltaTime;

        Vector3 pos = transform.position;

        if (x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if ((x < 0 && pos.x > -18) || (x > 0 && pos.x < 18))
            transform.Translate(new Vector2(x * translateMove, 0f));

        anim.SetFloat("Move", Input.GetAxisRaw("Horizontal"));
    }

    void jump()
    {
        //점프 중 Z키 누르면 점프공격
        if (!isJump)
        {
            rigid2D.velocity = Vector2.up * jumpForce;
            isJump = true;
            anim.SetFloat("Jump", 1);
        }
    }
    public void JumpAttack()
    {
        Instantiate(missile, missilePosition.position, missilePosition.rotation);
        SoundManager.instance.PlaySoundAir();
        anim.SetBool("JumpAttack", false);
        comboCount = 0;

    }

    public void combo1Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(cPos1.position, boxSize1, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.SendMessage("HitAttack", SendMessageOptions.DontRequireReceiver);
                GameObject.Find("Main Camera").GetComponent<CsCamera>().GoShake1();
                SoundManager.instance.PlaySoundAttack1();
                doAttack = true;
            }
        }
        if (!doAttack)
            SoundManager.instance.PlaySoundAir();
    }
    public void AttackEnd()
    {
        doAttack = false;
    }
    public void combo1()
    {

        if (comboCount >= 2)
        {
            anim.SetFloat("Combo", 2);
        }
        else
        {
            anim.SetFloat("Combo", 0);
            comboCount = 0;
        }
    }
    public void combo2Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(cPos2.position, boxSize2, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.SendMessage("HitAttack", SendMessageOptions.DontRequireReceiver);
                GameObject.Find("Main Camera").GetComponent<CsCamera>().GoShake2();
                SoundManager.instance.PlaySoundAttack2();
                doAttack = true;
            }
        }
        if (!doAttack)
            SoundManager.instance.PlaySoundAir();

    }
    public void combo2()
    {
        if (comboCount >= 3)
        {
            anim.SetFloat("Combo", 3);
        }
        else
        {
            anim.SetFloat("Combo", 0);
            comboCount = 0;
        }
    }
    public void combo3Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(cPos3.position, boxSize3, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.SendMessage("HitSupuerAttack", SendMessageOptions.DontRequireReceiver);
                GameObject.Find("Main Camera").GetComponent<CsCamera>().GoShake3();
                SoundManager.instance.PlaySoundAttack3();
                doAttack = true;
            }
        }
        if (!doAttack)
            SoundManager.instance.PlaySoundAir();

    }
    public void combo3()
    {
        anim.SetFloat("Combo", 0);
        comboCount = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && (transform.position.y - collision.transform.position.y) > 0.5)
        {
            isJump = false;
            anim.SetFloat("Jump", 0);
        }

    }
    
    public void HitAttack()
    {
        if (!isHit)
        {
            hp--;
            GameObject.Find("StageManager").GetComponent<CsStage>().MinuseHeart(hp); // 스테이지 스크립트에 플레이어의 hp 전달
            SoundManager.instance.PlaySoundHit();
            if (hp == 0)
            {
                StartCoroutine("DestroyPlayer");
            }
            StartCoroutine("HitAnimation");
        }
    }

     IEnumerator DestroyPlayer()
     {
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(0.2f);
        while (fades < 1f)
        {
            fades += 0.1f;
            fade.color = new Color(0f, 0f, 0f, fades);
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene("GameOver");
        //isDead = true;
    }
    IEnumerator HitAnimation()
    {
        isHit = true;
        for(int i = 0; i < 4; i++)
        {
            //color.a = 0.5f;
            spr.color = new Color(1f, 1f, 1f, 0.4f);
            yield return new WaitForSeconds(0.1f);
            spr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
            //UnityEngine.Debug.Log("피격 판정중. . .");
        }
        //UnityEngine.Debug.Log("피격 판정 종료");
        isHit = false;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.blue;
        Gizmos.DrawWireCube(cPos1.position, boxSize1);
    }
    */
    
    
}