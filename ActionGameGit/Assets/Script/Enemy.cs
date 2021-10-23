using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy
{
    public GameObject Player;
    public int hp { get; set; }
    public int speed { get; set; }

    protected virtual void Move(Transform transform)
    {
        /*
        Vector2 playerPos = Player.transform.position;
        float translateMove = speed * Time.smoothDeltaTime;

        
        if ((-1 < playerPos.x - transform.position.x || 1 > playerPos.x - transform.position.x))
            Attack();
        
        if (playerPos.x < transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
            transform.Translate(new Vector3(translateMove * -1, 0f));
        }
        else if (playerPos.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
            trans
                form.Translate(new Vector3(translateMove * 1, 0f));
        }
        */
    }
    protected virtual void Attack()
    {
        //이건 나중에 클래스별로 몹을 생성해서 공격방식을 분류할 필요가 있을듯... 이동방식도 응,,,
    }

    protected virtual void HitAttack()
    {
        hp -= 5;
        if (hp < 0)
        {
            DestroySelf();
        }
    }
    protected virtual void DestroySelf()
    {
        //죽는 애니메이션
        //Destroy(gameObject);
    }
}

public class ManStudent : Enemy
{
    protected override void Move(Transform transform)
    {
        
        Vector2 playerPos = Player.transform.position;
        float translateMove = speed * Time.smoothDeltaTime;

        
        //if ((-1 < playerPos.x - transform.position.x || 1 > playerPos.x - transform.position.x))
            //Attack();
        
        if (playerPos.x < transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
            transform.Translate(new Vector3(translateMove * -1, 0f));
        }
        else if (playerPos.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
            transform.Translate(new Vector3(translateMove * 1, 0f));
        }
        
    }
}