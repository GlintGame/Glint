using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnHit : MonoBehaviour, IHitable
{
    public int hitsNeeded = 1;

    public void TakeDamages(int damages, Vector3 origin)
    {
        this.hitsNeeded--;
        if(hitsNeeded <= 0)
            Destroy(this.gameObject);
    }
}
