using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, IHitable, IRespawnable {

    private Vector3 respawnPotition;
    private Rigidbody2D Rigidbody;
    private Transform Transform;

    public int MaxHp = 100;
    public Vector2 pushForce = new Vector2(40, 20);

    [System.Serializable]
    public class PlayerHPChange : UnityEvent<int, int> { }
    public PlayerHPChange onPvChange;

    private int hp;
    public int Hp
    {
        get
        {
            return this.hp;
        }
        private set
        {
            this.hp = value;
            this.onPvChange.Invoke(value, this.MaxHp);
        }
    }

    public bool IsInvicible { get; set; }

    private void Awake()
    {
        this.Transform = this.GetComponent<Transform>();
        this.Rigidbody = this.GetComponent<Rigidbody2D>();
        this.respawnPotition = this.Transform.position;
        this.Hp = this.MaxHp;
    }

    public void respawn()
    {
        this.Hp = this.MaxHp;
        this.StartCoroutine(utils.Coroutine.Do.StopLookAhead(1f));

        this.Rigidbody.velocity = Vector2.zero;
        this.Transform.position = this.respawnPotition;
    }

    public void setRespawn(Transform position)
    {
        this.respawnPotition = position.position;
    }

    public void TakeDamages(int damages, Vector3 origin)
    {
        if (this.IsInvicible)
            return;

        int pushDirection = (this.Transform.position.x - origin.x) > 0 ? 1 : -1;
        var push = new Vector2(this.pushForce.x * pushDirection, this.pushForce.y);
        this.Rigidbody.velocity += push;

        this.Hp -= damages;

        if(this.Hp <= 0)
        {
            this.respawn();
        }
    }
}
