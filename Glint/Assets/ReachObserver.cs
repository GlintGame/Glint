using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachObserver : MonoBehaviour
{
    public SlashyAI AI;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
            this.AI.OnReach();
    }
}
