using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUMN = "MusicVolume";
    public static MusicManager Instance { get; private set; }
    
    private AudioSource audioSource;
    
    private float volume = .3f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUMN, .3f);
        audioSource.volume = volume;
    }
    
    public void ChangeVolume()
    {
        volume += .1f;
        // Round the volume to one decimal place so 1.0000001f becomes perfectly 1.0f
        volume = Mathf.Round(volume * 10f) / 10f;
        // volumn = volumn % 1.1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUMN, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}