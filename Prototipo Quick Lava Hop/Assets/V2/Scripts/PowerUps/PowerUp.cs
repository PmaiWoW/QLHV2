using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    // The value of the object ex: Speed or Number of keys
    public abstract float Value { get; set; }

    // On Pickup
    public abstract void OnPickup();
}
