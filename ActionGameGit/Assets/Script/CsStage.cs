using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CsStage : MonoBehaviour
{
    private Vector2 screenCenter;
    private Vector2 screenTop;
    private Vector2 screenTopLeft;
    private Vector2 playerPos;
    private Vector2 screenTopRight;

    public GameObject Player;
    public static CsStage instance;

    public GameObject clear;
    public GameObject wave;
    public GameObject heart;
    public GameObject end;

    public int stage;
    private int maxEnemy;
    public int enemyCount = 0;

    private Animator anim;
    private Animator anim2;

    void Awake()  // Start함수보다 먼저 호출됨
    {
        if (CsStage.instance == null)  //게임시작했을때 이 instance가 없을때
            CsStage.instance = this;  // instance를 생성
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (stage)
        {
            case 1:
                maxEnemy = 22;
                break;
            case 2:
                maxEnemy = 24;
                break;
        }
        playerPos = Player.transform.position;
        anim = heart.GetComponent<Animator>();
        anim2 = wave.GetComponent<Animator>();

        anim.SetFloat("HP", 6);
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 1:
                maxEnemy = 22;
                break;
            case 2:
                maxEnemy = 24;
                break;
        }
        if (Player.transform.position.x < 10.2f && Player.transform.position.x > -10.2f)
            playerPos = Player.transform.position;
        else if (Player.transform.position.x >= 10.2f)
            playerPos = new Vector3(10.2f, 0, -10);
        else
            playerPos = new Vector3(-10.2f, 0, -10);
        screenCenter = new Vector2(playerPos.x, 0);
        screenTop = new Vector2(playerPos.x, 4.5f);
        screenTopLeft = new Vector2(playerPos.x - 7.3f, 4.5f);
        screenTopRight = new Vector2(playerPos.x + 8.2f, 4.4f);
        wave.transform.position = screenTop;
        heart.transform.position = screenTopLeft;
        end.transform.position = screenTopRight;
    }

    public void PlusCount()
    {
        enemyCount++;
        if (enemyCount == maxEnemy)
        {
            if(stage == 1)
            {
                stage = 2;
                anim2.SetFloat("Wave", 2);
                StartCoroutine(this.NewWave());
                enemyCount = 0;
            }
            else if(stage == 2)
                Instantiate(clear, screenCenter, Quaternion.identity);

        }

    }

    IEnumerator NewWave()
    {
        SoundManagerOther.instance.PlaySoundNewWave();
        yield return new WaitForSeconds(5f);
        GameObject.Find("EnemySpawn").GetComponent<CsEnemySpawn>().GoStage2();
    }

    public void MinuseHeart(int hp)
    {
        anim.SetFloat("HP", hp);
        if(hp == 0)
            heart.gameObject.SetActive(false);
    }

}
