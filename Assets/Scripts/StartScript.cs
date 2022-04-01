using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class StartScript : MonoBehaviour
{
    public GameObject playScene;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(playScene.name);
    }
}
