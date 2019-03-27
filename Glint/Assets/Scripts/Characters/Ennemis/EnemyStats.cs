using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour, IHitable
{
    public int Damages = 10;

    public int MaxHp = 100;

    [System.Serializable]
    public class EnemyHpChange : UnityEvent<int, int> { }
    public EnemyHpChange onPvChange;
    public UnityEvent onDie;

    private int hp;
    public int Hp
    {
        get
        {
            return this.hp;
        }
        private set
        {
            this.onPvChange.Invoke(value, this.MaxHp);
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

        if (this.Hp <= 0)
            this.Kill();
    }

    public void Kill()
    {
        Destroy(this.gameObject);
        this.onDie.Invoke();
    }
}
