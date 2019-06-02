using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // GM Script
    [SerializeField] private GameMngr gm;

    // Animator
    private Animator anim;

    // RB
    private Rigidbody2D rb;

    // Colliders
    [SerializeField] private BoxCollider2D upCol, downCol, leftCol, rightCol;

    // Movement vars
    [SerializeField] private float initialSpeed;
    public float Speed { get; set; }
    private bool moving;
    private bool onWater;
    private float speedHold;
    private float x;
    private float y;
    public string Direction { get; private set; }

    // PowerUp Vars
    private bool speedUp;


    // Awake
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        DisableDirectionalColliders();
        Speed = initialSpeed;
        anim.speed = 0;
        Direction = "";
        onWater = false;
    }

    private void UpdateBlendTreeParams()
    {
        // Update blend tree params
        anim.SetFloat("HSpeed", rb.velocity.x);
        anim.SetFloat("VSpeed", rb.velocity.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Inputs
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Check if player is wants to move
        if (moving == false && (x != 0 || y != 0))
        {
            moving = true;
            anim.speed = 1;

            rb.velocity = new Vector2(x * Speed, y * Speed);

            if (speedUp && !onWater)
            {
                anim.speed /= 3;
                Speed = initialSpeed;
                speedUp = false;
            }
        }

        // Moving left
        if (moving == true && rb.velocity[0] < 0)
        {
            rb.velocity = new Vector2(-Speed, 0);
            EnableCollider(leftCol);
            UpdateBlendTreeParams();
            Direction = "Left";
        }
        // Moving up
        else if (moving == true && rb.velocity[1] > 0)
        {
            rb.velocity = new Vector2(0, Speed);
            EnableCollider(upCol);
            UpdateBlendTreeParams();
            Direction = "Up";
        }
        // Moving down
        else if (moving == true && rb.velocity[1] < 0)
        {
            rb.velocity = new Vector2(0, -Speed);
            EnableCollider(downCol);
            UpdateBlendTreeParams();
            Direction = "Down";
        }
        // Moving right
        else if (moving == true && rb.velocity[0] > 0)
        {
            rb.velocity = new Vector2(Speed, 0);
            EnableCollider(rightCol);
            UpdateBlendTreeParams();
            Direction = "Right";
        }
    }

    // Disable helping colliders
    private void DisableDirectionalColliders()
    {
        upCol.enabled = false;
        downCol.enabled = false;
        leftCol.enabled = false;
        rightCol.enabled = false;
    }

    // Enable specific helper collider
    private void EnableCollider(Collider2D col)
    {
        col.enabled = true;
    }

    // Collisions
    private void OnCollisionEnter2D(Collision2D col)
    {
        moving = false;
        DisableDirectionalColliders();

        if (col.collider.tag == "Obstacle")
        {
            rb.velocity = new Vector2(0f, 0f);
            //gm.CameraShake(0.01f, 0.02f);
            anim.speed = 0;
            onWater = false;
        }

        else if (col.collider.tag == "Hazard")
        {
            anim.Play("Death");
            anim.speed = 1;
            gm.CameraShake(0.4f, 0.4f);
            rb.velocity = new Vector2(0f, 0f);
            gm.RestartLvl();
        }

        else if (col.collider.tag == "Water" && !onWater)
        {
            onWater = true;
            rb.velocity = new Vector2(0f, 0f);
            anim.speed = 0;
        }
    }

    // Handling powerups
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.tag);
        // Check the tag
        if (col.tag == "PowerUp")
            switch (col.name[0])
            {
                // Speed Up
                case 'S':
                    float speed = col.GetComponent<SpeedUp>().Value;
                    col.GetComponent<SpeedUp>().OnPickup();
                    Destroy(col.gameObject);

                    speedUp = true;
                    Speed *= speed;
                    speedHold = speed;
                    Debug.Log(speedHold);
                    anim.speed *= 2;

                    // Check if it is a speed down
                    if (speed < 1)
                        gm.CameraShake(0.05f, 0.2f);

                    break;
            }

        // Checkpoint ######### USE IT TO SOFT RESTART #########
        else if (col.tag == "CheckPoint")
        {
            gm.CheckPoint = col.transform.position;
            Destroy(col.gameObject);
        }

        // Portal
        else if (col.tag == "Portal")
        {
            if (moving)
            {
                transform.position = col.GetComponent<Portal>().ExitPortal;
                gm.CameraShake(0.05f, 0.03f);
            }
        }
    }
}
