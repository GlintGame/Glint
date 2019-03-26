using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionObserver : MonoBehaviour
{
    public SlashyAI AI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
            this.AI.OnDetected(collision.gameObject);
    }
}
