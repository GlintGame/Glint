using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {

    private static Singleton instance;

    void Awake()
    {
        if (Singleton.instance == null)
        {
            Singleton.instance = this;

            GameObject.DontDestroyOnLoad(this);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
