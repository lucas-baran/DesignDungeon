using UnityEngine;
using UnityEngine.Audio;

public enum EVolumeType
{
    Master,
    Musics,
    Effects,
    Voices,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource musics_source;
    public AudioSource voices_source;
    public AudioSource effects_source;
    public AudioMixer mixer;

    public void SetVolume( EVolumeType volume_type, float new_volume )
    {
        new_volume = Mathf.Max(Mathf.Log10( new_volume ) * 40, -80f);
        switch( volume_type )
        {
            case EVolumeType.Master:
                mixer.SetFloat( "Master", new_volume );
                break;
            case EVolumeType.Musics:
                mixer.SetFloat( "Musics" , new_volume );
                break;
            case EVolumeType.Effects:
                mixer.SetFloat( "Effects", new_volume );
                break;
            case EVolumeType.Voices:
                mixer.SetFloat( "Voices", new_volume );
                break;
        }
    }

    public float GetVolume( EVolumeType volume_type )
    {
        float res = -1f;
        switch( volume_type )
        {
            case EVolumeType.Master:
                mixer.GetFloat( "Master", out res );
                break;
            case EVolumeType.Musics:
                mixer.GetFloat( "Musics", out res );
                break;
            case EVolumeType.Effects:
                mixer.GetFloat( "Effects", out res );
                break;
            case EVolumeType.Voices:
                mixer.GetFloat( "Voices", out res );
                break;
        }
        return res;
    }

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }
        else
        {
            Destroy( this );
            return;
        }

    }
}
