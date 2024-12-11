using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Options()
    {
        ///
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Menu(){
        Debug.Log("Menu Clicked");
        SceneManager.LoadScene(0);
    }
}
