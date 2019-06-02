using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class holedeath : MonoBehaviour
{

    public GameObject allLight;
    public GameObject warningLight;

    public GameObject retryButton;
    public GameObject exitButton;
       
    public TextMeshProUGUI gameOverTxt;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player") // Start of input time
        {
            allLight.SetActive(false);
            warningLight.SetActive(true);

            gameOverTxt.enabled = !gameOverTxt.enabled;
            allLight.SetActive(false);
            Destroy(player);
            exitButton.SetActive(true);
            retryButton.SetActive(true);
        }
    }
}