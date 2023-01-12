using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public float smooth = 0.125f;

    private void Update()
    {
        Vector3 desiredPosition = followTarget.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPosition, smooth);
        transform.position = smoothPos;
        //transform.LookAt(followTarget);
        
       
    }

   
}
