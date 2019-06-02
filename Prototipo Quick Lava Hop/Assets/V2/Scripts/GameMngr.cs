using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMngr : MonoBehaviour
{
    // Vars
    [SerializeField] private float timerSeconds;
    [SerializeField] private Image img;
    private Animator imgAnim;
    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private GameObject plr;
    [SerializeField] private Camera cam;

    public Vector3 CheckPoint { get; set; }

    private float fixedTimeOnDeath;

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        CheckPoint = new Vector3(7.494f, -3.5f, 0.0f);
        fixedTimeOnDeath = 0;
        imgAnim = img.GetComponent<Animator>();
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Math for time to percentage
        float time =
            Mathf.Floor(100 -
            ((Time.fixedTime - fixedTimeOnDeath) * 100 / timerSeconds));

        // Visual cues
        img.fillAmount -= Time.deltaTime / timerSeconds;
        timeTxt.text = $"{time.ToString()} %";

        // Timer ended
        if (time <= 0)
        {
            time = 0;
            img.fillAmount = 1;
            imgAnim.SetBool("Warning", false);

            // Disable player
            //GameOver();
            RestartLvl();
        }
        else if (time <= 30)
        {
            imgAnim.SetBool("Warning", true);
        }
    }

    public void CameraShake(float duration, float ammount)
    {
        cam.GetComponent<CameraShake>().Shake(duration, ammount);
    }

    // Game over, very explicit
    public void GameOver()
    {
        Time.timeScale = 1;
        //plr.SetActive(false);
        // Run over, game over scene
    }

    // Also very explicit
    public void RestartLvl()
    {
        StartCoroutine(Restart());
    }

    // Wait death anim to complete
    private IEnumerator Restart()
    {
        fixedTimeOnDeath = Time.fixedTime;
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.2f);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        // To do after
    }
}
