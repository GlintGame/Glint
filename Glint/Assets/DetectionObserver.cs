using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionObserver : MonoBehaviour
{
    public int detectionLayer = 9;

    [System.Serializable]
    public class DetectionEvent : UnityEvent<GameObject> { }

    public DetectionEvent OnDetectOut;
    public DetectionEvent OnDetectIn;
    public DetectionEvent OnDetectStay;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == this.detectionLayer)
            this.OnDetectOut.Invoke(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == this.detectionLayer)
            this.OnDetectIn.Invoke(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == this.detectionLayer)
            this.OnDetectStay.Invoke(collision.gameObject);
    }
}
