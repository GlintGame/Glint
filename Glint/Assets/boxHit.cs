using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxHit : MonoBehaviour {

    public Sprite happy;
    public Sprite sad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().sprite = sad;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().sprite = happy;
        }
    }
}
