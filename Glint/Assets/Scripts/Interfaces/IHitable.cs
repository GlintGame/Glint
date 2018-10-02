using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable{

    void TakeDamages(int damages, Vector3 origin);

}
