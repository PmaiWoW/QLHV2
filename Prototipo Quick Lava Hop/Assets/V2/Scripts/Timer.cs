using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Vars
    [SerializeField] private GameMngr gameManager;
    [SerializeField] private float timeLimitSeconds;
    [SerializeField] private Image timerImage;
    private Animator imgAnim;
    [SerializeField] private TextMeshProUGUI timerText;

    private float fixedTimeOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        fixedTimeOnDeath = Time.fixedTime;
        imgAnim = timerImage.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Math for time to percentage
        float time =
            Mathf.Floor(100 -
            ((Time.fixedTime - fixedTimeOnDeath) * 100 / timeLimitSeconds));

        // Visual cues
        timerImage.fillAmount -= Time.deltaTime / timeLimitSeconds;
        timerText.text = $"{time.ToString()} %";

        // Timer ended
        if (time <= 0)
        {
            time = 0;
            timerImage.fillAmount = 1;
            imgAnim.SetBool("Warning", false);

            // Disable player
            //GameOver();
            gameManager.RestartLvl();
        }
        else if (time <= 30)
        {
            imgAnim.SetBool("Warning", true);
        }
    }
}
