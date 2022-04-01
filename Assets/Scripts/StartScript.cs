using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }
}
