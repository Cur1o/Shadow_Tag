using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] GameObject handel;
    [SerializeField] private bool isLeft;
    [SerializeField] private bool isRight;
    // Start is called before the first frame update
    void Start()
    {

    }
    protected override void Interact()
    {
        if (handel.transform.localRotation == Quaternion.Euler(0, 0, 0) && isLeft == true)
        {
            handel.transform.localRotation = Quaternion.Euler(0, 0, -120);
        }
        else if (handel.transform.localRotation == Quaternion.Euler(0, 0, -120) && isLeft == true)
        {
            handel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (handel.transform.localRotation == Quaternion.Euler(0, 0, 0) && isRight == true)
        {
            handel.transform.localRotation = Quaternion.Euler(0, 0, 120);
        }
        else if (handel.transform.localRotation == Quaternion.Euler(0, 0, 120) && isRight == true)
        {
            handel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
