using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPlay : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
