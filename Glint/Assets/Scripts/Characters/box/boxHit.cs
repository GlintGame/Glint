using UnityEngine;

public class BoxHit : MonoBehaviour, IHitable
{
    public Sprite happy;
    public Sprite sad;

    private Transform Transform;
    private Rigidbody2D Rigidbody;

    private void Awake()
    {
        this.Transform = this.GetComponent<Transform>();
        this.Rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void TakeDamages(int damages, Vector3 origin)
    {
        int pushDirection = (this.Transform.position.x - origin.x) > 0 ? 1 : -1;
        Vector2 push = Vector2.right * damages * pushDirection;
        this.Rigidbody.velocity += push;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().sprite = this.sad;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().sprite = this.happy;
        }
    }
}
