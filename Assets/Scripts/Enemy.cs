using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Enemy : MonoBehaviour
{

    public Transform enemyArea;

    [SerializeField] private GameObject enemy;
    public Transform lookPlayer;
    [Range(0f, 1f)] [SerializeField] private float DistanceFactor, Radius;


    public int enemyOfClone;
    public TextMeshPro enemyCounterTxt;

 

    void Start()
    {
        
        enemyArea = transform;
        enemyOfClone = transform.childCount - 1;
        enemyCounterTxt.text = enemyOfClone.ToString();
        MakeEnemy();
        FormatEnemyClone();
    }

    void Update()
    {
        

        enemyOfClone = transform.childCount;
        enemyCounterTxt.text = enemyOfClone.ToString();


    }



    public void MakeEnemy()
    {
        for (int i = 0; i < Random.Range(20,21); i++)
        {
            GameObject enemyClone = Instantiate(enemy, new Vector3(transform.position.x, -0.45f, transform.position.z), new Quaternion(0f, 0, 0f, 1f), transform);

        }
        transform.LookAt(lookPlayer);
        enemyOfClone = transform.childCount - 1;
        enemyCounterTxt.text = enemyOfClone.ToString();

    }

    public void FormatEnemyClone()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            float x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            float z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);
            Vector3 NewPos = new Vector3(x, -0.4f, z);
            transform.transform.GetChild(i).localPosition = NewPos;
        }

    }

    
}
