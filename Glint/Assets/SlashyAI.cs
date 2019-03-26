using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashyAI : MonoBehaviour
{

    private SlashyStates state = SlashyStates.Idle;
    private enum SlashyStates
    {
        Follow,
        Hit,
        Idle,
    }

    private CharacterController2D CharacterController;
    private Transform Transform;
    private Transform target;
    // use incoming events to change state and hadle atacks
    public void OnDetected(GameObject target)
    {
        this.state = SlashyStates.Follow;
        this.target = target.GetComponent<Transform>();
    }

    public void OnEscape()
    {
        this.state = SlashyStates.Idle;
        this.target = null;
    }

    public void OnReach()
    {
        if(this.state != SlashyStates.Hit) StartCoroutine(Slash());
    }

    private void Awake()
    {
        this.CharacterController = this.GetComponent<CharacterController2D>();
        this.Transform = this.GetComponent<Transform>();
    }

    private void Update()
    {
        if(this.state == SlashyStates.Follow && this.target != null)
        {
            float hmove = this.Transform.position.x < this.target.position.x ? 1 : -1;
            this.CharacterController.Move(hmove, true);
        }
    }

    public IEnumerator Slash()
    {
        this.state = SlashyStates.Hit;

        yield return new WaitForSeconds(0.5f);
        // attack !
        yield return new WaitForSeconds(0.5f);

        this.state = SlashyStates.Follow;
    }
}


