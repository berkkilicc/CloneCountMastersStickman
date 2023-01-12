using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMove : MonoBehaviour
{
    public GameObject target;

    
    private float speed = 10f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

  
}
