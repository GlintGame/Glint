using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeObserver : MonoBehaviour
{
    public SlashyAI AI;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
            this.AI.OnEscape();
    }
}
