using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public bool moving = false;
    public BoxCollider2D centerBox;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        centerBox = GetComponent<BoxCollider2D>();
        centerBox.enabled = !centerBox.enabled;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (rb.velocity[0] == 0 && rb.velocity[1] == 0)
            moving = false;

        if (moving == false && (x != 0 || y != 0))
        {
            rb.velocity = new Vector2(x * speed, y * speed);
            moving = true;
            centerBox.enabled = !centerBox.enabled;
        }

        if (moving == true && rb.velocity[0] < 0) // Moving left
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        else if (rb.velocity[1] > 0) // Moving up
        {
            rb.velocity = new Vector2(0, speed);
        }
        else if (rb.velocity[1] < 0) // Moving down
        {
            rb.velocity = new Vector2(0, -speed);
        }
        else if (rb.velocity[0] > 0) // Moving right
        {
            rb.velocity = new Vector2(speed, 0);
        }

        if (rb.velocity[0] == 0 && rb.velocity[1] == 0)
            moving = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision");
        if (moving == false )
            centerBox.enabled = !centerBox.enabled;
    }
}
