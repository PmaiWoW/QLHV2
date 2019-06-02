using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpeedUp : PowerUp
{
    [SerializeField] private float speedUpValue;

    public override float Value
    {
        get => speedUpValue;
        set
        {
            Value = speedUpValue;
        }
    }

    public override void OnPickup()
    {
        Debug.Log("BEAM ME UP SCOTTY");        
    }
}
