using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Obstacle : MonoBehaviour
{
    private PlayerManager plymanager;
    private void Start()
    {
        plymanager = GetComponent<PlayerManager>();

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Clone")
        {
            Debug.Log("Dokundu");
            Destroy(other.gameObject, 0.1f);
            FindObjectOfType<PlayerManager>().NumberOfClone -= 1;
            FindObjectOfType<PlayerManager>().Countertxt.text.ToString();
        }
        if (other.gameObject.tag =="Player" && FindObjectOfType<PlayerManager>().NumberOfClone == 1)
        {
            Destroy(other.gameObject);
        }
    }



}
