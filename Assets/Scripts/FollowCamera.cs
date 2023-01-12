using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 velocity;
    public float smoothTime;

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(transform.position.x,transform.position.y,followTarget.position.z);
        transform.position = Vector3.SmoothDamp(transform.position,targetPos,ref velocity, smoothTime);
    }

   
}
