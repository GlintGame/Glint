using UnityEngine;

public class TestPickup : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    private void Awake()
    {
        this.ParticleSystem = this.GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var picker = collision.gameObject.GetComponent<IPicker>();

        if (picker != null)
        {
            picker.Pick(1);
            AudioManager.Play("Correct");
            this.gameObject.SetActive(false);
        }
    }
}
