using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public enum enemyAudio {hello = 0, hello2 = 1, GhostHit = 2, GhostDeath = 3, SkeletonHit = 4, SkeletonDeath = 5, SlimeHit = 6, SlimeDeath = 7, collectCoin = 8}
    public enemyAudio currentAudioToPlay;
    [SerializeField] private List<AudioClip> EnemyAudioList;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
    }
    public void SetAudio()
    {
        mixer.SetFloat("master", Settings.Instance.masterVolume.value);
        mixer.SetFloat("music", Settings.Instance.musicVolume.value);
        mixer.SetFloat("sfx", Settings.Instance.effectVolume.value);
        mixer.SetFloat("ambience", Settings.Instance.ambienceVolume.value);
        mixer.SetFloat("dialouge", Settings.Instance.dialougeVolume.value);
    }
    public void PlayAudio(enemyAudio audioToPlay)
    {
        currentAudioToPlay = audioToPlay;
        audioSource.PlayOneShot(EnemyAudioList[(int)currentAudioToPlay]);
    }
}
