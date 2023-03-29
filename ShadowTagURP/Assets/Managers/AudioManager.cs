using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    private void Start()
    {
        SetAudio();
    }
    public void SetAudio()
    {
        mixer.SetFloat("master", GameManager.Instance.masterVolume);
        mixer.SetFloat("music", GameManager.Instance.musicVolume);
        mixer.SetFloat("sfx", GameManager.Instance.effectVolume);
        mixer.SetFloat("ambience", GameManager.Instance.ambienceVolume);
        mixer.SetFloat("dialouge", GameManager.Instance.dialougeVolume);
    }
}
