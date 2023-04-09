using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioTrigger : MonoBehaviour
{
    [SerializeField] AudioClip triggerAudio;
    [SerializeField] AudioSource source;
    private void Start() => source = GetComponent<AudioSource>();
    public void TriggerAudio() => source.PlayOneShot(triggerAudio);
}
