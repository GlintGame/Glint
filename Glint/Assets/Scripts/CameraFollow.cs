using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    public float YOffset;
    public float YLimitBeforeReset;
    [Range(0f, 1f)] public float Smoothing = 0.125f;
    
    void FixedUpdate()
    {
        float xposition = Target.position.x;
        float yTarget = this.YOffset;

        float ypositionShift = Target.position.y - transform.position.y;
        if (ypositionShift > this.YLimitBeforeReset 
            || ypositionShift < this.YLimitBeforeReset)
        {
            yTarget += ypositionShift;
        }

        Vector3 desiredPosition = new Vector3(xposition, yTarget, -1f);
        Vector3 smoothedPosition = Vector3.Lerp(this.transform.position, desiredPosition, this.Smoothing);
        this.transform.position = smoothedPosition;
    }

}
