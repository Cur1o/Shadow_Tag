using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        private Camera cam;
        [Header("Interaction Settings")]
        [SerializeField] private float distance = 3f;
        [SerializeField] private LayerMask mask;
        private PlayerUI playerUI;
        private InputManager inputManager;
        void Start()
        {
            cam = GetComponent<PlayerLook>().cam;
            playerUI = PlayerUI.Instance;
            inputManager = GetComponent<InputManager>();
        }

        // Update is called once per frame
        void Update()
        {
            playerUI.UpdateText(string.Empty);
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * distance);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo, distance, mask))
            {
                if(hitInfo.collider.GetComponent<Interactable>() != null)
                {
                    Interactable interact = hitInfo.collider.GetComponent<Interactable>();
                    playerUI.UpdateText(interact.promptMessage);   //the update Text function from PlayerUI is called
                    if (inputManager.onWalk.Interact.triggered)
                    {
                        interact.BaseInteract();
                    }
                }
            }
        }
    }
}
