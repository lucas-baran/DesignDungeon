using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public GameObject GameplaySettingsMenu;
    public GameObject SoundSettingsMenu;
    public GameObject InputUIMenu;

    public delegate void BackButtonHandler();
    public event BackButtonHandler OnBackButton;
    public Scrollbar MasterScrollbar;
    public Scrollbar MusicsScrollbar;
    public Scrollbar EffectsScrollbar;
    public Scrollbar VoicesScrollbar;

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
    private void InputSystem_OnActionKeyChanged( EKeyActionType key_action_type, KeyCode key_code )
    {
        ShowInputUI();
    }

    public void ChangeVolumeMaster( float new_volume )
    {
        AudioManager.Instance.SetVolume( EVolumeType.Master, new_volume );
    }
    public void ChangeVolumeMusics( float new_volume )
    {
        AudioManager.Instance.SetVolume( EVolumeType.Musics, new_volume );
    }
    public void ChangeVolumeVoices( float new_volume )
    {
        AudioManager.Instance.SetVolume( EVolumeType.Voices, new_volume );
    }
    public void ChangeVolumeEffects( float new_volume )
    {
        AudioManager.Instance.SetVolume( EVolumeType.Effects, new_volume );
    }

    public void ShowInputUI()
    {
        InputUIMenu.SetActive( true );
    }

    public void HideInputUI()
    {
        InputUIMenu.SetActive( false );
    }

    public void UpdateScrollBars()
    {
        MasterScrollbar.value = Mathf.Pow( 10, AudioManager.Instance.GetVolume( EVolumeType.Master ) / 40 );
        MusicsScrollbar.value = Mathf.Pow( 10, AudioManager.Instance.GetVolume( EVolumeType.Musics ) / 40 );
        EffectsScrollbar.value = Mathf.Pow( 10, AudioManager.Instance.GetVolume( EVolumeType.Effects ) / 40 );
        VoicesScrollbar.value = Mathf.Pow( 10, AudioManager.Instance.GetVolume( EVolumeType.Voices ) / 40 );
    }

    private void OnEnable()
    {
        InputSystem.Instance.OnActionKeyChanged += InputSystem_OnActionKeyChanged;
        GameplaySettingsMenu.SetActive( true );
        SoundSettingsMenu.SetActive( false );
        InputUIMenu.SetActive( false );
        UpdateScrollBars();
    }

    private void OnDisable()
    {
        InputSystem.Instance.OnActionKeyChanged -= InputSystem_OnActionKeyChanged;
    }

    public void BackButton()
    {
        OnBackButton?.Invoke();
    }
}
