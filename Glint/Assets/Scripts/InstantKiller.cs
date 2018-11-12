using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKiller : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable toKill = collision.gameObject.GetComponent<IHitable>();

        if(toKill != null)
        {
            toKill.TakeDamages(99999, this.GetComponent<Transform>().position);
        }
    }
}
