using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button startButton;
    public string sceneToLoad;

    void Start()
    {
        startButton.onClick.AddListener(TaskOnClick);
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            LoadNext();
        }
    }
}