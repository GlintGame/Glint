using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    public Vector3 Offset;
    [Range(0f, 1f)] public float Smoothing = 0.125f;
    
    void FixedUpdate()
    {
        Vector3 DesiredPosition = Target.position + this.Offset;
        Vector3 SmoothedPosition = Vector3.Lerp(this.transform.position, DesiredPosition, this.Smoothing);
        this.transform.position = SmoothedPosition;
    }

}
