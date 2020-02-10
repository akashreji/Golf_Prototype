using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerHandle : MonoBehaviour
{
    float force;
    private void Start()
    {
        force = 0;
    }
    private void OnMouseDrag()
    {
        force += 5;
        print(force);
    }
}
