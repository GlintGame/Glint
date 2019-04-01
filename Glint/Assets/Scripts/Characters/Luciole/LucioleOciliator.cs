using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucioleOciliator : MonoBehaviour
{
    public float Aplitude = 1;
    public float Frequency = 1;

    private float RadCount = 0;

    private Vector2 Oringin;

    private void Awake()
    {
        this.Oringin = this.transform.position;
        this.RadCount = Random.Range(0, Mathf.PI);
    }

    void FixedUpdate()
    {
        float newY = Mathf.Sin(RadCount) * this.Aplitude;
        this.transform.position = new Vector2(Oringin.x, Oringin.y + newY);

        this.RadCount += Mathf.PI * Time.fixedDeltaTime * this.Frequency;
    }
}
