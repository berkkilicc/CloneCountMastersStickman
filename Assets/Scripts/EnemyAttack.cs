using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject bloodEffects;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Clone")
        {
            
            Destroy(other.gameObject);
            

        }
        else if (other.gameObject.tag == "Player" && FindObjectOfType<PlayerManager>().NumberOfClone == 1)
        {
           
            other.gameObject.SetActive(false);
            FindObjectOfType<PlayerManager>().playerMoveSpeed = 0f;
            FindObjectOfType<PlayerManager>().playerTouchSpeed = 0f;

        }
        else
        {
            return;
        }
    }
}
