using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject allLight;
    public float deathTimer = 40.0f;
    public TextMeshProUGUI TextPro;
    public TextMeshProUGUI gameOverTxt;
    public string menu, lvl1, lvl2, lvl3; // Insert Scene names in inspector
    public string toLoadWhenRetry;
    public TextMeshProUGUI escText;
    public GameObject retryButton;
    public GameObject exitButton;
    public bool loadNextLvl = true;
    public bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        retryButton.SetActive(false);
        exitButton.SetActive(false);
        gameOverTxt.enabled = !gameOverTxt.enabled;
        escText.enabled = !escText.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            TextPro.text = (deathTimer -= Time.deltaTime).ToString("#");
        }
        else if (deathTimer < 0)
        {
            TextPro.text = "0";
            gameOverTxt.enabled = !gameOverTxt.enabled;
            GameOver();
        }
        if (loadNextLvl)
            if (player.transform.position.x > 9.3f)
            {
                LoadScene(lvl2);
            }
        //pause
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    public void GameOver()
    {
        Destroy(player);
        allLight.SetActive(false);
        exitButton.SetActive(true);
        retryButton.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Scene to load: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void Pause()
    {
        pause = !pause;
        if (pause)
        {
            exitButton.SetActive(true);
            escText.enabled = !escText.enabled;
            Time.timeScale = 0.05f;
        }
        else
        {
            escText.enabled = !escText.enabled;
            exitButton.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
    public void Retry()
    {
        LoadScene(toLoadWhenRetry);
    }
}
