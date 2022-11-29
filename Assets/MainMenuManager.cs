using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public SettingsMenuManager SettingsMenu;
    public GameObject CreditsMenu;

    [SerializeField] private Scenario _playScenario;

    public void Awake()
    {
        CreditBack();
    }

    private void OnEnable()
    {
        SettingsMenu.OnBackButton += SettingsMenu_BackButton;
    }

    public void PlayButton()
    {
        GameManager.Instance.LoadScenario( _playScenario );
    }

    public void CreditBack()
    {
        MainMenu.SetActive( true );
        SettingsMenu.gameObject.SetActive( false );
        CreditsMenu.SetActive( false );
    }

    public void SettingsMenu_BackButton()
    {
        MainMenu.SetActive( true );
        SettingsMenu.gameObject.SetActive( false );
    }

    public void QuitButton()
    {
        Application.Quit();
    }
   

    public void SettingsButton()
    {
        MainMenu.SetActive( false );
        SettingsMenu.gameObject.SetActive( true );
    }

    public void CreditButton()
    {
        CreditsMenu.SetActive( true );
        MainMenu.SetActive( false );
        SettingsMenu.gameObject.SetActive( false );
    }

    public void OpenLink(string link )
    {
        Application.OpenURL( link );
    }
}
