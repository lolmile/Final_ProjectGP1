using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public void clickRetryButton()
    {
        SceneManager.LoadScene("demoMap");
    }
    public void ClickBackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ClickQuitButton()
    {
        Application.Quit();
    }
}
