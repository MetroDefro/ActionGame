using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CsEnemySpawn : MonoBehaviour
{
    public Transform[] points;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;

    public float createTime;
    public int maxEnemy;
    public bool isGameOver = false;

    public int EnemyCount = 0;

    public int wave;

    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.Find("EnemySpawn").GetComponentsInChildren<Transform>();

        StartCoroutine(this.CreateEnemy());
    }
    // Update is called once per frame
    void Update()
    {

    }
    /*
    IEnumerator CreateEnemy() {
        
        while (!isGameOver)
        {

            if(EnemyCount < maxEnemy)
            {
                yield return new WaitForSeconds(createTime);

                int idx = UnityEngine.Random.Range(1, points.Length);

                Instantiate(enemyPrefab, points[idx].position, points[idx].rotation);

                EnemyCount++;
            }
            else
            {
                yield return null;
            }
        }       
    }
    */
    IEnumerator CreateEnemy()
    {
        //1
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        Instantiate(enemyPrefab, points[5].position, points[5].rotation);
        //2
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        //3
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        Instantiate(enemyPrefab, points[4].position, points[4].rotation);
        //4
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[1].position, points[1].rotation);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        //5
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        Instantiate(enemyPrefab, points[5].position, points[5].rotation);
        //6
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[4].position, points[4].rotation);
        //7
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        //8
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        Instantiate(enemyPrefab, points[5].position, points[5].rotation);
        //9
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        //10
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        Instantiate(enemyPrefab, points[4].position, points[4].rotation);
        //11
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[1].position, points[1].rotation);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        //12
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        Instantiate(enemyPrefab, points[5].position, points[5].rotation);
        //13
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[4].position, points[4].rotation);

    }
    public void GoStage2()
    {
        StartCoroutine(this.CreateEnemy2());
    }
    IEnumerator CreateEnemy2()
    {
        //1
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        Instantiate(enemyPrefab, points[5].position, points[5].rotation);
        //2
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[4].position, points[4].rotation);
        //3
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[1].position, points[1].rotation);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        //4
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        Instantiate(enemyPrefab2, points[2].position, points[2].rotation);
        //5
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[1].position, points[1].rotation);
        Instantiate(enemyPrefab, points[5].position, points[5].rotation);
        //6
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[4].position, points[4].rotation);
        //7
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        //8
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab2, points[4].position, points[4].rotation);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        //9
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        //10
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[3].position, points[3].rotation);
        //11
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[1].position, points[1].rotation);
        Instantiate(enemyPrefab2, points[2].position, points[2].rotation);
        Instantiate(enemyPrefab, points[5].position, points[5].rotation);
        //12
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab2, points[4].position, points[4].rotation);
        //13
        yield return new WaitForSeconds(createTime);
        Instantiate(enemyPrefab, points[1].position, points[1].rotation);
        Instantiate(enemyPrefab, points[2].position, points[2].rotation);
        Instantiate(enemyPrefab, points[5].position, points[5].rotation);
    }
}