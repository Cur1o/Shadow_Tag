using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGate : Interactable
{
    [SerializeField] GameObject handel;
    [SerializeField] private bool isLeft;
    [SerializeField] private bool isRight;
    [SerializeField] private GameObject Fence;
    [SerializeField] AudioTrigger audioTrigger;
    private Animator currentAnimator;
    bool switcher = true;
    
    // Start is called before the first frame update
    void Start()
    {
        currentAnimator = Fence.GetComponent<Animator>();
    }
    protected override void Interact()
    {
        if (handel.transform.localRotation == Quaternion.Euler(0, 0, 0) && switcher == true)
        {
            switcher = !switcher;
            currentAnimator.SetTrigger("OpenFence");
            currentAnimator.ResetTrigger("CloseFence");
            audioTrigger.TriggerAudio();
        }
        else if (handel.transform.localRotation == Quaternion.Euler(0, 0, 120) && switcher == false)
        {
            switcher = !switcher;
            currentAnimator.SetTrigger("CloseFence");
            currentAnimator.ResetTrigger("OpenFence");
            audioTrigger.TriggerAudio();
        }
    }
}
