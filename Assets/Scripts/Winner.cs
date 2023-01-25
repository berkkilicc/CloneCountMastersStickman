using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    [SerializeField] ParticleSystem winner1;
    [SerializeField] ParticleSystem winner2;
    [SerializeField] ParticleSystem winner3;
    [SerializeField] GameObject pe;
    [SerializeField] GameObject pe1;
    [SerializeField] GameObject pe2;
    // Start is called before the first frame update
    void Start()
    {
        winner1 = GetComponent<ParticleSystem>();
        winner2 = GetComponent<ParticleSystem>();
        winner3 = GetComponent<ParticleSystem>();

        pe.SetActive(false);
        pe1.SetActive(false);
        pe2.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Clone")
        {
            pe.SetActive(true);
            pe1.SetActive(true);
            pe2.SetActive(true);

            winner1.Play();
            winner2.Play();
            winner3.Play();
        }
        //else
        //{
        //    winner1.Stop();
        //    winner2.Stop();
        //    winner3.Stop();
        //}
       
    }
}
