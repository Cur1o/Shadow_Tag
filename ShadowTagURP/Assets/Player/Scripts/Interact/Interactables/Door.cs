using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] GameObject handel;
    [SerializeField] private bool isLeft;
    [SerializeField] private bool isRight;
    [SerializeField] private GameObject house;
    private Animator currentAnimator;
    // Start is called before the first frame update
    void Start()
    {
        currentAnimator = house.GetComponent<Animator>();
    }
    protected override void Interact()
    {
        if (handel.transform.localRotation == Quaternion.Euler(0, 0, 0) && isLeft == true)
        {
            currentAnimator.SetTrigger("OpenDoor");
            currentAnimator.ResetTrigger("CloseDoor");
            //handel.transform.localRotation = Quaternion.Euler(0, 0, -120);
        }
        else if (handel.transform.localRotation == Quaternion.Euler(0, 0, -120) && isLeft == true)
        {
            currentAnimator.SetTrigger("CloseDoor");
            currentAnimator.ResetTrigger("OpenDoor");
            //handel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
