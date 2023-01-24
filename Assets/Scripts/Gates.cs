using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gates : MonoBehaviour
{
    public TextMeshPro GateNo;
    public int randomNumberMultiply;
    public int randomNumberIncrease;
    public bool multiply;

    private void Start()
    {
        if (multiply)
        {
            randomNumberMultiply = Random.Range(1, 2);
            GateNo.text = "X" + randomNumberMultiply;
        }
        else
        {
            randomNumberIncrease = Random.Range(30, 50);
            if (randomNumberIncrease %2 !=0)
            {
                randomNumberIncrease += 1;
            }
            GateNo.text = "+" + randomNumberIncrease.ToString();
        }
    }
}
