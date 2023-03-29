using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGate : Interactable
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
        if (gameObject.transform.localRotation == Quaternion.Euler(0, 0, 0) && isLeft == true) 
        { 
            gameObject.transform.localRotation = Quaternion.Euler(0, 0,-120);
            handel.transform.localRotation = Quaternion.Euler(0, 0, 120);
        }
        else if (gameObject.transform.localRotation == Quaternion.Euler(0, 0, -120) && isLeft == true)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            handel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (gameObject.transform.localRotation == Quaternion.Euler(0, 0, 0) && isRight == true)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 120);
            handel.transform.localRotation = Quaternion.Euler(0, 0, -120);
        }
        else if (gameObject.transform.localRotation == Quaternion.Euler(0, 0, 120) && isRight == true)
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
