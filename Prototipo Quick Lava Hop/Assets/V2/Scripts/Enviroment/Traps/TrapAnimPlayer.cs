using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAnimPlayer : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            anim.Play("Triggered");
        }
    }
}
