//This code contains parts from a Tutorial https://youtu.be/gPPGnpV1Y1c
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class Interactable : MonoBehaviour
    {
        public string promptMessage;   //Message that can be displayed to the player
        public void BaseInteract()
        {
            Interact();
        }
        protected virtual void Interact()
        {

        }
    }
}

