using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput playerInput;                //The Player Input
        public PlayerInput.PlayerWalkActions onWalk;   //The Player Action
        private PlayerMovement movement;                //The Player Movement
        private PlayerLook look;                        //The Player Look   
                                                        
        void Awake()
        {
            playerInput = new PlayerInput();            //the Player input element gets assigned
            onWalk = playerInput.PlayerWalk;            //the player action onWalk gets assigned
            movement = GetComponent<PlayerMovement>();  //the Script PlayerMovement is searched and assigned
            look = GetComponent<PlayerLook>();          //the Script PlayerLook is searched and assigned        
            onWalk.Jump.performed += ctx => movement.Jump();    //if the player jumps the jump function in PlayerMovement is called
            onWalk.Crouch.performed += ctx => movement.Crouch(); //if the player crouches the crouch function in PlayerMovement is called
            onWalk.Sprint.performed += ctx => movement.Sprint();
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
