using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGuy : MonoBehaviour
{
    public static SceneGuy instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Error: 2 sceneGuys");
            return;
        }
        instance = this;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
