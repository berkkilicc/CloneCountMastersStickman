using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Attack : MonoBehaviour
{
    private Enemy enm;
    public Transform player;
    [Range(0f, 1f)] [SerializeField] private float Distance, Radius;
    [SerializeField] private GameObject bloodEffects;
    [SerializeField] private GameObject bluebloodEffects;

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
            GameObject bloods = Instantiate(bloodEffects, transform.position, Quaternion.identity, transform);
            GameObject bluebloods = Instantiate(bluebloodEffects, transform.position, Quaternion.identity, transform);
            FindObjectOfType<PlayerManager>().RayCast();
            Destroy(other.gameObject);
            FindObjectOfType<Enemy>().enemyOfClone -= 1;
            FindObjectOfType<Enemy>().enemyCounterTxt.text.ToString();
            


        }
        else if (FindObjectOfType<Enemy>().enemyOfClone == 0 && FindObjectOfType<Enemy>().enemyCounterTxt.text == 0.ToString())
        {
            GameObject bloods = Instantiate(bloodEffects, transform.position, Quaternion.identity, transform);
            GameObject bluebloods = Instantiate(bluebloodEffects, transform.position, Quaternion.identity, transform);
            FindObjectOfType<PlayerManager>().playerMoveSpeed = 5f;
            FindObjectOfType<PlayerManager>().playerTouchSpeed = 5f;
            FindObjectOfType<Enemy>().enemyOfClone -= 1;
            FindObjectOfType<Enemy>().enemyCounterTxt.text.ToString();
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
