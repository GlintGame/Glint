using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour, IHitable
{
    public int Damages = 10;

    public int MaxHp = 100;

    public UnityEvent onPvChange;

    private int hp;
    private int Hp
    {
        get
        {
            return this.hp;
        }
        set
        {
            this.onPvChange.Invoke();
            this.hp = value;
        }
    }

    private Transform Transform;

    private void Awake()
    {
        this.Transform = gameObject.GetComponent<Transform>();
        this.Hp = this.MaxHp;
    }

    public void TakeDamages(int damages, Vector3 origin)
    {
        int pushDirection = (this.Transform.position.x - origin.x) > 0 ? 1 : -1;
        Vector2 push = Vector2.right * damages * pushDirection;
        this.GetComponent<Rigidbody2D>().velocity += push;
        
        this.Hp -= damages;
        
        Debug.Log("enemy was has taken" + damages + "damages, it has " + this.Hp + "hp");

        if (this.Hp <= 0)
            this.Kill();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject.GetComponent<IHitable>();

        if(other != null && !(other is EnemyStats))
        {
            other.TakeDamages(this.Damages, this.transform.position);
        }
    }

    public void Kill()
    {
        Debug.Log("enemy was killed");
        Destroy(this.gameObject);
    }
}
