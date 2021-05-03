using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSliders : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer SFXmixer;
    public AudioMixer EnviormentMixer;

    public void SetMusicVolume (float volume)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10 (volume) *20 );
        Debug.Log(volume);
    }

    public void SetSFXSound(float volume)
    {
        SFXmixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);

        Debug.Log(volume);
    }

    public void SetEnviormentSound(float volume)
    {
        EnviormentMixer.SetFloat("EnviormentSounds", Mathf.Log10(volume) * 20);

        Debug.Log(volume);
    }
}
