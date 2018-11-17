
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour, IHitable
{

    private Transform Transform;

    private void Awake()
    {
        this.Transform = gameObject.GetComponent<Transform>();
    }

    public void TakeDamages(int damages, Vector3 origin)
    {
        int pushDirection = (this.Transform.position.x - origin.x) > 0 ? 1 : -1;
        Vector2 push = Vector2.right * damages * pushDirection;
        this.GetComponent<Rigidbody2D>().velocity += push;
    }
}
