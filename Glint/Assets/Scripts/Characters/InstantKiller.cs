using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKiller : MonoBehaviour {

    public string ToKillTag = "all";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable toKill = collision.gameObject.GetComponent<IHitable>();
        
        if(toKill != null)
        {
            if (this.ToKillTag == "all")
            {
                toKill.TakeDamages(99999, collision.gameObject.GetComponent<Transform>().position);
            }
            else
            {
                if (this.ToKillTag == collision.gameObject.tag)
                {
                    toKill.TakeDamages(99999, collision.gameObject.GetComponent<Transform>().position);
                }
            }
        }
    }
}
