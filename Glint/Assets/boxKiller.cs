using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxKiller : MonoBehaviour {

    public Transform respawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.ToString() == "box")
        {
            Debug.Log('1');
            collision.gameObject.transform.position = respawn.position;
        }
    }


}
