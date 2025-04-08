using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Main");
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
}
