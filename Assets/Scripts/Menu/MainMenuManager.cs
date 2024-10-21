using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
    }

    public void CreditsButton()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
