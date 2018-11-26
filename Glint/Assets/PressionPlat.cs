using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressionPlat : MonoBehaviour {

    public float dep = 1;
    
    public Transform image;
    public GameObject ToActivate;
    private IActivable activable;
    private int ObjectOnPlatform = 0;

    private void Awake()
    {
        this.activable = this.ToActivate.GetComponent<IActivable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.ObjectOnPlatform++;
        if (this.ObjectOnPlatform == 1)
        {
            this.image.position = new Vector2(this.image.position.x, this.image.position.y - this.dep);
            this.activable.Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.ObjectOnPlatform--;
        if (this.ObjectOnPlatform == 0)
        {
            this.image.position = new Vector2(this.image.position.x, this.image.position.y + this.dep);
            this.activable.Disactivate();
        }
    }

}
