using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BadGuy : MonoBehaviour
{
    public bool attackUp, attackDown, attackRight, attackLeft, visible;
    public BoxCollider2D vision;
    Rigidbody2D rb;
    public float speed = 5;
    SpriteRenderer sr;
    public GameObject player;
    public Rigidbody2D playerRB;

    public KeyCode input = KeyCode.Q;

    public GameManager gm;

    public GameObject allLight;
    public GameObject warningLight;

    public float inputTime = 20.0f;
    public TextMeshProUGUI letterQ;
    public TextMeshProUGUI gameOverTxt;


    public GameObject retryButton;
    public GameObject exitButton;

    // Start is called before the first frame update
    void Start()
    {
        visible = false;
        attackDown = false;
        attackUp = true;
        attackRight = false;
        attackLeft = false;
        vision = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = !sr.enabled;

        playerRB = player.GetComponent<Rigidbody2D>();

        warningLight.SetActive(false);
        letterQ.enabled = !letterQ.enabled;

        retryButton.SetActive(false);
        exitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (visible)
        {
            playerRB.velocity = new Vector2(0, 0);
            if (Input.GetKeyDown(input))
                EndOfInputTime();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player") // Start of input time
        {
            allLight.SetActive(false);
            warningLight.SetActive(true);
            Destroy(vision);
            playerRB.velocity = new Vector2(0, 0);

            if (visible == false)
            {
                sr.enabled = !sr.enabled;
                letterQ.enabled = !letterQ.enabled;
                visible = true;
            }

            if (visible == true)
                if (attackDown)
                    AttackDown();
                else if (attackUp)
                    AttackUp();
                else if (attackLeft)
                    AttackLeft();
                else if (attackRight)
                    AttackRight();

            Time.timeScale = 0.2f;
        }

        if (col.gameObject.name == "Tilemap Obstacles") // End of input time
        {
            EndOfInputTime();
            gameOverTxt.enabled = !gameOverTxt.enabled;
            allLight.SetActive(false);
            Destroy(player);
            exitButton.SetActive(true);
            retryButton.SetActive(true);
        }
    }

    public void AttackUp()
    {
        rb.velocity = new Vector2(0, speed);
    }
    public void AttackDown()
    {
        rb.velocity = new Vector2(0, -speed);
    }
    public void AttackLeft()
    {
        rb.velocity = new Vector2(-speed, 0);
    }
    public void AttackRight()
    {
        rb.velocity = new Vector2(speed, 0);
    }
    public void EndOfInputTime()
    {
        allLight.SetActive(true);
        warningLight.SetActive(false);
        letterQ.enabled = !letterQ.enabled;
        Destroy(gameObject);
        Time.timeScale = 1f;
        

        if (attackDown)
            playerRB.velocity = new Vector2(-2, 0);
        else if (attackUp)
            playerRB.velocity = new Vector2(2, 0);
        else if (attackLeft)
            playerRB.velocity = new Vector2(0, -2);
        else if (attackRight)
            playerRB.velocity = new Vector2(0, 2);
    }
}