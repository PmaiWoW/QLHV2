using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class win : MonoBehaviour
{
    public GameObject player;
    public GameObject allLight;

    public TextMeshProUGUI winText;

    public GameObject retryButton;
    public GameObject exitButton;


    // Start is called before the first frame update
    void Start()
    {

        winText.enabled = !winText.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 9.3f)
        {
            allLight.SetActive(false);

            winText.enabled = !winText.enabled;
            allLight.SetActive(false);
            Destroy(player);
            exitButton.SetActive(true);
            retryButton.SetActive(true);
        }
    }
}
