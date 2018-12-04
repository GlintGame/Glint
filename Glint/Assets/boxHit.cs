using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxHit : MonoBehaviour, IHitable {

    public Sprite happy;
    public Sprite sad;
    public Transform respawn;

    public void TakeDamages(int damages, Vector3 origin)
    {
        this.gameObject.transform.position = respawn.position;
    }

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
