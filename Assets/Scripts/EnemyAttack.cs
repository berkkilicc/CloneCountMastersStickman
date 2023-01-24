using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAttack : MonoBehaviour
{
    public Transform enemy;
    [Range(0f, 1f)] [SerializeField] private float Distance, Radius;

    private void Start()
    {
        enemy = transform;
    }

    private void Update()
    {
        //FindObjectOfType<PlayerManager>().NumberOfClone -= 1;
        //FindObjectOfType<PlayerManager>().Countertxt.text.ToString();

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Clone")
        {
            Debug.Log("Text çalýþýyor");
            Destroy(other.gameObject);
            FindObjectOfType<PlayerManager>().NumberOfClone -= 1;
            FindObjectOfType<PlayerManager>().Countertxt.text.ToString();
         


        }
        else if (other.gameObject.tag == "Player" && FindObjectOfType<PlayerManager>().NumberOfClone == 1)
        {
           
            other.gameObject.SetActive(false);
            FindObjectOfType<PlayerManager>().playerMoveSpeed = 0f;
            FindObjectOfType<PlayerManager>().playerTouchSpeed = 0f;
            FindObjectOfType<PlayerManager>().NumberOfClone -= 1;
            FindObjectOfType<PlayerManager>().Countertxt.text.ToString();

        }
        else
        {
            return;
        }
    }


    public void EnemyFormat()
    {
        for (int i = 1; i < enemy.childCount; i++)
        {
            float x = Distance * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            float z = Distance * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

            Vector3 newPos = new Vector3(x, -0.485f, z);

            enemy.transform.GetChild(i).DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);


        }
    }
}
