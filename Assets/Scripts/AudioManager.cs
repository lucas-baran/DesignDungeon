using UnityEngine;
using UnityEngine.Audio;

public enum EVolumeType
{
    Master,
    Musics,
    Effects,
    Voices,
}

public enum ESoundType
{
    DamageReceived,
    Death,
    Happy,
    Attack,
    AzzuroAngry,
    AzzuroTalking,
    Musicpool,
}

[System.Serializable]
public struct SoundPoolType
{
    public ESoundType type;
    public AudioClip[] pool;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource musics_source;
    public AudioSource voices_source;
    public AudioSource effects_source;
    public AudioMixer mixer;

    public SoundPoolType[] Sounds;

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

    public void PlaySound( ESoundType type)
    {
        AudioClip sound_to_play = null;
        foreach( var pool in Sounds )
        {
            if (pool.type == type )
            {
                sound_to_play = pool.pool[ Random.Range( 0, pool.pool.Length ) ];
            }
        }

        switch( type )
        {
            case ESoundType.DamageReceived:
            case ESoundType.Death:
            case ESoundType.Happy:
            case ESoundType.AzzuroAngry:
            case ESoundType.AzzuroTalking:
            case ESoundType.Attack:
                voices_source.PlayOneShot( sound_to_play );
                break;

            case ESoundType.Musicpool:
                musics_source.clip = sound_to_play;
                break;
            default:
                break;
        }
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

    private void Start()
    {
        SetVolume( EVolumeType.Master, 0.5f );
        SetVolume( EVolumeType.Musics, 0.5f );
        SetVolume( EVolumeType.Effects, 0.5f );
        SetVolume( EVolumeType.Voices, 0.5f );

        musics_source.loop = true;
    }
}
