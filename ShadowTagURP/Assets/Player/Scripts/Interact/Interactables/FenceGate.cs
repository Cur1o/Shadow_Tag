using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGate : Interactable
{
    [SerializeField] GameObject handel;
    [SerializeField] private bool isLeft;
    [SerializeField] private bool isRight;
    private bool switcher = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected override void Interact()
    {
        if (isLeft && switcher == true) 
        { 
            gameObject.transform.localRotation = Quaternion.Euler(0, 0,-120);
            handel.transform.localRotation = Quaternion.Euler(0, 0, 120);
        }
        else if (isLeft && switcher == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            handel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (isRight && switcher == true)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 120);
            handel.transform.localRotation = Quaternion.Euler(0, 0, -120);
        }
        else if (isRight && switcher == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            handel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
