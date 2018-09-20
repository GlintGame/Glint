using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSkills : MonoBehaviour, ICharacterSkills
{
    [Range(0.4f, 1.5f)] public float dashDuration = 0.6f;
    public float dashforce = 2.5f;

    public UnityEvent OnDashEnd;

    private Rigidbody2D Rigidbody;
    private CharacterController2D CharacterController;
    private float _currentDashDuration;
    private bool isDashing = false;

    public void Awake()
    {
        this.Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        this.CharacterController = this.gameObject.GetComponent<CharacterController2D>();
    }

    public void LaunchSkills(InputsParameters inputs)
    {
        if (!this.isDashing && inputs.Dash)
        {
            StartCoroutine("Dash");
        }
    }

    public IEnumerator Dash()
    {
        int dashDirection = this.CharacterController.direction;
        this._currentDashDuration = 0;
        while(this._currentDashDuration < this.dashDuration)
        {
            this.Rigidbody.velocity = new Vector2(this.dashforce * dashDirection, 0);

            this._currentDashDuration += Time.deltaTime;
            Debug.Log(this.dashDuration - this._currentDashDuration);
            yield return null;
        }
        this.OnDashEnd.Invoke();
        this.isDashing = false;
    }
}
