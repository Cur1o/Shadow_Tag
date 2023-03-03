using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput playerInput;                //The Player Input
        public PlayerInput.PlayerWalkActions onWalk;    //The Player Action while Walking
        public PlayerInput.WeaponActions onWeapon;      //The Player Action while holding a gun
        private PlayerMovement movement;                //The Player Movement
        private PlayerLook look;                        //The Player Look
        private Gun gun;                                //
                                                        
        void Awake()
        {
            playerInput = new PlayerInput();            //the Player input element gets assigned
            onWalk = playerInput.PlayerWalk;            //the player action onWalk gets assigned
            movement = GetComponent<PlayerMovement>();  //the Script PlayerMovement is searched and assigned
            look = GetComponent<PlayerLook>();          //the Script PlayerLook is searched and assigned
            gun = GetComponent<Gun>();                  //the script Gun
            onWalk.Jump.performed += ctx => movement.Jump();    //if the player jumps the jump function in PlayerMovement is called
            onWalk.Crouch.performed += ctx => movement.Crouch(); //if the player crouches the crouch function in PlayerMovement is called
            onWalk.Sprint.performed += ctx => movement.Sprint();    //if the player sprints the spriunt funtion in PlayerMovement is caslled
        }
        void FixedUpdate()
        {
            movement.ManageMove(onWalk.Move.ReadValue<Vector2>());  //sends the movement information to PlayerMovement to move the player
        }
        private void LateUpdate()
        {
            look.ManageLook(onWalk.Look.ReadValue<Vector2>());
        }
        // Update is called once per frame
        void Update()
        {

        }
        //public void switchWeapon(int gunNumber)
        //{
        //    switch (Gun.gunT)
        //    {
        //        case gunType.pistol:
        //            if (gunNumber == 0)
        //            {
        //                prefab.SetActive(true);
        //            }
        //            else
        //            {
        //                prefab.SetActive(false);
        //            }
        //            break;
        //        case gunType.assaultRifle:
        //            if (gunNumber == 1)
        //            {
        //                prefab.SetActive(true);
        //            }
        //            else
        //            {
        //                prefab.SetActive(false);
        //            }
        //            break;
        //        case gunType.rifle:
        //            if (gunNumber == 2)
        //            {
        //                prefab.SetActive(true);
        //            }
        //            else
        //            {
        //                prefab.SetActive(false);
        //            }
        //            break;
        //        case gunType.shotgun:
        //            if (gunNumber == 3)
        //            {
        //                prefab.SetActive(true);
        //            }
        //            else
        //            {
        //                prefab.SetActive(false);
        //            }
        //            break;
        //        default:

        //            break;
        //    }
        //}
        private void OnEnable()
        {
            onWalk.Enable();    //The action is enabled
        }
        private void OnDisable()
        {
            onWalk.Disable();   //The action is disabled
        }
    }
}
