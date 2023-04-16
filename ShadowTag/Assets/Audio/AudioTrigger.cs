using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioTrigger : MonoBehaviour
{
    public enum audioClips {equipWeapon = 0, equipNewWapon = 1}
    public audioClips audioClipsStory;
    [SerializeField] AudioClip triggerAudio;
    [SerializeField] AudioSource source;
    bool first = true;
    [SerializeField] bool isPortal;
    private void Start() => source = GetComponent<AudioSource>();
    public void TriggerAudio() => source.PlayOneShot(triggerAudio);
    private void OnTriggerEnter(Collider other)
    {
        if (first)
        {
            if (source != null)
            {
                TriggerAudio();
            }
            else
            {
                AudioManager.Instance.storyAudioSource.PlayOneShot(triggerAudio);
            }
            first = false;
        }
    }
    public void PlayStoryAudio()
    {

        AudioManager.Instance.PlayStoryAudio((int)audioClipsStory);
    }
}
