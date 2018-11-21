using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressionPlat : MonoBehaviour {

    public float dep = 1;
    
    public Transform image;
    public GameObject ToActivate;
    private IActivable activable;

    private bool activated = false;

    private void Awake()
    {
        this.activable = this.ToActivate.GetComponent<IActivable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.activated = true;
        this.image.position = new Vector2(this.image.position.x, this.image.position.y - this.dep);
        this.activable.Activate();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.activated = false;
        this.image.position = new Vector2(this.image.position.x, this.image.position.y + this.dep);
        this.activable.Disactivate();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.activated == false)
        {
            this.OnTriggerEnter2D(collision);
        }
    }

}
