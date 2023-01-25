using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Attack : MonoBehaviour
{
    private Enemy enm;
    public Transform player;
    [Range(0f, 1f)] [SerializeField] private float Distance, Radius;


    //[SerializeField] private Transform enemyarea;
    //[SerializeField] private bool attack;
    void Start()
    {
        enm = gameObject.GetComponent<Enemy>();
    }


    void Update()
    {
        player = transform;
     
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyArea")
        {
      
            
            Destroy(other.gameObject);
            FindObjectOfType<Enemy>().enemyOfClone -= 1;
            FindObjectOfType<Enemy>().enemyCounterTxt.text.ToString();
            


        }
        else if (FindObjectOfType<Enemy>().enemyOfClone == 0 && FindObjectOfType<Enemy>().enemyCounterTxt.text == 0.ToString())
        {
            FindObjectOfType<Enemy>().enemyOfClone -= 1;
            FindObjectOfType<Enemy>().enemyCounterTxt.text.ToString();
        }

        if (other.gameObject.tag == "Enemy" && FindObjectOfType<Enemy>().enemyOfClone >= 1)
        {
            Debug.Log("Enemy'e dokundu");
            FindObjectOfType<PlayerManager>().attack = true;
            FindObjectOfType<PlayerManager>().playerMoveSpeed = 1f;
            
        }
        else
        {
            FindObjectOfType<PlayerManager>().attack = false;
            FindObjectOfType<PlayerManager>().playerMoveSpeed = 5f;
        }
        if (other.gameObject.tag == "Boss")
        {
            Destroy(other.gameObject, 2f);
            FindObjectOfType<PlayerManager>().playerMoveSpeed = 0f;
            FindObjectOfType<PlayerManager>().playerTouchSpeed = 0f;
        }


    }



    public void AfterAttack()
    {
        for (int i = 1; i < player.childCount; i++)
        {
            float x = Distance * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            float z = Distance * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

            Vector3 newPos = new Vector3(x, -0.485f, z);

            player.transform.GetChild(i).DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);


        }
    }
}
