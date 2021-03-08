using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour
{
    // private void OnCollisionExit(Collision other)
    // {
    //     other.collider.isTrigger = false;
    // }

    private void OnTriggerExit(Collider other)
    {
        other.isTrigger = false;
    }
}
