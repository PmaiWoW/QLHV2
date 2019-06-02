using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private GameMngr gameManager;
    private BoxCollider2D triggerBox;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        triggerBox = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Collected
        if (col.tag == "Player")
        {
            gameManager.CollectablePickup();
            anim.Play("Pickup");
            triggerBox.enabled = false;
        }
    }
}
