using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGate : Interactable
{
    [SerializeField] GameObject handel;
    [SerializeField] private bool isLeft;
    [SerializeField] private bool isRight;
    [SerializeField] private GameObject Fence;
    private Animator currentAnimator;
    // Start is called before the first frame update
    void Start()
    {
        currentAnimator = Fence.GetComponent<Animator>();
    }
    protected override void Interact()
    {
        if (handel.transform.localRotation == Quaternion.Euler(0, 0, 0) && isLeft == true)
        {
            currentAnimator.SetTrigger("OpenFence");
            currentAnimator.ResetTrigger("CloseFence");
            //handel.transform.localRotation = Quaternion.Euler(0, 0, -120);
        }
        else if (handel.transform.localRotation == Quaternion.Euler(0, 0, -120) && isLeft == true)
        {
            currentAnimator.SetTrigger("CloseFence");
            currentAnimator.ResetTrigger("OpenFence");
            //handel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
