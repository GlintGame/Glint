using Characters.Player.Skills;
using UnityEngine;

public class AFireBall : MonoBehaviour {

    private GameObject from;
    public GameObject From
    {
        get
        {
            return this.from;
        }
    }

    public float speed = 5f;
    public float duration = 5f;

    private int damages;
    private float direction;
    private Rigidbody2D Rigidbody;
    private Transform Transform;

    private void Awake()
    {
        this.from = GameObject
            .FindGameObjectWithTag("Player");

        this.direction = this.from
            .GetComponent<CharacterController2D>()
            .direction;

        this.damages = this.from
            .GetComponent<FireBall>()
            .FireBallDamage;

        this.Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        this.Transform = gameObject.GetComponent<Transform>();

        this.Rigidbody.velocity = Vector2.right * this.direction * speed;

        StartCoroutine(utils.Coroutine.Do.Wait(this.duration, () => Destroy(gameObject)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable target = collision.gameObject.GetComponent<IHitable>();
        if (collision.gameObject != from && target != null)
        {
            target.TakeDamages(this.damages, this.Transform.position);
            Destroy(gameObject);
        }
    }
}
