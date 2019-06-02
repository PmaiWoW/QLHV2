using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMngr : MonoBehaviour
{
    // Vars
    [SerializeField] private GameObject plr;
    [SerializeField] private CameraShake cam;
    [SerializeField] private TextMeshProUGUI collectablesText;

    public byte LvlCollectables { get; private set; }
    public byte PlayerCollectables { get; private set; }

    public Vector3 CheckPoint { get; set; }
    
    //public static GameMngr Instance { get; private set; }

    // When script is loaded
    private void Awake()
    {
        CheckPoint = new Vector3(7.494f, -3.5f, 0.0f);

        //If no gamemngr ever existed, we are it.
        //if (Instance == null)
        //{
        //    Instance = this;
            DontDestroyOnLoad(this);
        //}
        //else if (this != Instance)
        //{
        //    Destroy(gameObject);
        //}
    }

    // Each time scene is loaded
    private void Start()
    {
        LvlCollectables = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public void CameraShake(float duration, float ammount)
    {
        cam.Shake(duration, ammount);
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
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.3f);
        // To do after
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void NextLvl()
    {
        PlayerCollectables = LvlCollectables;
        LvlCollectables = 0;
    }

    public void CollectablePickup()
    {
        LvlCollectables ++;
        collectablesText.text = LvlCollectables.ToString();
    }
}
