using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    Animator anim;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Clone")
        {
            GetComponent<Animator>().SetBool("attack", true);
            Destroy(other.gameObject);
            FindObjectOfType<PlayerManager>().NumberOfClone -= 1;
            FindObjectOfType<PlayerManager>().Countertxt.text.ToString();

        }
        else if (other.gameObject.tag == "Player" && FindObjectOfType<PlayerManager>().NumberOfClone == 1)
        {
            Destroy(other.gameObject);
        }
    }
}
