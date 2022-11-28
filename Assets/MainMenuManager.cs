using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject GameplaySettingsMenu;
    public GameObject SoundSettingsMenu;
    public GameObject CreditsMenu;

    public void Awake()
    {
        BackButton();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SoundButton()
    {
        GameplaySettingsMenu.SetActive( false );
        SoundSettingsMenu.SetActive( true );
    }
    public void GameplayButton()
    {
        GameplaySettingsMenu.SetActive( true );
        SoundSettingsMenu.SetActive( false );
    }

    public void SettingsButton()
    {
        MainMenu.SetActive( false );
        SettingsMenu.SetActive( true );
        GameplaySettingsMenu.SetActive( true );
        SoundSettingsMenu.SetActive( false );
    }

    public void BackButton()
    {
        MainMenu.SetActive( true );
        SettingsMenu.SetActive( false );
        GameplaySettingsMenu.SetActive( false );
        SoundSettingsMenu.SetActive( false );
        CreditsMenu.SetActive( false );
    }

    public void CreditButton()
    {
        CreditsMenu.SetActive( true );
        MainMenu.SetActive( false );
        SettingsMenu.SetActive( false );
        GameplaySettingsMenu.SetActive( false );
        SoundSettingsMenu.SetActive( false );

    }

    public void OpenLink(string link )
    {
        Application.OpenURL( link );
    }
}
