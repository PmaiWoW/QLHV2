using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blob : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetTxtObject;
    private Animator txtAnim;
    [SerializeField] private BoxCollider2D visionRange;
    [SerializeField] private BoxCollider2D deathBox;
    [SerializeField] private KeyCode keyToInput;
    [SerializeField] private string direction;
    [SerializeField] private float speed;
    [SerializeField] private float timeModifier;

    private Animator anim;
    private Rigidbody2D rb;
    private bool triggered;
    private Vector2 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        txtAnim = targetTxtObject.GetComponent<Animator>();
        visionRange.enabled = true;
        triggered = false;        
        deathBox.enabled = false;


        switch (direction)
        {
            case "Up":
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                velocity = new Vector2(0, speed);
                visionRange.offset = new Vector2(0f, visionRange.offset.y);
                break;
            case "Down":
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                velocity = new Vector2(0, -speed);
                visionRange.offset = new Vector2(0f, -visionRange.offset.y);
                break;
            case "Right":
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                velocity = new Vector2(speed, 0);
                visionRange.offset = new Vector2
                    (visionRange.offset.y + 0.2f, 0.1f);
                visionRange.size = new Vector2
                    (visionRange.size.y, visionRange.size.x);
                break;
            case "Left":
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                velocity = new Vector2(-speed, 0);
                visionRange.offset = new Vector2
                    (-visionRange.offset.y + 0.2f, 0.1f);
                visionRange.size = new Vector2
                    (visionRange.size.y, visionRange.size.x);
                break;
        }
        rb.freezeRotation = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Time.timeScale = timeModifier;
            txtAnim.Play("Warning");
            OnTrigger();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Obstacle")
            triggered = false;

        targetTxtObject.enabled = false;
        deathBox.enabled = false;
        targetTxtObject.text = "";
        anim.Play("Death");
    }



    private void OnTrigger()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 255f;
        GetComponent<SpriteRenderer>().color = tmp;
        visionRange.enabled = false;
        deathBox.enabled = true;
        triggered = true;
        targetTxtObject.text = keyToInput.ToString();
        targetTxtObject.enabled = true;
    }

    private void Update()
    {
        if (triggered)
        {
            rb.velocity = velocity;

            if (Input.GetKeyDown(keyToInput))
            {
                Debug.Log(keyToInput.ToString());
                Death();
            }
        }
    }

    private void Death()
    {
        deathBox.enabled = false;
        triggered = false;
        anim.Play("Death");
        txtAnim.Play("Inputed");
        rb.velocity = new Vector2(0f,0f);
        Time.timeScale = 1;
    }
}
