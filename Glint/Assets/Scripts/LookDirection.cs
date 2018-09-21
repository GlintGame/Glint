using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirection : MonoBehaviour
{
    public Transform Source;
    public float YOffset = 5;
    [Range(0.1f, 0.9f)] public float Sensitivity = 0.8f;    

    public void LookAxisVertical(float verticalAxis)
    {
        Vector3 target = Source.position;
        if (SensitivityCheck(ref verticalAxis))
        {
            target.y += this.YOffset * (verticalAxis > 0 ? 1 : -1);
        }
        transform.position = target;        
    }    

    private bool SensitivityCheck(ref float value)
    {
        return Mathf.Abs(value) > (1 - this.Sensitivity);
    }    
}
