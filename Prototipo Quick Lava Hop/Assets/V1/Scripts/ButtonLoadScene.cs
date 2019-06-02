using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button startButton;
    public string sceneToLoad;

    void Start()
    {
        startButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}