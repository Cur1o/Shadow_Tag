using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioMixer mixer;
    private void Awake()
    {
        Instance = this;
    }
    public void SetAudio()
    {
        //Debug.Log("Start setting Audio "+ mixer);
        mixer.SetFloat("master", Settings.Instance.masterVolume.value);
        mixer.SetFloat("music", Settings.Instance.musicVolume.value);
        mixer.SetFloat("sfx", Settings.Instance.effectVolume.value);
        mixer.SetFloat("ambience", Settings.Instance.ambienceVolume.value);
        mixer.SetFloat("dialouge", Settings.Instance.dialougeVolume.value);
    }
}
