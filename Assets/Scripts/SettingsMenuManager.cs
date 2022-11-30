using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI textToChange;

    public KeyActionButtonData[] _keyActionButtons = null;
    
    [System.Serializable]
    public struct KeyActionButtonData
    {
        public EKeyActionType KeyAction;
        public ButtonText button;
    }

    private void OnButtonPressed( EKeyActionType key_action )
    {
        ShowInputUI( key_action );
        InputSystem.Instance.ListenActionKey( key_action );
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

    private void InputSystem_OnActionKeyChanged( EKeyActionType key_action_type, KeyCode key_code )
    {
        foreach( var key_action_button in _keyActionButtons)
        {
            if ( key_action_type == key_action_button.KeyAction )
            {
                key_action_button.button.text.text = key_code.ToString();
            }
        }
        HideInputUI();
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

    public void ShowInputUI( EKeyActionType key_action_type )
    {
        InputUIMenu.SetActive( true );
        textToChange.text = key_action_type.ToString();
    }

    public void HideInputUI()
    {
        InputUIMenu.SetActive( false );
    }

    public void InputSystem_OnCancelKeyChanged()
    {
        HideInputUI();
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
        InputSystem.Instance.OnCancelKeyChanged += InputSystem_OnCancelKeyChanged;
        GameplaySettingsMenu.SetActive( true );
        SoundSettingsMenu.SetActive( false );
        InputUIMenu.SetActive( false );
        UpdateScrollBars();
    }

    private void OnDisable()
    {
        InputSystem.Instance.OnActionKeyChanged -= InputSystem_OnActionKeyChanged;
        InputSystem.Instance.OnCancelKeyChanged -= InputSystem_OnCancelKeyChanged;
    }

    public void BackButton()
    {
        OnBackButton?.Invoke();
    }
    private void Awake()
    {
        foreach( var key_action_button in _keyActionButtons )
        {
            key_action_button.button.button.onClick.AddListener( () => OnButtonPressed( key_action_button.KeyAction ) );
        }
    }
}
