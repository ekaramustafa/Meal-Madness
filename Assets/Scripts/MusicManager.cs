using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance { get; private set; }

    private AudioSource auidoSource;
    private float volume = .3f;

    private void Awake()
    {
        Instance = this;
        auidoSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME,.3f);
        auidoSource.volume = volume;

    }

    public void TurnUpVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 1f;
        }

        auidoSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();

    }

    public void TurnDownVolume()
    {
        volume -= .1f;
        if (volume < 0f)
        {
            volume = 0f;
        }

        auidoSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();

    }

    public float GetVolume()
    {
        return volume;
    }


}
