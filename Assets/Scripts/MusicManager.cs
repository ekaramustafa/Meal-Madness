using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance { get; private set; }

    private AudioSource auidoSource;
    private float volume;

    private void Awake()
    {
        Instance = this;
        auidoSource = GetComponent<AudioSource>();
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }

        auidoSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }


}
