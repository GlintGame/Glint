using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayOnTop : MonoBehaviour {

	void OnSceneLoaded()
    {
        this.transform.SetAsLastSibling();
    }
}
